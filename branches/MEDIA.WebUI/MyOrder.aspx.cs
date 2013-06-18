using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI
{
    public partial class MyOrder :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            Model.User model = GetSession(SysConfigConst.FrontLoginUser) as Model.User;
            IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();

            switch(type)
            {
                case "0":
                   this.lblTitle.Text = "Unpayed Orders";
                   this.RepOrder.DataSource = iOrderSec.GetOrdersbyUserId(model.UserId).Where(m => m.IsPayment == false).ToList();
                   this.RepOrder.DataBind();
                    break;
                case "1":
                   this.lblTitle.Text = "Payed Orders";
                   this.RepOrder.DataSource = iOrderSec.GetOrdersbyUserId(model.UserId).Where(m => m.IsPayment == true).ToList();
                   this.RepOrder.DataBind();
                    break;
                case "2":
                    this.lblTitle.Text = "Received Orders";
                    this.RepOrder.DataSource = iOrderSec.GetReceivedOrders(model.UserId);
                    this.RepOrder.DataBind();
                    break;
            }
        }

        protected string Show()
        {
            if (Request.QueryString["type"] == "2")
            {
                return string.Format("<td class=\"text-left\" colspan=\"3\" class=\"tlr\"> <ul><li>FirstName: {0}</li><li>EmailAddress: {1}</li><li>Address: {2}</li><li>Zip: {3}</li><li>Town: {4}</li><li>CountryName: {5}</li><li>PayType: {6}</li></ul> </td>",
                    Eval("FirstName"), Eval("EmailAddress"), Eval("Address"), Eval("Zip"), Eval("Town"), Eval("CountryName"), Eval("PayType"));
            }
            else
            {
                if ((bool)Eval("IsPayment"))
                {
                    return "<td colspan=\"3\" class=\"tlr\">You have paid for the trade  </td>";
                }
                else
                {
                    return "<td colspan=\"3\" class=\"tlr\">You have not paid for the trade <a href=\"Payment.aspx?orderid=" + Eval("OrderId") + "\" class=\"button bg-color-greenDark fg-color-white\">Go to payment</a></td>";
                }
            }
        }
    }
}