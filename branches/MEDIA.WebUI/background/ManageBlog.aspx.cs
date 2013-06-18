using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;

namespace MEDIA.WebUI.background
{
    public partial class ManageBlog :BasePage
    {
        public Model.Blog model;
        protected void Page_Load(object sender, EventArgs e)
        {
            string bid = Request.QueryString["bid"];
            if (!string.IsNullOrEmpty(bid))
            {
                IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                model = iBlogSec.GetModel(int.Parse(bid));
                if (string.IsNullOrEmpty(model.ImgUrl))
                {
                    this.imgblog.Visible = false;
                }
                else
                {
                    this.imgblog.ImageUrl = base.ImgVirtualPath + model.ImgUrl;
                }
            }
        }
    }
}