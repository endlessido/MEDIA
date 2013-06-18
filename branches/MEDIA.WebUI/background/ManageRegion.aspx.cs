using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IDAL;
using MEDIA.IBLL;

namespace MEDIA.WebUI.background
{
    public partial class ManageRegion : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                BindCountryData();
            }
        }

        private void BindCountryData()
        {
            ICountryRepository iCountryRepos = GetBusinessInterface<ICountryRepository>();
            this.RepCountry.DataSource = iCountryRepos.Find(model => model.IsDel != true).ToList();
            this.RepCountry.DataBind();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string newIds = Request.Form["NewOption"];
            string editIds = Request.Form["EditOption"];
            string delIds = Request.Form["DelOption"];
            ICountryService iCountrySec = GetBusinessInterface<ICountryService>();
            if (!string.IsNullOrEmpty(newIds))
            {
                iCountrySec.Add(newIds.Split(';'));
            }
            if (!string.IsNullOrEmpty(editIds))
            {
                iCountrySec.Modify(editIds.Split(';'));
            }
            if (!string.IsNullOrEmpty(delIds))
            {
                iCountrySec.Del(delIds.Split(';'));
            }
            BindCountryData();
        }
    }
}