using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI
{
    public partial class ForgetPassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            IUserService iUserSec = GetBusinessInterface<IUserService>();
            Model.User model = iUserSec.GetModelByEmail(this.txtEmail.Text);
            if (model != null)
            {
                string[] emailConfig = base.GetEmailConfig(SysConfigConst.ForgetPwdEmail);
                EmailMessage emailMsg = new EmailMessage();
                emailMsg.To = model.EmailAddress;
                emailMsg.Subject = emailConfig[0];
                emailMsg.Body = string.Format(emailConfig[1], model.FirstName, EncryptUtil.Decrypt(model.LoginPwd), this.IndexPageUrl);
                emailMsg.SendCompleted += new EmailMessage.SendResultCallBackHandler(emailMsg_SendCompleted);
                emailMsg.Send();  
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_emailwrong + "');</script>");
            }
        }

        void emailMsg_SendCompleted(object sender, EmailMessage.SendResult e)
        {
            switch (e)
            {
                case EmailMessage.SendResult.Success:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_checkpwd + "','UserLogin.aspx');</script>");
                    break;
                case EmailMessage.SendResult.Error:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_tryagain + "');</script>");
                    break;
                case EmailMessage.SendResult.Cancled:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_servercancled + "');</script>");
                    break;
            }
        }
    }
}