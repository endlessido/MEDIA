using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;

namespace MEDIA.WebUI
{
    public partial class ActiveAccount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uId = Request.QueryString["id"];
            string activeKey = Request.QueryString["ak"];

            if (!string.IsNullOrEmpty(uId) && !string.IsNullOrEmpty(activeKey))
            {
                IUserService iUserSec = GetBusinessInterface<IUserService>();
                int userId = int.Parse(uId);
                Model.User model = iUserSec.GetModelById(userId);
                this.lblUserName.Text = model.LastName;
                iUserSec.Active(userId, Guid.Parse(activeKey));
            }
        }
    }
}