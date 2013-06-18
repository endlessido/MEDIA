using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.IDAL;
using MEDIA.Model;
using System.Data.Objects.DataClasses;
using System.Text;

namespace MEDIA.WebUI
{
    public partial class ProjectListAjax : BasePage
    {
        private string Sort
        {
            get { return ViewState["Sort"] == null ? string.Empty : ViewState["Sort"].ToString(); }
            set { ViewState["Sort"] = value; }
        }

        public string Region
        {
            get { return ViewState["Area"] == null ? string.Empty : ViewState["Area"].ToString(); }
            set { ViewState["Area"] = value; }
        }

        public string Category
        {
            get { return ViewState["Category"] == null ? string.Empty : ViewState["Category"].ToString(); }
            set { ViewState["Category"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["searchname"]))
            {
                this.txtSearch.Text = Request["searchname"];
            }
            
            ListDataBind();
            if (!Page.IsPostBack)
            {
                DataBindCategory();
            }
            else
            {
                this.SearchConditionList1.Category = this.Category;
                this.SearchConditionList1.Region = this.Region;
            }
            ScriptManager.RegisterStartupScript(this.SearchConditionList1, this.GetType(), "CancelScript", "$('.page-condbox').on('click',CancelHandler);", true);
        }

        protected void SearchConditionList1_Command(object sender, CommandEventArgs e)
        {
            string str = e.CommandArgument.ToString();
            if (this.Category.Contains(str))
            {
                try
                {
                    this.Category = this.Category.Remove(this.Category.IndexOf(str), str.Length + 1);
                }
                catch {
                    this.Category = this.Category.Remove(this.Category.IndexOf(str), str.Length);
                }
            }
            if (this.Region.Contains(str))
            {
                try
                {
                    this.Region = this.Region.Remove(this.Region.IndexOf(str), str.Length +1);
                }
                catch
                {
                    this.Region = this.Region.Remove(this.Region.IndexOf(str), str.Length);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.SearchConditionList1, this.GetType(), "doPostBack", "__doPostBack(\"ctl00$ContentPlaceHolder1$LinkButton2\", \"postback\")", true);
        }

        protected void CustomPager1_Command(object sender, CommandEventArgs e)
        {
            this.CustomPager1.CurrentPageIndex = int.Parse(e.CommandArgument.ToString());
            this.SearchConditionList1.Category = this.Category;
            this.SearchConditionList1.Region = this.Region;
            ListDataBind();
        }

        private void DataBindCategory()
        {
            ISportCategoryService iSportSec = GetBusinessInterface<ISportCategoryService>();
            this.RepCategory.DataSource = iSportSec.GetUnDels();
            this.RepCategory.DataBind();

            ICountryRepository iCountryRepos = GetBusinessInterface<ICountryRepository>();
            this.RepCountry.DataSource = iCountryRepos.Find(model => model.IsDel != true).ToList();
            this.RepCountry.DataBind();
        }

        protected void SortBy_Command(object sender, CommandEventArgs e)
        {
            this.CustomPager1.CurrentPageIndex = 1;
            string eventArgument = Request.Params.Get("__EVENTARGUMENT");
            if (!string.IsNullOrEmpty(eventArgument) )
            {
                if (eventArgument != "postback")
                {
                    string[] condition = eventArgument.Split(';');
                    this.Region = condition[0];
                    this.Category = condition[1];
                    this.SearchConditionList1.Category = this.Category;
                    this.SearchConditionList1.Region = this.Region;
                    this.ListDataBind();
                }
            }
            else
            {
                LinkButton btn = sender as LinkButton;
                if (btn != null)
                {
                    this.LinkButton1.CssClass = "";
                    this.LinkButton2.CssClass = "";
                    this.LinkButton3.CssClass = "";
                    btn.CssClass = "fg-color-orangeDark";
                } 
                this.Sort = e.CommandArgument.ToString(); 
                this.ListDataBind();
            }
           
        }

        private void ListDataBind()
        {
            IDonationProjectService iProjectSec = GetBusinessInterface<IDonationProjectService>();
            List<DonationProject> list = iProjectSec.GetSearchList(this.txtSearch.Text,this.Category,this.Region);
            
            if (!string.IsNullOrEmpty(this.Sort))
            {
                switch (this.Sort)
                {
                    case "popular":
                        list = list.OrderByDescending(m => m.DonateNum).ToList();
                        break;
                    case "recent":
                        list = list.OrderByDescending(m => m.CreateDate).ToList();
                        break;
                    case "ending":
                        list = list.OrderBy(m => m.EndDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            this.CustomPager1.SetPageCount(list.Count, base.FrontPageSize);
            this.DataRepeater.DataSource = list.Skip((this.CustomPager1.CurrentPageIndex - 1) * base.FrontPageSize).Take(base.FrontPageSize);
            this.DataRepeater.DataBind();
        }

        protected bool IsHaveChild(object areas)
        {
            List<Area> reslut = (areas as EntityCollection<Area>).Where(m => m.IsDel != true).ToList();
           return reslut.Count == 0 ? false : true;
        }

        protected string BindCountryData(object areas)
        {
            List<Area> reslut = (areas as EntityCollection<Area>).Where(m=>m.IsDel != true).ToList();
            StringBuilder strBuilder = new StringBuilder();

            if (IsHaveChild(Eval("Areas"))){
                strBuilder.Append("<li data-role=\"dropdown\" class=\"dropdown active\">");
                strBuilder.Append("<a href='#'><i class='icon-list'></i> " + Eval("CountryName") + "</a>");
                strBuilder.Append("<ul class='sub-menu multidropdown sidebar-dropdown-menu' data-category='region'>");
                foreach (Area item in reslut)
                {
                    strBuilder.Append("<li><a href='#'>"+item.AreaName+"</a></li>");
                }
                strBuilder.Append("</ul></li>");
            }
            else
            {
                strBuilder.Append("<li><a href='#'>" + Eval("CountryName") + "</a></li>");
            }
            return strBuilder.ToString();
        }
    }
}