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
    public partial class ProjectDetails : BasePage
    {
        public DonationProject model { get; set; }
        public string YoutubeUrl { get; set; }
        public decimal Percent;
        public int leaveDays;
        public string ProjectId
        {
            set { ViewState["ProjectId"] = value; }
            get { return ViewState["ProjectId"] == null ? string.Empty : ViewState["ProjectId"].ToString(); }
        }
        public string WriteBlog
        {
            set { ViewState["WriteBlog"] = value; }
            get { return ViewState["WriteBlog"] == null ? string.Empty : ViewState["WriteBlog"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string projectId = Request.QueryString["pid"];
                ProjectId = projectId;
                if (!string.IsNullOrEmpty(projectId))
                {
                    int pid = int.Parse(projectId);
                    SettingProject(pid);
                    SettingWriteBlog();
                }
                else
                {
                    Response.Redirect("Index.aspx");
                }
            }
        }

        private void SettingProject(int pid)
        {
            IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
            model = iProjectSec.GetModelById(pid);
            if (model != null && model.IsFinished != true)
            {
                hyperLink.NavigateUrl = model.Homepage.Contains("http://") ? model.Homepage : "http://" + model.Homepage;
                hyperLink.Text = model.Homepage;
                leaveDays = this.GetProjectLeaveDays(model.EndDate);
                Percent = this.GetPercent(model.TotalFunding, model.ReceivedFunding);
                try
                {
                    this.YoutubeUrl = model.YoutubeUrl.Contains("?v=") ? model.YoutubeUrl.Split('=')[1] : model.YoutubeUrl.Contains("youtu.be/") ? model.YoutubeUrl.Substring(model.YoutubeUrl.LastIndexOf('/')) : string.Empty;
                }
                catch
                {
                    this.YoutubeUrl = string.Empty;
                }

                IGoodyService iGoodySec = GetBusinessInterface<IGoodyService>();
                this.RepeaterGoodys.DataSource = iGoodySec.GetGoodysByProjectId(pid);
                this.RepeaterGoodys.DataBind();

                IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                this.RepBlog.DataSource = iBlogSec.GetBlogsByProjectId(pid).Take(4).ToList();
                this.RepBlog.DataBind();
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        private void SettingWriteBlog()
        {
            if (Session[SysConfigConst.FrontLoginUser] != null)
            {
                User userModel = Session[SysConfigConst.FrontLoginUser] as User;
                if (model.UserId == userModel.UserId)
                {
                    WriteBlog = "Write A Blog";
                }
            }
        } 
    }
}