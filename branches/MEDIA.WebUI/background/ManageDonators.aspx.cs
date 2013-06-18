using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Model;
using MEDIA.BLL.Entity;

namespace MEDIA.WebUI.background
{
    public partial class ManageDonators :BasePage
    {
        public Goody model;
        protected void Page_Load(object sender, EventArgs e)
        {
            string gid = Request.QueryString["gid"];
            if (!string.IsNullOrEmpty(gid))
            {
                if (!Page.IsPostBack)
                {
                    int goodyId;
                    if (int.TryParse(gid, out goodyId))
                    {
                        IGoodyService iGoodySec = GetBusinessInterface<IGoodyService>();
                        model = iGoodySec.GetGoodyById(goodyId);

                        IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
                        List<DonatedRecordBusiEntity> list = iRecordSec.GetDonatorByGoodieId(goodyId);
                        if (list != null)
                        {
                            this.CustomPager1.SetPageCount(list.Count, base.BackgroundPageSize);
                            this.CustomPager1.SearchCondition.Add("gid", gid);
                            string curpageindex = Request.QueryString["curpageindex"];
                            if (!string.IsNullOrEmpty(curpageindex))
                            {
                                this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                            }
                            this.RepeaterUser.DataSource = list.Skip((this.CustomPager1.CurrentPageIndex - 1) * BackgroundPageSize).Take(BackgroundPageSize);
                            this.RepeaterUser.DataBind();
                        }
                    }
                }
            }
        }
    }
}