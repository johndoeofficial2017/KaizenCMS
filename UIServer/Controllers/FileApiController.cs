using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace UIServer.Controllers
{
    public class FileApiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage DownloadFile(string filename, string Destination)
        {
            filename = filename.Replace("\\\\", "\\").Replace("'", "").Replace("\"", "");
            if (!char.IsLetter(filename[0]))
            {
                filename = filename.Substring(2);
            }
            filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Photo/" + Destination), filename);
            var fileinfo = new FileInfo(filename);
            if (!fileinfo.Exists)
            {
                throw new FileNotFoundException(fileinfo.Name);
            }

            try
            {
                var excelData = File.ReadAllBytes(filename);
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new MemoryStream(excelData);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileinfo.Name
                };
                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage OpenFile(string filename, string Destination, string filetype)
        {
            filename = filename.Replace("\\\\", "\\").Replace("'", "").Replace("\"", "");
            if (!char.IsLetter(filename[0]))
            {
                filename = filename.Substring(2);
            }
            filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Photo/" + Destination), filename);
            var fileinfo = new FileInfo(filename);
            if (!fileinfo.Exists)
            {
                var result = new Kaizen.Admin.KaizenResult()
                {
                    Status = false,
                    Massage = "File Not Fount !",
                    Description = "There is no file on the server with the same name !!"
                };
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "File Not Found !!");
            }

            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                System.Diagnostics.Process.Start(filename);
                return result;
            }
            catch (FileNotFoundException ex)
            {
                var result = new Kaizen.Admin.KaizenResult() {
                    Status=false,
                    Massage="File Not Fount !",
                    Description="There is no file on the server with the same name !!"
                };
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "File Not Found !!");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex);
            }
        }


    }
}
