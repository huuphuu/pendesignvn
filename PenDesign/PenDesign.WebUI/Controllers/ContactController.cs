﻿using Newtonsoft.Json;
using PenDesign.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PenDesign.WebUI.Controllers
{
    [AllowAnonymous]
    public class ContactrController : ApiController
    {
        [HttpPost]
        [Route("api/contact/EmailRegister")]
        public HttpResponseMessage EmailRegister(string recaptchaResponse, [FromBody] Contact contactModel)
        {
            try
            {
                const string secret = "6LcL-AoTAAAAAIAm2S9gWNuVujkNijGG4cheyk2d"; //Secrect key: 6LcL-AoTAAAAAIAm2S9gWNuVujkNijGG4cheyk2d  Sitekey:6LcL-AoTAAAAAObVhV2B5hllIV2lHGknd-prcN9y
                var client = new WebClient();
                var reply = client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret,
                        recaptchaResponse));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
                if (!captchaResponse.Success)
                {
                    if (captchaResponse.ErrorCodes.Count <= 0)
                    {
                        var responseMessage = new {message = "Thành công"};
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
                    var rpMessage = new {message = message};
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, rpMessage);
                }
                else
                {
                    //using (var db = new DBContext())
                    //{
                    //    if (contactModel.Type == "contact")
                    //    {
                    //        if (ModelState.IsValid)
                    //        {
                    //            db.EmailRegister.Add(contactModel);
                    //            db.SaveChanges();
                    //            var responseMessage = new { message = "Đã gửi tin thành công" };
                    //            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    //        }
                    //        else
                    //        {
                    //            var responseMessage = new { message = "Lỗi! Nội dung nhập không đúng định dạng" };
                    //            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    //        }

                    //    }
                    //    else if (contactModel.Type == "register")
                    //    {
                    //        if (db.EmailRegister.Any(m => m.Email == contactModel.Email))
                    //        {
                    //            var responseMessage = new { message = "Email này đã được đăng ký!" };
                    //            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    //        }
                    //        else
                    //        {
                    //            if (ModelState.IsValid)
                    //            {
                    //                db.EmailRegister.Add(contactModel);
                    //                db.SaveChanges();
                    //                var responseMessage = new { message = "Thành công" };
                    //                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    //            }
                    //            else
                    //            {
                    //                var responseMessage = new { message = "Lỗi! Email nhập không đúng định dạng" };
                    //                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    //            }

                    //        }
                    //    }
                    var response = new {message = "Thành công"};
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }

            catch (Exception)
            {
                var responseMessage = new {message = "Lỗi! Vui lòng thử lại"};
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