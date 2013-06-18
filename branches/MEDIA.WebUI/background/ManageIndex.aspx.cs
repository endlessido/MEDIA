using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.Model;
using MEDIA.IBLL;

namespace MEDIA.WebUI.background
{
    public partial class ManageIndex : BasePage
    {
       
        public int NewlyProjectCount
        {
            set { ViewState["NewlyProjectCount"] = value; }
            get { return (int)ViewState["NewlyProjectCount"]; }
        }
        public int GoingProjectCount
        {
            set { ViewState["GoingProjectCount"] = value; }
            get { return (int)ViewState["GoingProjectCount"]; }
        }
        public int EndProjectCount
        {
            set { ViewState["EndProjectCount"] = value; }
            get { return (int)ViewState["EndProjectCount"]; }
        }
        public int TotalUserCount
        {
            set { ViewState["TotalUserCount"] = value; }
            get { return (int)ViewState["TotalUserCount"]; }
        }
        public int NewlyUserCount
        {
            set { ViewState["NewlyUserCount"] = value; }
            get { return (int)ViewState["NewlyUserCount"]; }
        }
        public int GoodyDonatedCount
        {
            set { ViewState["GoodyDonatedCount"] = value; }
            get { return (int)ViewState["GoodyDonatedCount"]; }
        }
        public int PopularGoodyCount
        {
            set { ViewState["PopularGoodyCount"] = value; }
            get { return (int)ViewState["PopularGoodyCount"]; }
        }
        public int NewlyBlogs
        {
            set { ViewState["NewlyBlogs"] = value; }
            get { return ViewState["NewlyBlogs"] == null ? 0 : (int)ViewState["NewlyBlogs"]; }
        }
        public int UnPaymentCount
        {
            set { ViewState["UnPaymentCount"] = value; }
            get { return ViewState["UnPaymentCount"] == null ? 0 : (int)ViewState["UnPaymentCount"]; }
        }
        public int CompletedCount
        {
            set { ViewState["CompletedCount"] = value; }
            get { return ViewState["CompletedCount"] == null ? 0 : (int)ViewState["CompletedCount"]; }
        }
        public string AdminName
        {
            set { ViewState["AdminName"] = value; }
            get { return ViewState["AdminName"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Administrator entity = GetSession(SysConfigConst.BGLoginUser) as Administrator;
            if (entity != null)
            {
                AdminName = entity.AdminName;
            }
            if (!Page.IsPostBack)
            {
              
                IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
                IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
                IBlogService iBlogSec = GetBusinessInterface<IBlogService>();
                IUserService iUserSec = GetBusinessInterface<IUserService>();
                IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();

                List<Goody> goodyList = iRecordSec.GetGoodyDonated();
                this.RepGoodies.DataSource = goodyList.Take(base.BackgroundPageSize);
                this.RepGoodies.DataBind();

                List<DonationProject> projectList = iProjectSec.GetNewlyProject();
                if (projectList != null)
                {
                    this.RepeaterProject.DataSource = projectList.Take(base.BackgroundPageSize);
                    this.RepeaterProject.DataBind();
                    NewlyProjectCount = projectList.Count;
                }

                List<User> userList = iUserSec.GetNewlyByCount();
                if (userList != null)
                {
                    this.RepeaterUser.DataSource = userList.Take(base.BackgroundPageSize);
                    this.RepeaterUser.DataBind();
                    NewlyUserCount = userList.Count;
                }

                List<Blog> blogList = iBlogSec.UnCheckBlogs();
                if (blogList != null)
                {
                    this.RepeaterBlogs.DataSource = blogList.Take(base.BackgroundPageSize);
                    this.RepeaterBlogs.DataBind();
                    NewlyBlogs = blogList.Count;
                }

                this.RepOrder.DataSource = iOrderSec.GetUnPaymentOrders().Take(base.BackgroundPageSize);
                this.RepOrder.DataBind();

                GoodyDonatedCount = iRecordSec.GetGoodyDonatedCount();
                PopularGoodyCount = goodyList == null ? 0 : goodyList.Count;
                GoingProjectCount = iProjectSec.GetGoingCount();
                EndProjectCount = iProjectSec.GetEndedCount();
                TotalUserCount = iUserSec.GetTotalCount();
                UnPaymentCount = iOrderSec.UnPaymentOrdersCount();
                CompletedCount = iOrderSec.CompletedOrdersCount();
            }
        }
    }
}