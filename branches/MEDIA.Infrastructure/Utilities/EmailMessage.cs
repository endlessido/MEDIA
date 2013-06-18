using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace MEDIA.Infrastructure.Utilities
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public enum SendResult
        {
            Success,Cancled,Error
        }

        public delegate void SendResultCallBackHandler(object sender, SendResult e);

        public event SendResultCallBackHandler SendCompleted;

        private MailMessage SetEmailContent()
        {
            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["EmailAccount"]);
            objMailMessage.To.Add(To);
            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;
            objMailMessage.BodyEncoding = Encoding.UTF8;
            objMailMessage.Priority = MailPriority.High;
            return objMailMessage;
        }

        public void Send()
        {
            MailMessage mail = SetEmailContent();
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAccount"], ConfigurationManager.AppSettings["Password"]);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Port"]))
            {
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            }
            smtp.Host = ConfigurationManager.AppSettings["SMTP"];
            smtp.EnableSsl = false;
            smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
            smtp.SendAsync(mail, mail);
        }

        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MailMessage mailMsg = (MailMessage)e.UserState;     
            string subject = mailMsg.Subject;
            SendResult enumResult = new SendResult();
            if (e.Cancelled)   
            {
                enumResult = SendResult.Cancled;  
            }    
            if (e.Error != null)    
            {
                enumResult = SendResult.Error;  
            }    
            else    
            {
                enumResult = SendResult.Success;
            }
            SendCompleted(sender, enumResult);
        }
    }
}
