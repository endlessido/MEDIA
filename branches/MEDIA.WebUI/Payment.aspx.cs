using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using MEDIA.Model;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI
{
    public partial class WebForm1 :BasePage
    {
        public decimal? SumPrice =0;
        public string CurStr;
        public string PSPID;
        public string ORDERID;
        public string LANGUAGE;
        public string CN;
        public string EMAIL;
        public string OWNERZIP;
        public string OWNERADDRESS;
        public string OWNERTOWN;
        public string SHASIGN;
        public string AMOUNT;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string PreviousURL = Request.ServerVariables["HTTP_REFERER"] == null ? string.Empty : Request.ServerVariables["HTTP_REFERER"].ToString();
                string orderId = Request.QueryString["orderid"];
                if (string.IsNullOrEmpty(orderId))
                {
                    Response.Redirect(PreviousURL);
                    return;
                }
                else
                {
                    IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
                    IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();
                    ORDERID = orderId;
                    OrderInfo orderModel = iOrderSec.GetModel(ORDERID);


                    this.RepGoodies.DataSource = iRecordSec.GetGoodybyOrderId(ORDERID);
                    this.RepGoodies.DataBind();

                    CurStr = orderModel.CurrencyStr;
                    PSPID = "supportyourlocals";
                    LANGUAGE = Request.Cookies["my_Language"] == null ? "en_US" : Request.Cookies["my_Language"].Value;
                    CN = orderModel.ReceiverName;
                    EMAIL = orderModel.User == null ? string.Empty : orderModel.User.EmailAddress;
                    OWNERZIP = orderModel.User == null ? string.Empty : orderModel.User.Zip;
                    OWNERADDRESS = orderModel.User == null ? string.Empty : orderModel.User.Address;
                    OWNERTOWN = orderModel.User == null ? string.Empty : orderModel.User.Town;
                    SumPrice = orderModel.TotalPrice;
                    SHASIGN = SetSHAString();
                }
            }
        }

        private string SetSHAString()
        {
            int indexOf = SumPrice.ToString().IndexOf('.');
            if (indexOf != -1)
            {
                AMOUNT = SumPrice.ToString().Remove(indexOf, 1) + "00";
            }
            else
            {
                AMOUNT = SumPrice.ToString() + "00";
            }
            SHA1Managed sha = new SHA1Managed();
            StringBuilder str = new StringBuilder();
            str.Append("ACCEPTURL=http://www.supportyourlocalteam.net/PayResponse.aspx" +base.SHAKEY);
            str.Append("AMOUNT=" + AMOUNT + base.SHAKEY);
            str.Append("BACKURL=http://www.supportyourlocalteam.net/index.aspx" + base.SHAKEY);
            str.Append("CANCELURL=http://www.supportyourlocalteam.net/PayResponse.aspx" + base.SHAKEY);
            str.Append("CN=" + CN + base.SHAKEY);
            str.Append("CURRENCY=" + CurStr + base.SHAKEY);
            str.Append("DECLINEURL=http://www.supportyourlocalteam.net/PayResponse.aspx" + base.SHAKEY);
            if (!string.IsNullOrEmpty(EMAIL))
            {
                str.Append("EMAIL=" + EMAIL + base.SHAKEY);
            }
            str.Append("EXCEPTIONURL=http://www.supportyourlocalteam.net/PayResponse.aspx" + base.SHAKEY);
            str.Append("LANGUAGE=" + LANGUAGE + base.SHAKEY);
            str.Append("ORDERID=" + ORDERID + base.SHAKEY);
            if (!string.IsNullOrEmpty(OWNERADDRESS))
            {
                str.Append("OWNERADDRESS=" + OWNERADDRESS + base.SHAKEY);
            }
            if (!string.IsNullOrEmpty(OWNERTOWN))
            {
                str.Append("OWNERTOWN=" + OWNERTOWN + base.SHAKEY);
            }
            if (!string.IsNullOrEmpty(OWNERZIP))
            {
                str.Append("OWNERZIP=" + OWNERZIP + base.SHAKEY);
            }
            str.Append("PSPID=supportyourlocals" + base.SHAKEY);

            byte[] result = Encoding.UTF8.GetBytes(str.ToString());
            sha.ComputeHash(result);
            StringBuilder displayString = new StringBuilder();
            for (int counter = 0; counter < sha.Hash.Length; counter++)
            {
                displayString.Append(sha.Hash[counter].ToString("x2").ToUpper());
            }
            return displayString.ToString();
        }
    }
}