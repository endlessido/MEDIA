using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.Model;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Utilities;
using MEDIA.IDAL;
using log4net;
using System.Reflection;

namespace MEDIA.WebUI
{
    public partial class MyProfile :BasePage
    {
        public User model;
        public string frameNum ;
        protected void Page_Load(object sender, EventArgs e)
        {
            model = GetSession(SysConfigConst.FrontLoginUser) as User;
            frameNum = Request.QueryString["frame"] == null ? "0" : Request.QueryString["frame"];
            if (!Page.IsPostBack)
            {
                
                this.lblUserName.Text = model.FirstName;
                IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
                this.RepFoundedPro.DataSource = iProjectSec.GetFoundedProjectsByUserId(model.UserId);
                this.RepFoundedPro.DataBind();

                IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
                this.RepDonatedPro.DataSource = iRecordSec.GetDonationProsByUserID(model.UserId);
                this.RepDonatedPro.DataBind();
                BindCountryData();
            }
        }

        public string IsHref(object isCheck,object pid)
        { 
            string result = string.Empty;
            if ((bool)isCheck)
            { 
             result = "<a href=\"ProjectDetails.aspx?pid="+pid.ToString()+"\">";
            }
            else
            {
             result = "<a href=\"#\">";
            }
            return result;
        }

        public string IsShowUnCheck()
        {
            string result = string.Empty;
            if (!(bool)Eval("IsCheck"))
            {
                result = "<span class='name'>Project Is Un Check </span>";
            }
            else
            {
                result = "<span class='name'> " + Resources.Resource.Progress + "</span>";
                result += "<span class='badge'>" + GetPercent(Eval("TotalFunding"), Eval("ReceivedFunding")) + "%</span>";
            }
            return result;
        }

        private void BindCountryData()
        {
            ICountryRepository iCountryRepos = GetBusinessInterface<ICountryRepository>();
            this.ddlCountry.DataSource = iCountryRepos.GetAll();
            this.ddlCountry.DataTextField = "CountryName";
            this.ddlCountry.DataValueField = "CountryId";
            this.ddlCountry.DataBind();
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            IUserService iuserSec = GetBusinessInterface<IUserService>();
            if (!string.IsNullOrEmpty(Request.Form["oldPwd"]) && !string.IsNullOrEmpty(Request.Form["newPwd"]))
            {
                if (model.LoginPwd.Equals(EncryptUtil.Encrypt(Request.Form["oldPwd"])))
                {
                    model.LoginPwd = EncryptUtil.Encrypt(Request.Form["newPwd"]);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_Pwdwrong + "');</script>");
                    return;
                }
            }
            try
            {
                model.FirstName = Request.Form["txtFirstName"];
                model.LastName = Request.Form["txtLastName"];
                model.Address = Request.Form["txtAddress"];
                model.CountryName = this.ddlCountry.SelectedItem.Text;
                model.CountryId = int.Parse(this.ddlCountry.SelectedItem.Value);
                model.Zip = Request.Form["ZIP"];
                model.Town = Request.Form["Town"];
                model.Birthday = System.DateTime.Parse(string.Format("{0}-{1}-{2}", Request.Form["year"], Request.Form["month"], Request.Form["day"]));
                iuserSec.Modify(model);
            }
            catch (Exception exp)
            {
                ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                log.Error("Modify User Account", exp);
            }
        }
    }
}