using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Reflection;
using MEDIA.Infrastructure.Utilities;
using System.Security.Cryptography;
using System.Text;
using MEDIA.IBLL;

namespace MEDIA.WebUI
{
    public partial class PayResponse : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderID = Request["orderID"];
            string currency = Request["currency"];
            string amount = Request["amount"];
            string PM = Request["PM"];
            string ACCEPTANCE = Request["ACCEPTANCE"];
            string STATUS = Request["STATUS"];
            string CARDNO = Request["CARDNO"];
            string ED = Request["ED"];
            string CN = Request["CN"];
            string TRXDATE = Request["TRXDATE"];
            string PAYID = Request["PAYID"];
            string NCERROR = Request["NCERROR"];
            string BRAND = Request["BRAND"];
            string IP = Request["IP"];
            string SHASIGN = Request["SHASIGN"];

            if (!string.IsNullOrEmpty(STATUS) && STATUS == "9" && !string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(orderID))
            {
                IOrderInfoService iOrderSec = GetBusinessInterface<IOrderInfoService>();
                iOrderSec.ModifyPayment(orderID, PM);
                IDonationProjectService iProcjetSec = GetBusinessInterface<IDonationProjectService>();
                int? projectId;
                iProcjetSec.ReceiveFunding(decimal.Parse(amount), orderID, out projectId);
                Response.Redirect("http://www.supportyourlocalteam.net/ProjectDetails.aspx?pid=" + projectId);
            }
            //NewMethod(orderID, currency, amount, PM, ACCEPTANCE, CARDNO, ED, CN, TRXDATE, PAYID, NCERROR, BRAND, IP, SHASIGN);
        }

        private void NewMethod(string orderID, string currency, string amount, string PM, string ACCEPTANCE, string CARDNO, string ED, string CN, string TRXDATE, string PAYID, string NCERROR, string BRAND, string IP, string SHASIGN)
        {
            SHA1Managed sha = new SHA1Managed();
            StringBuilder str = new StringBuilder();
            str.Append(string.Format("ACCEPTANCE={0}{1}", ACCEPTANCE, base.SHAKEY));
            str.Append(string.Format("amount={0}{1}", amount, base.SHAKEY));
            str.Append(string.Format("BRAND={0}{1}", BRAND, base.SHAKEY));
            str.Append(string.Format("CARDNO={0}{1}", CARDNO, base.SHAKEY));
            str.Append(string.Format("CN={0}{1}", CN, base.SHAKEY));
            str.Append(string.Format("currency={0}{1}", currency, base.SHAKEY));
            str.Append(string.Format("ED={0}{1}", ED, base.SHAKEY));
            str.Append(string.Format("IP={0}{1}", IP, base.SHAKEY));
            str.Append(string.Format("NCERROR={0}{1}", NCERROR, base.SHAKEY));
            str.Append(string.Format("orderID={0}{1}", orderID, base.SHAKEY));
            str.Append(string.Format("PAYID={0}{1}", PAYID, base.SHAKEY));
            str.Append(string.Format("PM={0}{1}", PM, base.SHAKEY));
            str.Append(string.Format("TRXDATE={0}{1}", TRXDATE, base.SHAKEY));


            byte[] result = Encoding.UTF8.GetBytes(str.ToString());
            sha.ComputeHash(result);
            StringBuilder displayString = new StringBuilder();
            for (int counter = 0; counter < sha.Hash.Length; counter++)
            {
                displayString.Append(sha.Hash[counter].ToString("x2").ToUpper());
            }
            if (SHASIGN == displayString.ToString())
            {
                Response.Redirect("http://www.supportyourlocalteam.net/index.aspx");
            }
            else
            {
                Response.Redirect("http://www.supportyourlocalteam.net/ProjectList.aspx");
            }
        }


    }
}