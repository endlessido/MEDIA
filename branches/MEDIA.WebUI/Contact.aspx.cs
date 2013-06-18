using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI
{
    public partial class Contact : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string userName = Request.Form["username"];
            string emailAddr = Request.Form["emailaddr"];
            string subject = Request.Form["subject"];
            string content = Request.Form["content"];

            EmailMessage emailMsg = new EmailMessage();
            emailMsg.To = base.ContactUsEmail;
            emailMsg.Subject = subject;
            emailMsg.Body = string.Format("customer name :{0} n/ customer e-mail :{1} /n leave message:{2}", userName, emailAddr, content);
            emailMsg.SendCompleted += new EmailMessage.SendResultCallBackHandler(emailMsg_SendCompleted);
            emailMsg.Send();
        }

        void emailMsg_SendCompleted(object sender, EmailMessage.SendResult e)
        {
            switch (e)
            {
                case EmailMessage.SendResult.Success:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','"+Resources.Resource.Alter_feedback+"');</script>");
                    break;
                case EmailMessage.SendResult.Error:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','"+Resources.Resource.Alter_tryagain+"');</script>");
                    break;
                case EmailMessage.SendResult.Cancled:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_servercancled + "');</script>");
                    break;
            }
        }
    }
}