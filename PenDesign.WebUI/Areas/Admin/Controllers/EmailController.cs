﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PenDesign.WebUI.Authencation;
using PenDesign.Core.Model;
using System.Web.Security;
using PenDesign.Common.Utils;
using PenDesign.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Interface.Data;
using PenDesign.Core.ViewModel;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class EmailController : ApiController
    {
        private readonly IConfigService _configService;
        private readonly IContactService _contactService;
        private IUnitOfWork _unitOfWork;

        public EmailController(IConfigService configService, IContactService contactService, IUnitOfWork unitOfWork)
        {
            this._configService = configService;
            this._contactService = contactService;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/email/SendEmail")]
        public HttpResponseMessage SendEmail(EmailVM email)
        {
            try
            {
                var configService = _configService.GetAll().FirstOrDefault();
                var companyName = configService.CompanyName;
                var sendingEmail = configService.Email;
                var emailPassword = configService.EmailPassword;
                var emailPort = configService.EmailPort;
                var emailSignature = configService.EmailSignature;
                var emailFrom = companyName +  "<" + sendingEmail + ">";

                var emailBody = "";
                if (email.Body != null)
                    emailBody = email.Body + "<br /><br />" + emailSignature + "<br /><img src=\"cid:Pic1\">";
                else
                    emailBody = emailSignature + "<img src=\"cid:Pic1\">";

                XMail.Send(sendingEmail, emailPassword, emailPort, emailFrom, email.ToEmail, "", "", email.Subject, emailBody, "");

                var responseMessage = new { message = "Gửi email thành công" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            catch (Exception)
            {
                var responseMessage = new { message = "Gửi email thất bại" };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                throw;
            }
        }


        [HttpGet]
        [Route("api/email/GetContactEmails")]
        public IQueryable<Contact> GetContactEmails()
        {
            return _contactService.GetAll();
        }

        [HttpPost]
        [Route("api/email/DeleteEmail/{id}")]
        public HttpResponseMessage DeleteEmail(int id)
        {
            try
            {
                var email = _contactService.GetById(id);
                _contactService.Delete(email);
                _unitOfWork.Commit();

                var responseMessage = new { message = "Xóa thành công" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            catch (Exception)
            {
                var responseMessage = new { message = "Xóa thất bại" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);   
                throw;
            }
            
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
