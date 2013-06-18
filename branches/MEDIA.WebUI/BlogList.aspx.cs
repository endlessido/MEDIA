using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;

namespace MEDIA.WebUI
{
    public partial class BlogList : BasePage
    {
        public string pid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                pid = Request.QueryString["pid"];
                string curpageindex = Request.QueryString["curpageindex"];
                if (!string.IsNullOrEmpty(curpageindex))
                {
                    this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                }
                int id = 0;
                if (int.TryParse(pid, out id))
                {
                    IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                    IDonationProjectService iProcetSec = GetBusinessInterface<IDonationProjectService>();
                    Model.DonationProject project = iProcetSec.GetModelById(id);
                    this.CustomPager1.SearchCondition.Add("pid", pid);
                    this.lblProjectName.Text = project.ProjectName;
                    this.lblProjectNeed.Text = project.Need;
                    this.lblProjectSummary.Text = project.ProjectSummary;
                    this.lblUserName.Text = project.User.FirstName + "'s Blog";
                    List<Model.Blog> list = iBlogSec.GetBlogsByProjectId(id);
                    if (list != null)
                    {
                        this.CustomPager1.SetPageCount(list.Count, base.FrontPageSize); 
                        this.RepBlog.DataSource = list.Skip((this.CustomPager1.CurrentPageIndex - 1) * FrontPageSize).Take(FrontPageSize);
                        this.RepBlog.DataBind();
                    }
                }
            }
        }
    }
}