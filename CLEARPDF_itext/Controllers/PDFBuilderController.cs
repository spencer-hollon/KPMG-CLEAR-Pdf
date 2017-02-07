using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Text;

using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;


using System.Configuration;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;

using PDFMeta;



namespace CLEARPDF_itext.Controllers
{
    public class pdfApiController : ApiController
    {
        private IOrganizationService _orgService;
        private OrganizationServiceProxy _serviceProxy;
        private string primaryPDFname;
        private List<PDFFillMeta> metaObjs = new List<PDFFillMeta>();
        private Dictionary<string, string> tempFiles = new Dictionary<string, string>(); //Dictionary<string = CRMEntityName, string = FullPathTOTempPDF>




        [HttpGet]
        public HttpResponseMessage GetPDF(string formName, [FromUri] string orgID, [FromUri] string recordID)
        {
            primaryPDFname = formName;

            SecureString secureString = new SecureString();
            foreach (char c in "Password10@".ToCharArray())
            {
                secureString.AppendChar(c);
            }

            try
            {

                //import the XML which describes the PDF files and mappings in strongly typed C# objects
                BuildMetaObjs();

                //Build Temporary files which the rest of the script will need 
                BuildTempFiles();




                /* CRM Connection Stuff */
                //retrieve the login info for orgID from the config file
                //during dev we will user array[0] and perform no orgChecking  
                string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;


                // Connect to the CRM web service using a connection string.
                //CrmServiceClient conn = new CrmServiceClient(connectionString);
                CrmServiceClient conn = new CrmServiceClient("admin@RBDMS.onmicrosoft.com", secureString, "NorthAmerica", "org4f013c98", false, false, null, true);


                // Cast the proxy client to the IOrganizationService interface.
                //_orgService = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;

                //connect to the CRM endpoint

                using (_serviceProxy = conn.OrganizationServiceProxy)
                {
                    _orgService = (IOrganizationService)_serviceProxy;

                    //iterate over CRM record filling in fields on the template PDF
                    //return filled PDF to browser with ContentDisposition set 
                    //HttpResponseMessage fileResult = new HttpResponseMessage(HttpStatusCode.OK);
                    //return fileResult;

                    //iterate over the list of metaObjs filling the matching TempXML for each
                    foreach (PDFFillMeta metaObj in metaObjs)
                    {
                        //get the Primary record
                        Entity PrimaryRecord = getRecord(metaObj.CRMEntityName, new Guid(recordID));

                        PdfReader PrimaryPDFReader = new PdfReader(tempFiles[metaObj.CRMEntityName]);
                        string outputFilePath = tempFiles[metaObj.CRMEntityName] + "Output";

                        using (PdfStamper stamper = new PdfStamper(PrimaryPDFReader, new FileStream(outputFilePath, FileMode.Create)))
                        {
                            //get a collection of acro fields out of the pdf template
                            AcroFields pdfFields = stamper.AcroFields;
                            var fieldNames = pdfFields.Fields.Keys;

                            //iterate over all the metaObj text fields
                            foreach (PDFFillMeta.textMapField textMap in metaObj.textFields)
                            {
                                //iterate over acrofields in the PDF looking for match to current textMap
                                foreach (string fieldName in fieldNames)
                                {
                                    //Replace Address Form field with my custom data
                                    if (fieldName == textMap.acroFieldName)
                                    {
                                        pdfFields.SetField(fieldName, mapTextField(textMap,PrimaryRecord));
                                    }
                                }


                            }//close foreach iterate over textmapfields

                            //iterate over all the metaObj conditional checkboxes
                            foreach (PDFFillMeta.conditionalCheckbox checkBox in metaObj.conditionalCheckboxes)
                            {
                                /*
                                //iterate over acrofields in the PDF looking for match to current textMap
                                foreach (string fieldName in fieldNames)
                                {
                                    //Replace Address Form field with my custom data
                                    if (fieldName.Contains(checkBox.))
                                    {
                                        pdfFields.SetField(fieldName, mapTextField(textMap, PrimaryRecord));
                                    }
                                }
                                */

                            }//close foreach iterate over textmapfields


                            //if the PDF has a subgrid in it.
                            if (metaObj.subGrids.Count > 0)
                            {

                                foreach (PDFFillMeta.subGridPDF subGrid in metaObj.subGrids)
                                {

                                    EntityCollection subRecords = getRelatedRecords(metaObj.CRMEntityName, metaObj.PrimaryEntityPrimaryKey, subGrid.relatedEntityName, subGrid.relatedEntityKey);
                                    int counter = 0;
                                    foreach (PDFFillMeta.subGridRow row in subGrid.subGridRows)
                                    {
                                        Entity thisRecord = subRecords[counter];

                                        foreach (PDFFillMeta.subGridColumn column in row.rowMap)
                                        {
                                            if (column.textField != null)
                                            {
                                                //iterate over the 
                                               

                                            }
                                        }


                                        counter++;
                                    }//close foreach subgrid row
                                }
                            }//close if isSubPDF

                            stamper.Close();
                        }//close using PdfStamper stamper

                        PrimaryPDFReader.Close();
                        PrimaryPDFReader.Dispose();
                        //Delete the Temporary Template and replace with our new output file
                        File.Delete(tempFiles[metaObj.CRMEntityName]);
                        tempFiles[metaObj.CRMEntityName] = outputFilePath;




                    }//close foreach which iterates over the metaObjs
                     //only after PDF sharp has for sure disposed of the pdfDoc do we then read it back into memory for delivery to client

                    //read the tempPDF into a memorystream which will be delivered to Client while original file is destroyed

                    //merge the files together
                    //string pathToMergedFile = buildFinalTemp();

                    //instead of merging we will send the one file since this is POC

                    byte[] pdfFile = File.ReadAllBytes(tempFiles[metaObjs[0].CRMEntityName]);
                    MemoryStream pdfMemStream = new MemoryStream(pdfFile);


                    //cleanup tempfiles
                    File.Delete(tempFiles[metaObjs[0].CRMEntityName]);





                    //Send the final merged File to the Client
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new StreamContent(pdfMemStream);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = formName + ".pdf";
                    //the following cookie is needed by the http://johnculviner.com/ file download jQuery plugin
                    CookieHeaderValue fileDownloadCookie = new CookieHeaderValue("fileDownload", "true");
                    fileDownloadCookie.Path = "/";
                    List<CookieHeaderValue> cookieList = new List<CookieHeaderValue>();
                    cookieList.Add(fileDownloadCookie);

                    result.Headers.AddCookies(cookieList);
                    result.Headers.Add("Cache-Control", "max-age=60, must-revalidate");

                    return result;


                    /*
                    var response = new HttpResponseMessage();
                    response.Content = new StringContent("<html><body>returned from GetPDF  Path to file = "+ pathToFile + "</body></html>");
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                    return response;
                    */
                }//close using _serviceProxy
            }//close outtermost wrapping try
            catch (Exception error)
            {
                HttpResponseMessage errorResult = new HttpResponseMessage();
                errorResult.Content = new StringContent("<html><body>Caught Exception : <br> " + error.Message + "</body></html>");
                errorResult.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return errorResult;
            }
        }//close GetPDF

