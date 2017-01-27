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
using PdfSharp;
using PdfSharp.Pdf;

using System.Configuration;
using System.Security;

using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;

using PDFMeta;


namespace CLEARPDF_webAPI2.Controllers
{
    public class pdfApiController : ApiController
    {
        private IOrganizationService _orgService;
        private OrganizationServiceProxy _serviceProxy;



        [HttpGet]
        public HttpResponseMessage GetPDF(string formName, [FromUri] string orgID, [FromUri] string entityName, [FromUri] string recordID)
        {
            SecureString secureString = new SecureString();
            foreach (char c in "Password10@".ToCharArray())
            {
                secureString.AppendChar(c);
            }

            try
            {
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
                    string CRMDRecord_fromDate;
                    string CRMRecord_througDate;
                    string CRMRecord_committeeID;
                    Guid recordGUID = new Guid(recordID);
                    string CRMRecord_committeename;
                    Entity CRMRecord = new Entity(entityName);

                    try
                    {
                        _orgService = (IOrganizationService)_serviceProxy;
                        //retrieve the corresponding record from the CRM
                        CRMRecord = _orgService.Retrieve(CRMRecord.LogicalName, recordGUID, new ColumnSet(true));

                    }
                    catch (Exception ex) { throw new Exception("Unable to retrieve CRM record <br> " + ex.Message); }

                    /* hard coded for demo */
                    CRMDRecord_fromDate = ((DateTime)CRMRecord["clear_statementperiodfrom"]).ToShortDateString();
                    CRMRecord_througDate = ((DateTime)CRMRecord["clear_statementperiodthrough"]).ToShortDateString();
                    CRMRecord_committeeID = (string)CRMRecord["clear_listidnumber"];
                    CRMRecord_committeename = ((EntityReference)CRMRecord["clear_committeename"]).Name;
                    //throw new Exception("Retrieved committeename = " + CRMRecord_committeename);

                    //retrieve the PDF template from the local file system

                    //iterate over CRM record filling in fields on the template PDF

                    //return filled PDF to browser with ContentDisposition set 

                    //HttpResponseMessage fileResult = new HttpResponseMessage(HttpStatusCode.OK);
                    //return fileResult;

                    Random rnd = new Random();

                    string pathToTemplate = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + formName + ".pdf";
                    string pathToTemplateMeta = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\" + formName + ".xml";

                    if (!File.Exists(pathToTemplate)) { throw new Exception("Cannot find template PDF for : " + formName); }
                    if (!File.Exists(pathToTemplateMeta)) { throw new Exception("Cannot find XML Meta file for : " + formName); }


                    string pathToTempFile = Environment.GetEnvironmentVariable("HOME").ToString() + "\\pdfTemplates\\temp" + formName + rnd.Next(1, 98732).ToString() + ".pdf";
                    FileInfo templateFile = new FileInfo(pathToTemplate);
                    templateFile.CopyTo(pathToTempFile, true);

                    //proceed only if the forgoing copy worked                
                    if (File.Exists(pathToTempFile))
                    {

                        using (PdfDocument pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(pathToTempFile, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Modify))
                        {
                            //get a collection of acro fields out of the pdf template
                            var pdfFields = pdfDoc.AcroForm.Fields;

                            //  this if/else structure is necessary to allow the pre-poulated values to show on the PDF
                            if (pdfDoc.AcroForm.Elements.ContainsKey("/NeedAppearances") == false)
                            {
                                pdfDoc.AcroForm.Elements.Add(
                                    "/NeedAppearances",
                                    new PdfSharp.Pdf.PdfBoolean(true));
                            }
                            else
                            {
                                pdfDoc.AcroForm.Elements["/NeedAppearances"] =
                                    new PdfSharp.Pdf.PdfBoolean(true);
                            }



                            for (int i = 0; i < pdfFields.Count(); i++)
                            {
                                try
                                {
                                    //Get the current PDF field
                                    var pdfField = pdfFields[i];
                                    if (pdfField.Name == "pg1-stmt cvrs - from")
                                    {

                                        pdfField.Value = new PdfSharp.Pdf.PdfString(CRMDRecord_fromDate);
                                        //pdfField.Value = new PdfSharp.Pdf.PdfString("10/08/1982");
                                        //throw new Exception("fieldName = "+pdfField.Name+"  and Value = "+pdfField.Value.ToString());
                                    }
                                    if (pdfField.Name == "pg1-stmt cvrs - through")
                                    {

                                        pdfField.Value = new PdfSharp.Pdf.PdfString(CRMRecord_througDate);
                                        //pdfField.Value = new PdfSharp.Pdf.PdfString("10/08/1982");
                                        //throw new Exception("fieldName = "+pdfField.Name+"  and Value = "+pdfField.Value.ToString());
                                    }
                                    if (pdfField.Name == "pg1-3 Comm ID#")
                                    {

                                        pdfField.Value = new PdfSharp.Pdf.PdfString(CRMRecord_committeeID);
                                        //pdfField.Value = new PdfSharp.Pdf.PdfString("10/08/1982");
                                        //throw new Exception("fieldName = "+pdfField.Name+"  and Value = "+pdfField.Value.ToString());
                                    }
                                    if (pdfField.Name == "pg1-3 comm name")
                                    {

                                        pdfField.Value = new PdfSharp.Pdf.PdfString(CRMRecord_committeename);
                                        //pdfField.Value = new PdfSharp.Pdf.PdfString("10/08/1982");
                                        //throw new Exception("fieldName = "+pdfField.Name+"  and Value = "+pdfField.Value.ToString());
                                    }


                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Error writing to PDF in memory <br>" + ex.Message);
                                }
                            }//close for loop

                            pdfDoc.Save(pathToTempFile);
                            pdfDoc.Close();
                        }//close using

                        //only after PDF sharp has for sure disposed of the pdfDoc do we then read it back into memory for delivery to client
                        //read the tempPDF into a memorystream which will be delivered to Client while original file is destroyed
                        byte[] pdfFile = File.ReadAllBytes(pathToTempFile);
                        MemoryStream pdfMemStream = new MemoryStream(pdfFile);

                        File.Delete(pathToTempFile);

                        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                        result.Content = new StreamContent(pdfMemStream);
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        result.Content.Headers.ContentDisposition.FileName = formName + ".pdf";
                        //the following cookie is needed by the http://johnculviner.com/ file download jQuery plugin
                        CookieHeaderValue fileDownloadCookie = new CookieHeaderValue("fileDownload", "true");
                        
                        fileDownloadCookie.Path = "/";

                        List<CookieHeaderValue> tempList = new List<CookieHeaderValue>();
                        tempList.Add(fileDownloadCookie);

                        result.Headers.AddCookies(tempList);
                        result.Headers.Add("Cache-Control", "max-age=60, must-revalidate");

                        return result;

                    }//close if file exists
                    else
                    {
                        HttpResponseMessage errorResult = new HttpResponseMessage();
                        errorResult.Content = new StringContent("<html><body>Error : <br> File Does not exist at Path = " + pathToTempFile + "</body></html>");
                        errorResult.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                        return errorResult;
                    }
                    /*
                    var response = new HttpResponseMessage();
                    response.Content = new StringContent("<html><body>returned from GetPDF  Path to file = "+ pathToFile + "</body></html>");
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                    return response;
                    */
                }//close using _serviceProxy
            }//wrapping try
            catch (Exception error)
            {
                HttpResponseMessage errorResult = new HttpResponseMessage();
                errorResult.Content = new StringContent("<html><body>Caught Exception : <br> " + error.Message + "</body></html>");
                errorResult.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return errorResult;
            }
        }
    }
}



