using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using MEDIA.IBLL;
using MEDIA.Model;
using System.Reflection;
using log4net;
using MEDIA.IDAL;


namespace MEDIA.WebUI
{
    public partial class CreateProject : BasePage
    {
        User userEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            userEntity = GetSession(SysConfigConst.FrontLoginUser) as MEDIA.Model.User;
            if (!Page.IsPostBack)
            {
                BindDropDownListData();
            }
        }

        private void BindDropDownListData()
        {
            ICountryRepository iCountryRepos = GetBusinessInterface<ICountryRepository>();
            IList<Country> countryList = iCountryRepos.Find(model=>model.IsDel != true).ToList();
            this.ddlCountry.Items.Add(new ListItem(Resources.Resource.Select_Country, ""));
            foreach (var item in countryList)
            {
                this.ddlCountry.Items.Add(new ListItem(item.CountryName, item.CountryId.ToString()));
            }
            ISportCategoryRepository iCategoryRepos = GetBusinessInterface<ISportCategoryRepository>();
            IList<SportCategory> categoryList = iCategoryRepos.Find(model => model.IsDel != true).ToList();
            this.ddlCategory.Items.Add(new ListItem(Resources.Resource.Select_Category, ""));
            foreach (var item in categoryList)
            {
                this.ddlCategory.Items.Add(new ListItem(item.CategoryName, item.CategoryId.ToString()));
            }
        }

        private Model.DonationProject GetModel()
        {
            string endDays = Request.Form["projtime"];
            int days = 0;
            if (!string.IsNullOrEmpty(endDays))
            {
                days = int.Parse(endDays);
            }
            string caton = Request.Form["canton"];
            string[] catonArray = null;
            if(!string.IsNullOrEmpty(caton))
            {
                 catonArray = caton.Split(',');
            }

            Model.DonationProject projectEntity = new Model.DonationProject
            {
                AreaName = catonArray[1],
                CountryName = this.ddlCountry.SelectedItem.Text,
                CategoryName = this.ddlCategory.SelectedItem.Text,
                AreaId = int.Parse(catonArray[0]),
                CategoryId = int.Parse(this.ddlCategory.SelectedItem.Value),
                TotalFunding = decimal.Parse(Request.Form["totalfunding"]),
                ReceivedFunding = 0,
                DonateNum = 0,
                IsFinished = false,
                CreateDate = System.DateTime.Now,
                CurrencyStr = Request.Form["fundingcurrency"],
                YoutubeUrl = this.txtYoutubeURL.Value,
                ProjectName = Request.Form["txtProductName"],
                ProjectSummary = this.txtProductSummary.Value,
                SelfIntroduce = this.txtSelfIntroduce.Value,
                NeedReason = this.txtNeedReason.Value,
                Homepage = Request.Form["homepage"],
                Feedbookpage = Request.Form["fdbookpage"],
                IsSendNews = this.chkIsSend.Checked,
                EndDate = System.DateTime.Now.AddDays(days),
                IsCheck = false,
                UserId = userEntity.UserId
            };
            return projectEntity;
        }

        private string SaveUploadFile()
        {
            HttpPostedFile file = this.uploadFile.PostedFile;
            if (this.uploadFile.PostedFile.ContentLength / 1024 > 1000)
            {
                throw new System.Web.HttpException();
            }
            string path = this.ImgUploadPath;
            string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ".jpg");
            file.SaveAs(string.Format("{0}{1}", path, fileName));
            return fileName;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string goodyName = Request.Form["goodyname"];
            string goodyDesc = Request.Form["goodydesc"];
            string goodyNum = Request.Form["goodynum"];
            string goodyPrice = Request.Form["goodyprice"];
            string ddlCurrency = Request.Form["fundingcurrency"];
            IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
            try
            {
                Model.DonationProject projectEntity = GetModel();
                projectEntity.ImgUrl = SaveUploadFile();
                iProjectSec.Add(projectEntity, goodyName, goodyDesc, goodyNum, goodyPrice, ddlCurrency);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','create success,wait for us to check','MyProfile.aspx');</script>");
            }
            catch (System.Web.HttpException imgExp)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','the size of image must be in 1M');</script>");
            }
            catch (Exception exp)
            {
                ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                log.Error("Submit a Project", exp);
            }
        }
    }
}