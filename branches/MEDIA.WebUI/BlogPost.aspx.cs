using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MEDIA.IBLL;

namespace MEDIA.WebUI
{
    public partial class BlogPost : BasePage
    {
        public string pid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Request.QueryString["pid"];
            if (!Page.IsPostBack)
            {
                IDonationProjectService iProcetSec = GetBusinessInterface<IDonationProjectService>();
                Model.DonationProject project = iProcetSec.GetModelById(int.Parse(pid));
                this.lblProjectName.Text = project.ProjectName;
                this.lblProjectNeed.Text = project.Need;
                this.lblProjectSummary.Text = project.ProjectSummary;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                Model.Blog model = new Model.Blog
                {
                    Content = Server.HtmlEncode(this.pageblogedit.Value),
                    CreateDate = System.DateTime.Now,
                    ImgUrl = SaveUploadFile(),
                    IsCheck = false,
                    Title = this.txtTitle.Value,
                    ProjectId = int.Parse(pid)
                };
                IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                if (iBlogSec.Add(model))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>showDialog('Attion','create success','BlogDetails.aspx?bid=" + model .blogId+ "');</script>");
                }
            }
            catch (Exception exp)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>showDialog('Attion','create blg failed');</script>");
            }
        }

        private string SaveUploadFile()
        {
            try
            {
                if (this.uploadFile.PostedFile.ContentLength == 0)
                    return string.Empty;
                HttpPostedFile file = this.uploadFile.PostedFile;
                string path = this.ImgUploadPath;
                string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ".jpg");
                file.SaveAs(string.Format("{0}{1}", path, fileName));
                return fileName;
            }
            catch (Exception exp)
            {
                throw new Exception("Upload Image Failed", exp);
            }
        }
    }
}