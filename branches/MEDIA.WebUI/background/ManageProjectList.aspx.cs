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
    public partial class ManageProjectList : BasePage
    {
        public string ProjectTitle
        {
            set { ViewState["ProjectTitle"] = value; }
            get { return ViewState["ProjectTitle"] == null ? string.Empty : ViewState["ProjectTitle"].ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);

            if (!Page.IsPostBack)
            {
                string title = Request.QueryString["type"];
                IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
                List<DonationProject> projectList = null;
                switch (title)
                {
                    case "newly":
                        ProjectTitle = "Newly Applied Projects";
                        projectList = iProjectSec.GetNewlyProject();
                        break;
                    case "ongoing":
                        ProjectTitle = "Ongoing Projects";
                        projectList = iProjectSec.GetOnGoingProject();
                        break;
                    case "ended":
                        ProjectTitle = "Ended Projects";
                        projectList = iProjectSec.GetEndingProject();
                        break;
                    default:
                        ProjectTitle = "Newly Applied Projects";
                        projectList = iProjectSec.GetNewlyProject();
                        break;
                }

                if (projectList != null)
                {
                    this.CustomPager1.SetPageCount(projectList.Count, base.BackgroundPageSize);
                    this.CustomPager1.SearchCondition.Add("type", title);
                    string curpageindex = Request.QueryString["curpageindex"];
                    if (!string.IsNullOrEmpty(curpageindex))
                    {
                        this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                    }
                    this.RepeaterProject.DataSource = projectList.Skip((this.CustomPager1.CurrentPageIndex - 1) * BackgroundPageSize).Take(BackgroundPageSize);
                    this.RepeaterProject.DataBind();
                }
            }
        }
    }
}