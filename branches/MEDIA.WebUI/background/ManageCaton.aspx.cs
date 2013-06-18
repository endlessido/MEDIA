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
    public partial class ManageCaton : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                BindAreaData();
            }
        }

        private void BindAreaData()
        {
            string country = Request.QueryString["country"];
            if (!string.IsNullOrEmpty(country))
            {
                int countryId = int.Parse(country);
                IAreaService iAreaSec = GetBusinessInterface<IAreaService>();
                this.RepArea.DataSource = iAreaSec.GetUnDelAreas(countryId);
                this.RepArea.DataBind();
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string country = Request.QueryString["country"];
            int countryId = int.Parse(country);
            string newIds = Request.Form["NewOption"];
            string editIds = Request.Form["EditOption"];
            string delIds = Request.Form["DelOption"];
            IAreaService iAreaSec = GetBusinessInterface<IAreaService>();
            if (!string.IsNullOrEmpty(newIds))
            {
                iAreaSec.Add(newIds.Split(';'), countryId);
            }
            if (!string.IsNullOrEmpty(editIds))
            {
                iAreaSec.Modify(editIds.Split(';'));
            }
            if (!string.IsNullOrEmpty(delIds))
            {
                iAreaSec.Del(delIds.Split(';'));
            }
            BindAreaData();
        }
    }
}