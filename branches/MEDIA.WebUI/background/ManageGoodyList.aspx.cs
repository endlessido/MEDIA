using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.Model;

namespace MEDIA.WebUI.background
{
    public partial class ManageGoodyList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IDonateRecordService iRecordSec = GetBusinessInterface<IDonateRecordService>();
            List<Goody> goodyList = iRecordSec.GetGoodyDonated();
            if (goodyList != null)
            {
                this.CustomPager1.SetPageCount(goodyList.Count, base.BackgroundPageSize);
                string curpageindex = Request.QueryString["curpageindex"];
                if (!string.IsNullOrEmpty(curpageindex))
                {
                    this.CustomPager1.CurrentPageIndex = int.Parse(curpageindex);
                }
                this.RepGoodies.DataSource = goodyList.Skip((this.CustomPager1.CurrentPageIndex - 1) * BackgroundPageSize).Take(BackgroundPageSize);
                this.RepGoodies.DataBind();
            }
        }
    }
}