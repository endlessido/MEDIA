using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;

namespace MEDIA.WebUI.background
{
    public partial class UserDetail : BasePage
    {
        public Model.User model;
        protected void Page_Load(object sender, EventArgs e)
        {
            IUserService iUserSec = GetBusinessInterface<IUserService>();
            string uid = Request.QueryString["uid"];
            if (!string.IsNullOrEmpty(uid))
            {
              model = iUserSec.GetModelById(int.Parse(uid));
            }
        }
    }
}