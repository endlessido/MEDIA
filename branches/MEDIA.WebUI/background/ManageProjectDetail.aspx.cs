using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Model;
using System.Text;
using MEDIA.BLL.Entity;
using MEDIA.IDAL;

namespace MEDIA.WebUI.background
{
    public partial class ManageProjectDetail : BasePage
    {
        public DonationProject model
        {
            get { return ViewState["projectmodel"] as DonationProject; }
            set { ViewState["projectmodel"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                string pid = Request.QueryString["pid"];
                if (!string.IsNullOrEmpty(pid))
                {
                    int nProjectId;
                    if (int.TryParse(pid, out nProjectId))
                    {
                        IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
                        model = iProjectSec.GetModelById(nProjectId);
                        try
                        {
                            this.YoutubeUrl = model.YoutubeUrl.Contains("?v=") ? model.YoutubeUrl.Split('=')[1] : string.Empty;
                        }
                        catch
                        {
                            this.YoutubeUrl = string.Empty;
                        }
                        if (model.IsCheck == false)
                        {
                            this.btnApprove.Text = "Approve";
                            this.btnApprove.CommandArgument = "Approve";
                        }
                        else
                        {
                            this.btnApprove.Text = "Finish";
                            this.btnApprove.CommandArgument = "Finish";
                        }
                        this.ProjectImg.ImageUrl = GetProjectImage(model.ImgUrl);

                        IGoodyService iGoodySec = GetBusinessInterface<IGoodyService>();
                        this.RepGoody.DataSource = iGoodySec.GetGoodysByProjectId(nProjectId);
                        this.RepGoody.ItemDataBound += new RepeaterItemEventHandler(RepGoody_ItemDataBound);
                        this.RepGoody.DataBind();

                        IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();
                        this.RepOrder.DataSource = iOrderSec.GetOrdersbyProjectId(nProjectId);
                        this.RepOrder.DataBind();
                    }
                }
            }
        }

        void RepGoody_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           Repeater repeater = e.Item.FindControl("RepUser") as Repeater;
           IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
           repeater.DataSource = iRecordSec.GetDonatorByGoodieId((e.Item.DataItem as Goody).Id);
           repeater.DataBind();
        }

        protected int GetDonatorsCount(object gId)
        {
            IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
            List<DonatedRecordBusiEntity> userList = iRecordSec.GetDonatorByGoodieId((int)gId);
            return userList == null ? 0 : userList.Count;
        }

        protected void BtnCheck_Command(object sender, CommandEventArgs e)
        {
            string pid = Request.QueryString["pid"];
            if (!string.IsNullOrEmpty(pid))
            {
                IDonationProjectRepository iProjectRepos = GetBusinessInterface<IDonationProjectRepository>();
                int projectId = int.Parse(pid);
                DonationProject model = iProjectRepos.GetSingle(m => m.ProjectId == projectId);
                switch (e.CommandArgument.ToString())
                {
                    case "Approve":
                        model.IsCheck = true;
                        break;
                    case "Finish":
                        model.IsFinished = true;
                        break;
                }
                iProjectRepos.SaveChanges();
                Response.Redirect("ManageIndex.aspx");
            }
        }

        public string YoutubeUrl { get; set; }
    }
}