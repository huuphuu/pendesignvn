using Newtonsoft.Json;
using PenDesign.Common.Utils;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PenDesign.WebUI.Controllers
{
    [AllowAnonymous]
    public class ContactController : ApiController
    {
        private IContactService _contactService;
        private IConfigService _configService;

        public ContactController(IContactService contactService, IConfigService configService)
        {
            this._contactService = contactService;
            this._configService = configService;
        }
        [HttpPost]
        [Route("api/contact/EmailRegister")]
        public HttpResponseMessage EmailRegister(string recaptchaResponse, [FromBody] Contact contactModel)
        {
            try
            {
                const string secret = "6Lc1uw4TAAAAAMz5gjeKyy8NkpAl_TdwhZ0Sf3D0"; //Secrect key: 6Lc1uw4TAAAAAMz5gjeKyy8NkpAl_TdwhZ0Sf3D0  Sitekey:6Lc1uw4TAAAAAF1kpJ8cBR6bG1XdIVyALs_HRXZy
                var client = new WebClient();
                var reply = client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret,
                        recaptchaResponse));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
                if (!captchaResponse.Success)
                {
                    if (captchaResponse.ErrorCodes.Count <= 0)
                    {
                        var responseMessage = new { message = "Thành công" };
                        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    }
                    var error = captchaResponse.ErrorCodes[0].ToLower();
                    var message = "";
                    switch (error)
                    {
                        case ("missing-input-secret"):
                            message = "The secret parameter is missing.";
                            break;
                        case ("invalid-input-secret"):
                            message = "The secret parameter is invalid or malformed.";
                            break;

                        case ("missing-input-response"):
                            message = "The response parameter is missing.";
                            break;
                        case ("invalid-input-response"):
                            message = "The response parameter is invalid or malformed.";
                            break;

                        default:
                            message = "Error occured. Please try again";
                            break;
                    }
                    var rpMessage = new { message = message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, rpMessage);
                }
                else
                {

                    if (ModelState.IsValid)
                    {
                        contactModel.Status = true;
                        _contactService.Add(contactModel);

                        var justAddedContactEmail = _contactService.Entities.OrderByDescending(b => b.Id).FirstOrDefault();

                        var configService = _configService.GetAll().FirstOrDefault();
                        var companyName = configService.CompanyName;
                        var sendingEmail = configService.Email;
                        var emailPassword = configService.EmailPassword;
                        var emailPort = configService.EmailPort;
                        var emailSignature = configService.EmailSignature;
                        var emailFrom = companyName + "<" + sendingEmail + ">";
                        var CCEmail = configService.CCEmail;

                        var emailBody = "Có 1 tin nhắn mới từ Websie! <br />";
                        emailBody += "Họ tên: " + justAddedContactEmail.Name + "<br />";
                        emailBody += "Email: " + justAddedContactEmail.Email + "<br />";
                        emailBody += "Điện thoại: " + justAddedContactEmail.Phone + "<br />";
                        emailBody += "Nội dung: " + justAddedContactEmail.Description + "<br />";
                        emailBody = emailBody + "<br /><br />" + emailSignature + "<br /><img src=\"cid:Pic1\">";

                        XMail.Send(sendingEmail, emailPassword, emailPort, emailFrom, CCEmail, "", "", "Tin nhắn mới từ Websie!", emailBody, "");

                        var responseMessage = new { message = "Đã gửi tin thành công" };
                        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    }
                    else
                    {
                        var responseMessage = new { message = "Lỗi! Nội dung nhập không đúng định dạng" };
                        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    }

                    //var response = new { message = "Thành công" };
                    //return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }

            catch (Exception)
            {
                var responseMessage = new { message = "Lỗi! Vui lòng thử lại" };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
            }
        }



        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}