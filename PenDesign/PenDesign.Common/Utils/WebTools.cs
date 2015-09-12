using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;


namespace PenDesign.Common.Utils
{
    public class WebTools
    {
        public static bool CreateThumbnail(string fileName, string filePath,
            int thumbWidth, int thumbHeight, bool maintainAspect, string thumbPrefix = "thumb_")
        {
            try
            {
                //string originalFile = Path.Combine(filePath, fileName);
                string originalFile = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/UploadFiles/images/images"), fileName);
                string thumbnail_name = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/UploadFiles/images/images"), thumbPrefix + fileName);

                if (File.Exists(originalFile))
                {
                    Image source = Image.FromFile(originalFile);
                    if (source.Width <= thumbWidth && source.Height <= thumbHeight)
                    {
                        File.Copy(originalFile, thumbnail_name, true);
                        return true;
                    }

                    int width = thumbWidth;
                    int height = thumbHeight;

                    if (maintainAspect)
                    {
                        double ratio = ((double) source.Width)/thumbWidth;
                        double ratio2 = ((double) source.Height)/thumbHeight;
                        if (ratio < ratio2) ratio = ratio2;

                        width = (int) (source.Width/ratio);
                        height = (int) (source.Height/ratio);
                    }
                    Bitmap thumbnail = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(thumbnail))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.FillRectangle(Brushes.Transparent, 0, 0, width, height);
                        g.DrawImage(source, 0, 0, width, height);
                    }

                    thumbnail.Save(thumbnail_name);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            } // tạm thời ko xử lý ngoại lệ, chỉ chặn để ngoại lệ ko văng ra làm hỏng chương trình
        }

        public static string ConvertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            char c;
            for (int i = 0; i < stFormD.Length; i++)
            {
                c = stFormD[i];
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    if (c == 'Đ')
                        c = 'D';
                    else if (c == 'đ')
                        c = 'd';
                    else if (c == ' ' || c == '\t')
                        c = '-';
                    switch (c)
                    {
                        case 'Đ':
                            c = 'D'; break;
                        case 'đ':
                            c = 'd'; break;
                        case ' ':
                        case '\t':
                        case '<':
                        case '>':
                        case '*':
                        case '%':
                        case '&':
                        case ':':
                        case '\\':
                            c = '-'; break;
                    }
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static void SendEmail(string fromEmail, string password, string toEmail, string mailSubject, string mailBody)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmail, password);
            client.Timeout = 20000; // 20s

            MailMessage mail = new MailMessage(fromEmail, toEmail);
            mail.Subject = mailSubject;

            mail.Body = mailBody;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.IsBodyHtml = true;

            client.Send(mail);
        }

        public static void SendEmailResetPassword(string fromEmail, string password, string customerEmail, string resetPasswordLink, int TokenExpirationInMinutesFromNow)
        {
            string mailSubject = "Thông báo lấy lại mật khẩu";

            DateTime expDate = DateTime.Now.AddMinutes(TokenExpirationInMinutesFromNow);
            StringBuilder builder = new StringBuilder();
            builder.Append("Click vào link này <a href=\"")
                .Append(resetPasswordLink)
                .Append("\">")
                .Append(resetPasswordLink)
                .Append("</a> để tạo mật khẩu mới<br />")
                .Append("Lưu ý: đường link này chỉ có giá trị đến ")
                .Append(expDate);
            string mailBody = builder.ToString();
            SendEmail(fromEmail, password, customerEmail, mailSubject, mailBody);
        }
    }
}
