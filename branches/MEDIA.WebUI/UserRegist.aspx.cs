using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Utilities;
using MEDIA.Model;
using MEDIA.IDAL;
using System.Text;

namespace MEDIA.WebUI
{
    public partial class UserRegist : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCountryData();
            }
        }

        private void BindCountryData()
        {
            ICountryRepository iCountryRepos = GetBusinessInterface<ICountryRepository>();
            IList<Country> list = iCountryRepos.Find(model=>model.IsDel != true).ToList();
            this.ddlCountry.Items.Add(new ListItem(Resources.Resource.Select_Country, ""));
            foreach (var item in list)
            {
                this.ddlCountry.Items.Add(new ListItem(item.CountryName, item.CountryId.ToString()));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                User newModel = ConstructModel();
                IUserService iUserSec = GetBusinessInterface<IUserService>();
                bool isSuccess = iUserSec.Regist(newModel);
                if (isSuccess)
                {
                    string[] emailConfig = base.GetEmailConfig(SysConfigConst.ActiveEmail);
                    EmailMessage emailMsg = new EmailMessage();
                    emailMsg.To = newModel.EmailAddress;
                    emailMsg.Subject = emailConfig[0];
                    emailMsg.Body = string.Format(emailConfig[1], newModel.FirstName, newModel.LastName, this.IndexPageUrl + "ActiveAccount.aspx?id=" + newModel.UserId + "&ak=" + newModel.ActiveKey + "");
                    emailMsg.SendCompleted += new EmailMessage.SendResultCallBackHandler(emailMsg_SendCompleted);
                    emailMsg.Send();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion', '" + Resources.Resource.Alter_registfailed + "');</script>");
                }
            }
            catch (System.FormatException exp)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>showDialog('Attion','birthday is wrong');</script>");
            }
            catch (System.ArgumentException exp)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>showDialog('Attion','Argument is wrong');</script>");
            }
        }

        private User ConstructModel()
        {
            string strPwd = Request.Form["txtPwd"];
            string strGender = Request.Form["gender"];

            DateTime birthday = System.DateTime.Parse(string.Format("{0}-{1}-{2}",Request.Form["txtYear"], Request.Form["txtMonth"],Request.Form["txtDay"]));
            Guid activeKey = Guid.NewGuid();
            User newModel = new User
            {
                CreateDate = System.DateTime.Now,
                FirstName = this.txtFirstName.Text,
                LastName = this.txtLastName.Text,
                EmailAddress = this.txtEmail.Text,
                LoginPwd = EncryptUtil.Encrypt(strPwd),
                Birthday = birthday,
                Zip = this.txtZIP.Text,
                Town = this.txtTown.Text,
                CountryName = this.ddlCountry.SelectedItem.Text,
                CountryId = int.Parse(this.ddlCountry.SelectedItem.Value),
                Address = this.txtAddress.Text,
                Gender = strGender == "1" ? true : false,
                IsCheck = false,
                ActiveKey = activeKey
            };
            return newModel;
        }

        void emailMsg_SendCompleted(object sender, EmailMessage.SendResult e)
        {
            switch (e)
            {
                case EmailMessage.SendResult.Success:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_activeaccount + "','UserLogin.aspx');</script>");
                    break;
                case EmailMessage.SendResult.Error:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_tryagain + "');</script>");
                    break;
                case EmailMessage.SendResult.Cancled:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + Resources.Resource.Alter_servercancled + "');</script>");
                    break;
            }
        }


    }
}