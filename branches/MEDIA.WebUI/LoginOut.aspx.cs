using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEDIA.WebUI
{
    public partial class LoginOut :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetSession(SysConfigConst.FrontLoginUser, null);
            Response.Redirect("Index.aspx");
        }
    }
}