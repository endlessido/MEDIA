using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using System.Globalization;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.WebUI.share
{
    public partial class Index : System.Web.UI.MasterPage
    {
        public bool isLogin = false;
        private BasePage basePage = new BasePage();
        public string PreviousURL = string.Empty;
        public string userName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            MEDIA.Model.User model = basePage.GetSession(SysConfigConst.FrontLoginUser) as MEDIA.Model.User;
            isLogin = model != null;
            userName = basePage.GetUserNameFromCookie(Request);
           
            if (!Page.IsPostBack)
            {
                
                PreviousURL = Request.ServerVariables["HTTP_REFERER"] == null ? string.Empty : Request.ServerVariables["HTTP_REFERER"].ToString();
                SetLanguageCookie();
                if (model != null)
                {
                    this.lblName.Text  = string.Format("{0}, {1}!", Resources.Resource.Welcome, model.FirstName);
                    IDonationProjectService iProjectSec = basePage.GetBusinessInterface<IDonationProjectService>();
                    int number1 = iProjectSec.GetFoundedProjectsByUserId(model.UserId).Count;
                    this.lblPublishProject.Text = number1.ToString();

                    IDonateRecordService iRecordSec = basePage.GetBusinessInterface<IDonateRecordService>();
                    int number2 = iRecordSec.GetDonationProsByUserID(model.UserId).Count;
                    this.lblDonateProject.Text = number2.ToString();

                    IOrderInfoService iOrderSec = basePage.GetBusinessInterface<IOrderInfoService>();
                    List<BLL.Entity.OrderBusiEntity> result  = iOrderSec.GetOrdersbyUserId(model.UserId);
                    int number3 = result.Where(m => m.IsPayment == true).Count();
                    this.lblPayOrder.Text = number3.ToString();
                    int number4 = (result.Count - int.Parse(this.lblPayOrder.Text));
                    this.lblUnpayOrder.Text = number4.ToString();
                    List<BLL.Entity.OrderBusiEntity>  orderList = iOrderSec.GetReceivedOrders(model.UserId);
                    int number5 = orderList.Count;
                    this.lblReceiveOrder.Text = number5.ToString();

                    this.lblTotalCount.Text = (number1 + number2 + number3 + number4 + number5).ToString();
                }
                else
                {
                    this.lblName.Text = Resources.Resource.Login;
                    SubmitLogin();
                }
                
            }
        }

        private void SubmitLogin()
        {
            string userName = Request.Form["email"];
            string userPwd = Request.Form["password"];
            string ck = Request.Form["ChkMasterRemember"];
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                UserLogin(userName, userPwd, Request.CurrentExecutionFilePath.Substring(1), ck=="on"? true:false);
            }
        }

        public void UserLogin(string userName, string userPwd, string pageUrl, bool isRemember)
        {
            string msg = string.Empty;
            IUserService iUserSec = basePage.GetBusinessInterface<IUserService>();
            MEDIA.Model.User entity = iUserSec.Login(userName);
            if (entity != null)
            {
                if (entity.IsCheck == false)
                {
                    msg = Resources.Resource.Alter_activeaccount;
                }
                else
                {
                    if (EncryptUtil.Decrypt(entity.LoginPwd) == userPwd)
                    {
                        basePage.RememberLogin(isRemember, userName, this.Response);
                        basePage.SetSession(SysConfigConst.FrontLoginUser, entity);
                        Response.Redirect(pageUrl);
                    }
                    else
                    {
                        msg = Resources.Resource.Alter_Pwdwrong;
                    }
                }
            }
            else
            {
                msg = Resources.Resource.Alter_emailwrong;
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + msg + "');</script>");
        }

        private void SetLanguageCookie()
        {
            string language = Request["lan"];
            if (!string.IsNullOrEmpty(language))
            {
                HttpCookie cookie = new HttpCookie("my_Language");
                cookie.Expires = DateTime.Now.AddDays(100);
                cookie.Value = language;
                Response.Cookies.Add(cookie);
                Response.Redirect(Request.Url.LocalPath);
            }
        }
    }
}