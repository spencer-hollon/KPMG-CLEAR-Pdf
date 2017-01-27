using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CLEARPDF_WebAPI.Controllers
{
    public class caSOSForm460Controller : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPDF([FromUri] string orgID, [FromUri] Guid recordID) {
            try
            {
                //retrieve the login info for orgID from the config file

                //connect to the CRM endpoint

                //retrieve the corresponding record from the CRM

                //retrieve the PDF template from the local file system

                //iterate over CRM record filling in fields on the template PDF

                //return filled PDF to browser with ContentDisposition set 

                //HttpResponseMessage fileResult = new HttpResponseMessage(HttpStatusCode.OK);
                //return fileResult;

                var response = new HttpResponseMessage();
                response.Content = new StringContent("<html><body>returned from GetPDF</body></html>");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;

            }
            catch (Exception error) {
                HttpResponseMessage errorResult = new HttpResponseMessage();
                errorResult.Content = new StringContent("<html><body>Error : <br> "+ error.Message  +"</body></html>");
                errorResult.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return errorResult;
            }
            

        }//close GetPDF
    }
}
