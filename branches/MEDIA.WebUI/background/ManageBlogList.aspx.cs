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
    public partial class ManageBlogList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                List<Blog> blogList = iBlogSec.UnCheckBlogs();
                if (blogList != null)
                {
                    this.CustomPager1.SetPageCount(blogList.Count, base.BackgroundPageSize);
                    string curpageindex = Request.QueryString["curpageindex"];
                    if (!string.IsNullOrEmpty(curpageindex))
                    {
                        this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                    }
                    this.RepeaterBlogs.DataSource = blogList.Skip((this.CustomPager1.CurrentPageIndex - 1) * BackgroundPageSize).Take(BackgroundPageSize);
                    this.RepeaterBlogs.DataBind();
                }
            }
        }
    }
}