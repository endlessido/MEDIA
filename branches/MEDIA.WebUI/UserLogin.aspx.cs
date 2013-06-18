using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Model;


namespace MEDIA.WebUI
{
    public partial class UserLogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtEmail.Text = base.GetUserNameFromCookie(this.Request);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = this.txtEmail.Text;
            string userPwd = this.txtPwd.Text;
            string returnPath = Request.QueryString["url"];
            if (string.IsNullOrEmpty(returnPath))
            {
                base.UserLogin(userName, userPwd, "Index.aspx",this.chkRemember.Checked);
            }
            else
            {
                base.UserLogin(userName, userPwd, returnPath, this.chkRemember.Checked);
            }  
        }
    }
}