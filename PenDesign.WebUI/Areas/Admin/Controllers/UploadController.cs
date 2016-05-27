using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using PenDesign.WebUI.Authencation;
using PenDesign.Common.Utils;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin, Users")]
    public class UploadController : ApiController
    {
        [HttpGet]
        [Route("api/upload/CheckFileNameExist")]
        public HttpResponseMessage CheckFileNameExist(string fileName)
        {
            try
            {
                var path = HttpContext.Current.Server.MapPath("~/Content/UploadFiles/images/images");
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] TXTFiles = di.GetFiles(fileName);
                if (TXTFiles.Length != 0)
                {
                    var response = new { message = "Tên ảnh đã tồn tại!" };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                var response = new { message = "Lỗi!" };
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
                throw;
            }

        }

        // POST: api/Upload
        [HttpPost]
        public HttpResponseMessage POST()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                var filename = httpPostedFile.FileName;

                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/Content/UploadFiles/images/images"));
                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/UploadFiles/images/images"));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UploadFiles/images/images"), filename);

                httpPostedFile.SaveAs(fileSavePath);

                var contentType = MimeTypes.GetContentType(fileSavePath);
                if (!contentType.StartsWith("image"))
                {
                    if (File.Exists(fileSavePath))
                    {
                        File.Delete(fileSavePath);
                    }
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                var responseObj = new
                {
                    fileName = "/Content/UploadFiles/images/images/" + filename
                };
                if (!WebTools.CreateThumbnail(filename, "/Content/UploadFiles/images/images/", 300, 178, false))
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);

                return Request.CreateResponse(HttpStatusCode.OK, responseObj);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }



    }
}
