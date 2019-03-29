using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ABF.Models
{
    public class SMTPEmail
    {
        public static void SendEmail(string emailTo, string emailSubject, string emailBody)
        {
            // compose the email
            MailMessage mailMessage = new MailMessage("appledoretest@gmail.com", emailTo, emailSubject, emailBody);

            // authenticate our email account settings
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "appledoretest@gmail.com",
                Password = "P0o9i8u7"
            };
            smtpClient.EnableSsl = true;

            // send the email
            smtpClient.Send(mailMessage);
        }
    }
}