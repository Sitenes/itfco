using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;

namespace Toplearn.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("a.aghakabiri@gmail.com","تاپ لرن");
            mail.To.Add(To);
            mail.Subject = Subject;

            mail.Body = Body;
            mail.IsBodyHtml = true;


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("a.aghakabiri@gmail.com", "ckjueznojnvpecvk");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}