using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;

namespace MEDIA.WebUI.background
{
    public partial class ManageCategory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSession(SysConfigConst.BGLoginUser);
            if (!Page.IsPostBack)
            {
                BindCategoryData();
            }
        }

        private void BindCategoryData()
        {
            ISportCategoryService iSportSec = GetBusinessInterface<ISportCategoryService>();
            this.RepCategory.DataSource = iSportSec.GetUnDels();
            this.RepCategory.DataBind();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string newIds = Request.Form["NewOption"];
            string editIds = Request.Form["EditOption"];
            string delIds = Request.Form["DelOption"];
            ISportCategoryService iSportSec = GetBusinessInterface<ISportCategoryService>();
            if (!string.IsNullOrEmpty(newIds))
            {
                iSportSec.Add(newIds.Split(';'));
            }
            if (!string.IsNullOrEmpty(editIds))
            {
                iSportSec.Modify(editIds.Split(';'));
            }
            if (!string.IsNullOrEmpty(delIds))
            {
                iSportSec.Del(delIds.Split(';'));
            }
            BindCategoryData();
        }
    }
}