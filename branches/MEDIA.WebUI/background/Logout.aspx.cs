using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEDIA.WebUI.background
{
    public partial class Logout :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetSession(SysConfigConst.BGLoginUser, null);
            Response.Redirect("ManageLogin.aspx");
        }
    }
}