        private void BuildTempFiles()
        {
            Random rnd = new Random();

            //iterate over the list of metaObjs and create a temp file for each.  fill the dictionary in the parent class with these items
            foreach (PDFFillMeta metaObj in metaObjs)
            {
                string pathToTempFile = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\temp" + metaObj.PDFFileName + rnd.Next(1, 98732).ToString() + ".pdf";
                FileInfo templateFile = new FileInfo(Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + metaObj.PDFFileName + ".pdf");
                templateFile.CopyTo(pathToTempFile, true);
                if (File.Exists(pathToTempFile)) { tempFiles.Add(metaObj.CRMEntityName, pathToTempFile); }


            }

            return;

        }//cloe BuildTempFiles

        private string buildFinalTemp()
        {
            try
            {
               
                Random rnd = new Random();
                string pathFinalTemp = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\temp" + primaryPDFname + rnd.Next(1, 98732).ToString() + ".pdf";
                /*
                // Open the output document
                PdfDocument outputDocument = new PdfDocument();


                // Iterate files
                foreach (KeyValuePair<string, string> fileEntry in tempFiles)
                {
                    // Open the document to flatten it
                    PdfDocument inputDocument = PdfSharp.Pdf.IO.PdfReader.Open(fileEntry.Value, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Modify);
                    //if (inputDocument.AcroForm != null) { inputDocument.AcroForm. }

                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                    inputDocument.Close();
                }//close foreach files in tempFiles

                //  this if/else structure is necessary to allow the pre-poulated values to show on the PDF
                if (outputDocument.AcroForm.Elements.ContainsKey("/NeedAppearances") == false)
                {
                    outputDocument.AcroForm.Elements.Add(
                        "/NeedAppearances",
                        new PdfSharp.Pdf.PdfBoolean(true));
                }
                else
                {
                    outputDocument.AcroForm.Elements["/NeedAppearances"] =
                        new PdfSharp.Pdf.PdfBoolean(true);
                }

                outputDocument.Save(pathFinalTemp);
                outputDocument.Close();
                */
                return pathFinalTemp;
                
            }
            catch (Exception err)
            {
                throw new Exception("Unable buildFinalTemp pdf <br> " + err.Message);
            }
        }

