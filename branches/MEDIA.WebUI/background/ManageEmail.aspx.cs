using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using log4net;
using System.Reflection;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI.background
{
    public partial class ManageEmail :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                string type = Request.QueryString["type"];
                string[] emailConfig = new string[2];
                switch (type)
                {
                    case "active":
                        emailConfig = base.GetEmailConfig(SysConfigConst.ActiveEmail);
                        this.lblMsg.Text = "{0} is Customer First Name; {1} is Customer Last Name; {2} is Email Active Url. All of them is required";
                        break;
                    case "forget":
                        emailConfig = base.GetEmailConfig(SysConfigConst.ForgetPwdEmail);
                        this.lblMsg.Text = "{0} is Customer First Name; {1} is Login Password; {2} is Login Page Url. All of them is required";
                        break;
                    default:
                        emailConfig = base.GetEmailConfig(SysConfigConst.ActiveEmail);
                        break;
                }
                this.txtTitle.Value = emailConfig[0];
                this.txtContent.Value = emailConfig[1];
            }
        }

 

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtContent.Value.Contains("{0}") && this.txtContent.Value.Contains("{1}") && this.txtContent.Value.Contains("{2}"))
            {
                string[] emailConfig = new string[2];
                emailConfig[0] = this.txtTitle.Value;
                emailConfig[1] = this.txtContent.Value;
                string type = Request.QueryString["type"];
                switch (type)
                {
                    case "active":
                        base.SetEmailConfig(SysConfigConst.ActiveEmail, emailConfig);
                        break;
                    case "forget":
                        base.SetEmailConfig(SysConfigConst.ForgetPwdEmail, emailConfig);
                        break;
                    default:
                        base.SetEmailConfig(SysConfigConst.ActiveEmail, emailConfig);
                        break;
                }
            }
        }
    }
}