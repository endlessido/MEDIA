using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using System.Data.Objects.DataClasses;
using MEDIA.Model;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI
{
    public partial class Index :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextEmail.Value = base.GetUserNameFromCookie(this.Request);
                MEDIA.Model.User model = Session[SysConfigConst.FrontLoginUser] as MEDIA.Model.User;
                if (model != null)
                {
                    UserName = string.Format("{0}, {1}!", Resources.Resource.Welcome, model.FirstName);
                    isLogin = true;
                }
                IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
                this.RepeaterProject.DataSource = iProjectSec.GetRecentRandomProject(base.ProjectOfDayCount);
                this.RepeaterProject.DataBind();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = this.TextEmail.Value;
            string userPwd = this.TextPwd.Value;
            UserLogin(userName, userPwd, "Index.aspx", chkIndexRemember.Checked);
        }

       

        protected string GetGoodiesTitle(object goody)
        {
            List<Goody> goodies = (goody as EntityCollection<Goody>).ToList();
            string result = string.Empty;
            foreach (var item in goodies)
            {
                result += item.Title + "&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            return result;
        }

        public bool isLogin { get; set; }

        public string UserName { get; set; }
    }
}