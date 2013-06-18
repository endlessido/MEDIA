using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Model;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI
{
    public partial class Donate :BasePage
    {
        public DonationProject model { get; set; }
        public int ProjectId 
        { 
            get 
            {
                if (!String.IsNullOrEmpty(Request.QueryString["pid"]))
                {
                    return int.Parse(Request.QueryString["pid"]);
                }
                else
                {
                    Response.Redirect("Index.aspx");
                    return 0;
                }
            }
        }
        public bool isLogin { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
            isLogin = Session[SysConfigConst.FrontLoginUser] == null ? false : true;
            model = iProjectSec.GetModelById(ProjectId);
            if (model != null && model.IsFinished != true)
            {
                if (!Page.IsPostBack)
                {
                    leaveDays = this.GetProjectLeaveDays(model.EndDate);
                    Percent = this.GetPercent(model.TotalFunding, model.ReceivedFunding);
                    IGoodyService iGoodySec = GetBusinessInterface<IGoodyService>();
                    List<Goody> list = iGoodySec.GetGoodysByProjectId(ProjectId);
                    this.RepeaterGoodys.DataSource = list;
                    this.RepeaterGoodys.DataBind();

                    this.RepeaterShowGoody.DataSource = list;
                    this.RepeaterShowGoody.DataBind();
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnDonate_Click(object sender, EventArgs e)
        {
            string goodyIds = Request.Form["goodySelect"];
            string ck_abondon = Request.Form["ck_abondon"];
            string donatemoney = Request.Form["donatemoney"]; 
            string orderid = CreateOrderID();
            if (ck_abondon == "on")
            {
                AnonymousDonate(goodyIds,donatemoney, orderid);
            }
            else
            {
                OnlineDonate(goodyIds, donatemoney, orderid);
            }
            Response.Redirect("Payment.aspx?orderid=" + orderid);
        }

        private void OnlineDonate(string goodyIds, string donatemoney, string orderid)
        {
            IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();
            decimal? SumPrice;
            SumPrice = GetSumPrice(goodyIds, donatemoney);
            // 非匿名捐赠
            User model_user = GetSession(SysConfigConst.FrontLoginUser) as User;
            iOrderSec.Add(new OrderInfo
            {
                OrderId = orderid,
                IsPayment = false,
                CurrencyStr = model.CurrencyStr,
                OrderDate = System.DateTime.Now,
                TotalPrice = SumPrice,
                PostCode = model_user.Zip,
                ReceiverName = string.Format("{0} {1}", model_user.FirstName, model_user.LastName),
                ReceiverAddress = model_user.Address,
                UserId = model_user.UserId
            });
            RecordDonate(goodyIds, orderid, SumPrice);
        }

        private void RecordDonate(string goodyIds, string orderid, decimal? SumPrice)
        {
            if (!string.IsNullOrEmpty(goodyIds))
            {
                IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
                iProjectSec.ReceiveFunding(goodyIds, this.ProjectId, 0, orderid);
            }
            else
            {
                IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
                iRecordSec.Add(new DonateRecord
                {
                    ProjectId = this.ProjectId,
                    OrderId = orderid,
                    IsPayment = false,
                    DonateDate = System.DateTime.Now,
                    DonateFunding = SumPrice,
                    CurrencyStr = model.CurrencyStr
                });
            }
        }

        private decimal? GetSumPrice(string goodyIds, string donatemoney)
        {
            IGoodyService iGoodySec = GetBusinessInterface<IGoodyService>();
            if (!string.IsNullOrEmpty(donatemoney))
            {
                return decimal.Parse(donatemoney);
            }
            else
            {
                return iGoodySec.GetSumPrice(goodyIds);
            }
        }

        private void AnonymousDonate(string goodyIds, string donatemoney, string orderid)
        {
            IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();
            decimal? SumPrice;
            // 匿名捐赠
            SumPrice = GetSumPrice(goodyIds, donatemoney);
            iOrderSec.Add(new OrderInfo
            {
                OrderId = orderid,
                IsPayment = false,
                CurrencyStr = model.CurrencyStr,
                OrderDate = System.DateTime.Now,
                TotalPrice = SumPrice,
                ReceiverName = "Anonymous"
            });
            RecordDonate(goodyIds, orderid, SumPrice);
        }

        public int leaveDays 
        {
            get { return (int)ViewState["leaveDays"]; }
            set { ViewState["leaveDays"] = value; }
        }

        public int Percent
        {
            get { return (int)ViewState["Percent"]; }
            set { ViewState["Percent"] = value; }
        }
    }
}