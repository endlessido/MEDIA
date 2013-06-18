using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Model;
namespace MEDIA.WebUI.background
{
    public partial class ManageLogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtAdminName.Value = base.GetUserNameFromCookie(this.Request, true);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           IAdminService iadminSec = GetBusinessInterface<IAdminService>();
           Administrator entity = iadminSec.Login(this.txtAdminName.Value, this.txtAdminPwd.Value);
           if (entity != null)
           {
               if (this.CKRemeber.Checked)
               {
                   base.RememberLogin(true, this.txtAdminName.Value, this.Response, true);
               }
               SetSession(SysConfigConst.BGLoginUser,entity);
               Response.Redirect("ManageFrame.aspx");
           }
           else
           {
               Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('user name or pwd is wrong!')</script>");
           }
        }
    }
}