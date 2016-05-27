using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace PenDesign.Common.Utils
{
    public static class XMail
    {
        /// <summary>
        /// Gửi email từ hệ thống
        /// </summary>
        /// <param name="To">Email người nhận</param>
        /// <param name="Subject">Tiêu đề</param>
        /// <param name="Body">Nội dung</param>
        public static void Send(String To, String Subject, String Body)
        {
            var From = "Web Master <mrthanh.kientao@gmail.com>";
            XMail.Send(From, To, Subject, Body);
        }

        /// <summary>
        /// Gửi email từ người đến người khác
        /// </summary>
        /// <param name="From">Email người gửi</param>
        /// <param name="To">Email người nhận</param>
        /// <param name="Subject">Tiêu đề</param>
        /// <param name="Body">Nội dung</param>
        public static void Send(String From, String To, String Subject, String Body)
        {
            var Cc = "";
            var Bcc = "";
            var Attachments = "";
            var Email = "";
            var EmailPassword = "";
            var Port = 487;
            XMail.Send(Email, EmailPassword, Port, From, To, Cc, Bcc, Subject, Body, Attachments);
        }

        /// <summary>
        /// Gửi email đầy đủ thông tin
        /// </summary>
        /// <param name="Email">Email người gửi</param>
        /// <param name="EmailPassword">Email password</param>
        /// <param name="Port">Port email</param>
        /// <param name="From">Email người gửi</param>
        /// <param name="To">Email người nhận</param>
        /// <param name="Cc">Email người cùng nhận được nhìn thấy trên mail</param>
        /// <param name="Bcc">Email người cùng nhận không nhìn thấy trên mail</param>
        /// <param name="Subject">Tiêu đề</param>
        /// <param name="Body">Nội dung</param>
        /// <param name="Attachments">File định kèm</param>
        public static void Send(String Email, string EmailPassword, int Port, String From, String To, String Cc, String Bcc, String Subject, String Body, String Attachments)
        {
            // Tài khoản gmail sử dụng để phát mail
            var mail = new SmtpClient("smtp.gmail.com", Port)
            {
                Credentials = new NetworkCredential(Email, EmailPassword),
                EnableSsl = true
            };


            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(Body, null, MediaTypeNames.Text.Html);
            var imageLocation = HttpContext.Current.Server.MapPath("~/Content/images/logo.png");
            LinkedResource pic1 = new LinkedResource(imageLocation);
            pic1.ContentId = "Pic1";
            avHtml.LinkedResources.Add(pic1);

            // Mail Message
            var message = new MailMessage();
            message.AlternateViews.Add(avHtml);
            message.IsBodyHtml = true;
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            
            message.From = new MailAddress(From);
            message.To.Add(new MailAddress(To));
            message.Subject = Subject;
            //message.Body = Body;
            message.ReplyToList.Add(message.From);

            if (!String.IsNullOrEmpty(Cc))
            {
                message.CC.Add(Regex.Replace(Cc, @"[,;\s]+", ","));
            }
            if (!String.IsNullOrEmpty(Bcc))
            {
                message.Bcc.Add(Regex.Replace(Bcc, @"[,;\s]+", ","));
            }
            if (!String.IsNullOrEmpty(Attachments))
            {
                var filenames = Attachments.Split(',', ';');
                foreach (var filename in filenames)
                {
                    message.Attachments.Add(new Attachment(filename.Trim()));
                }
            }

            // Send mail message
            mail.Send(message);
        }
    }
}