        private void BuildMetaObjs()
        {
            List<PDFFillMeta> returnList = new List<PDFFillMeta>();
            PDFFillMeta PrimaryMeta = new PDFFillMeta();
            XmlSerializer XMLer = new XmlSerializer(typeof(PDFFillMeta));

            //get the first PDFFillMeta using the name passed in the GET params of the original request
            if (testReqFiles(primaryPDFname))
            {
                FileStream xmlStream = new FileStream(Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + primaryPDFname + ".xml", FileMode.Open);
                PrimaryMeta = (PDFFillMeta)XMLer.Deserialize(xmlStream);
                returnList.Add(PrimaryMeta);
            }
            //get additional PDFFillMeta objects from related entities listed in the subGrids node of the primary XML file
            foreach (PDFFillMeta.subGridPDF subPDF in PrimaryMeta.subGrids)
            {
                PDFFillMeta additionalMeta = new PDFFillMeta();
                if (testReqFiles(subPDF.relatedPDF))
                {
                    FileStream xmlStream = new FileStream(Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + subPDF.relatedPDF + ".xml", FileMode.Open);
                    additionalMeta = (PDFFillMeta)XMLer.Deserialize(xmlStream);
                    returnList.Add(additionalMeta);
                }

            }

            metaObjs = returnList;
            return;
        }

        private bool testReqFiles(string formName)
        {
            string pathToTemplate = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + formName + ".pdf";
            string pathToTemplateMeta = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + formName + ".xml";

            if (!File.Exists(pathToTemplate)) { throw new Exception("Cannot find template PDF for  : " + formName); }
            if (!File.Exists(pathToTemplateMeta)) { throw new Exception("Cannot find XML Meta file for : " + formName); }

            return true;
        }

        private Entity getRecord(string type, Guid recordID)
        {
            Entity toReturn = new Entity(type);
            try
            {
                toReturn = _orgService.Retrieve(toReturn.LogicalName, recordID, new ColumnSet(true));

            }
            catch (Exception ex) { throw new Exception("Unable to retrieve CRM record <br> " + ex.Message); }

            return toReturn;

        }

        private EntityCollection getRelatedRecords(string primaryType, string primaryKey, string relatedType, string relatedKey)
        {
            EntityCollection toReturn = new EntityCollection();


            try
            {
                // Retrieve all records of type A related to Primary Record
                // where the last name of the user is not Cannon. 
                string fetchTemplate = @"
                   <fetch mapping='logical'>
                     <entity name='{0}'> 
                        <all-attributes />
                        <link-entity name='{1}' from='{2}' to='{3}' >
                        </link-entity> 
                     </entity> 
                   </fetch> ";

                string fetchXML = String.Format(fetchTemplate, relatedType, primaryType, relatedKey, primaryKey);

                //Entity relatedRecord
                toReturn = _orgService.RetrieveMultiple(new FetchExpression(fetchXML));
            }
            catch (Exception err) { throw new Exception("Error in getRelatedRecords : " + err.Message); }

            return toReturn;
        }//close getRelatedRecords

        private string mapTextField(PDFFillMeta.textMapField passedField, Entity passedRecord)
        {
            string toReturn;

            if (passedField.isConcat)
            {
                //if this is a concat field that means the crmAttribute is a string of multiple CRM attribute names that need to be retrieved seperately and added together
                StringBuilder toSetPDF = new StringBuilder();
                string[] crmFields = passedField.crmAttributeName.Split(new char[] { ',' });
                foreach (string indCrmField in crmFields)
                {
                    toSetPDF.AppendLine((string)passedRecord[indCrmField] + " ");
                }
                toReturn = toSetPDF.ToString();

            }
            else if (passedField.isDate) { toReturn = ((DateTime)passedRecord[passedField.crmAttributeName]).ToShortDateString(); }
            else if (passedField.isEntityRef) { toReturn = ((EntityReference)passedRecord[passedField.crmAttributeName]).Name; }
            else if (passedField.isMoney) { toReturn = ((Money)passedRecord[passedField.crmAttributeName]).Value.ToString(); }
            else { toReturn = (string)passedRecord[passedField.crmAttributeName]; }

            return toReturn;
        }//close mapTextField



    }//close apicontroller
}



