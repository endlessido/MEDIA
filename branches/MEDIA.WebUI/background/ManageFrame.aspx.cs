using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.Model;

namespace MEDIA.WebUI.background
{
    public partial class WebForm1 : BasePage
    {
        public string AdminName
        {
            set { ViewState["AdminName"] = value; }
            get { return ViewState["AdminName"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Administrator entity = GetSession(SysConfigConst.BGLoginUser) as Administrator;
            if (entity != null)
            {
                AdminName = entity.AdminName;
            }
        }
    }
}