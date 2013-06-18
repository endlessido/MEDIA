using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;

namespace MEDIA.WebUI
{
    public partial class BlogDetails : BasePage
    {
        public string pid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string blogId = Request.QueryString["bid"];
                IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                int bid = 0;
                if (int.TryParse(blogId, out bid))
                {
                  Model.Blog model =  iBlogSec.GetModel(bid);
                  InitBlogModel(model);
                }
            }
        }

        private void InitBlogModel(Model.Blog model)
        {
            if (model != null)
            {
                this.lblblogContent.Text = Server.HtmlDecode(model.Content);
                this.lblBlogDate.Text = model.CreateDate.ToString();
                this.lblProjectName.Text = model.DonationProject.ProjectName;
                this.lblProjectNeed.Text = model.DonationProject.Need;
                this.lblProjectSummary.Text = model.DonationProject.ProjectSummary;
                this.lblTitle.Text = model.Title;
                this.lblUserName.Text = model.DonationProject.User.FirstName;
                if (string.IsNullOrEmpty(model.ImgUrl))
                {
                    this.imgblog.Visible = false;
                }
                else
                {
                    this.imgblog.ImageUrl = base.ImgVirtualPath + model.ImgUrl;
                }
                pid = model.ProjectId.ToString();
            }
        }
    }
}