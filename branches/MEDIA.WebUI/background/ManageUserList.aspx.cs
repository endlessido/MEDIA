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
    public partial class ManageUserList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                IUserService iUserSec = GetBusinessInterface<IUserService>();
                List<User> userList = null; 
                string type = Request.QueryString["type"];
                switch (type)
                {
                    case "newly":
                        userList = iUserSec.GetNewlyByCount();
                        this.lblTitle.Text = "Newly Registed Users";
                        break;
                    case "all":
                        userList = iUserSec.GetAll();
                        this.lblTitle.Text = "All Users";
                        break;
                    default:
                        userList = iUserSec.GetAll();
                        break;
                }

                this.CustomPager1.SearchCondition.Add("type", type);
                if (userList != null)
                {
                    this.CustomPager1.SetPageCount(userList.Count, base.BackgroundPageSize);
                    string curpageindex = Request.QueryString["curpageindex"];
                    if (!string.IsNullOrEmpty(curpageindex))
                    {
                        this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                    }
                    this.RepeaterUser.DataSource = userList.Skip((this.CustomPager1.CurrentPageIndex - 1) * BackgroundPageSize).Take(BackgroundPageSize);
                    this.RepeaterUser.DataBind();
                }
            }
        }
    }
}