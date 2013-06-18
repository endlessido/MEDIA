using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.BLL.Entity;

namespace MEDIA.WebUI.background
{
    public partial class ManageOrderList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                string type = Request.QueryString["type"];
                string sort = Request.QueryString["sort"];
                if (!string.IsNullOrEmpty(type))
                {
                    this.CustomPager1.SearchCondition.Add("type", type);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    this.CustomPager1.SearchCondition.Add("sort", sort);
                }
                NewMethod(type, sort);
            }
        }

        private void NewMethod(string type, string sort)
        {
            IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();
            List<OrderBusiEntity> userList = null;
            if (!string.IsNullOrEmpty(sort))
            {
                this.LinkButton1.CssClass = "";
                this.LinkButton2.CssClass = "";
                this.LinkButton3.CssClass = "";
                if (LinkButton1.CommandArgument == sort)
                {
                    LinkButton1.CssClass = "fg-color-orangeDark";
                }
                if (LinkButton2.CommandArgument == sort)
                {
                    LinkButton2.CssClass = "fg-color-orangeDark";
                }
                if (LinkButton3.CommandArgument == sort)
                {
                    LinkButton3.CssClass = "fg-color-orangeDark";
                }
                if (LinkButton4.CommandArgument == sort)
                {
                    LinkButton4.CssClass = "fg-color-orangeDark";
                }
            }

            switch (type)
            {
                case "newly":
                    userList = iOrderSec.GetUnPaymentOrders();
                    lblTitle.Text = "Newly Orders";
                    break;
                case "completed":
                    userList = iOrderSec.GetCompletedOrders();
                    lblTitle.Text = "Completed Orders";
                    break;
                default:
                    userList = iOrderSec.GetUnPaymentOrders();
                    lblTitle.Text = "Newly Orders";
                    break;
            }

            switch (sort)
            {
                case "OrderDate":
                    userList = userList.OrderByDescending(m => m.OrderDate).ToList();
                    break;
                case "Address":
                    userList = userList.OrderByDescending(m => m.Address).ToList();
                    break;
                case "Quantity":
                    userList = userList.OrderByDescending(m => m.Quantity).ToList();
                    break;
                case "ProjecName":
                    userList = userList.OrderByDescending(m => m.ProjecName).ToList();
                    break;
            }

            if (userList != null)
            {
                this.CustomPager1.SetPageCount(userList.Count, base.BackgroundPageSize);
                string curpageindex = Request.QueryString["curpageindex"];
                if (!string.IsNullOrEmpty(curpageindex))
                {
                    this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                }
                this.RepOrder.DataSource = userList.Skip((this.CustomPager1.CurrentPageIndex - 1) * BackgroundPageSize).Take(BackgroundPageSize);
                this.RepOrder.DataBind();
            }
        }

        protected void SortBy_Command(object sender, CommandEventArgs e)
        {
            this.CustomPager1.CurrentPageIndex = 1;
            string type = Request.QueryString["type"];
            this.CustomPager1.SearchCondition.Add("type", type);
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                this.LinkButton1.CssClass = "";
                this.LinkButton2.CssClass = "";
                this.LinkButton3.CssClass = "";
                this.LinkButton4.CssClass = "";
                btn.CssClass = "fg-color-orangeDark";
                this.CustomPager1.SearchCondition.Add("sort", btn.CommandArgument);
            }
            NewMethod(type, btn.CommandArgument);
        }
    }
}