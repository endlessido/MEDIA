using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace MEDIA.WebUI
{
    [ToolboxData("<{0}:CustomPager runat=\"server\"></{0}:CustomPager>")]
    public class CustomPager : WebControl, INamingContainer
    {
        public enum RequiredMethodEnum
        {
            get,post
        }

        public Dictionary<string,string> SearchCondition { get; set; }

        public RequiredMethodEnum RequiredMethod { get; set; }

        public event CommandEventHandler Command
        {
            add
            {
                Events.AddHandler(this, value);
            }
            remove
            {
                Events.RemoveHandler(this, value);
            }
        }

        void btn_Command(object sender, CommandEventArgs e)
        {
            OnCommand(sender, e);
        }

        protected void OnCommand(object sender, CommandEventArgs e)
        {
            CommandEventHandler cmd = (CommandEventHandler)base.Events[this];
            if (cmd != null)
            {
                cmd(sender, e);
            }
        }

        private List<CustomPostLi> PostLilist ;

        public CustomPager()
        {
            this.SearchCondition = new Dictionary<string, string>();
        }

        public void SetPageCount(int count, int pagingSize)
        {
            this.PageCount = count % pagingSize == 0 ? count / pagingSize : count / pagingSize + 1;
        }

        public int PageCount 
        {
            get { return ViewState["PageCount"] == null ? 0 : (int)ViewState["PageCount"]; }
            private set { ViewState["PageCount"] = value; }
        }

        public int PageSize
        {
            get { return ViewState["PageSize"] == null ? 5 : (int)ViewState["PageSize"]; }
            set { ViewState["PageSize"] = value; }
        }

        public int CurrentPageIndex
        {
            get { return ViewState["CurrentPageIndex"] == null ? 1 : (int)ViewState["CurrentPageIndex"]; }
            set { ViewState["CurrentPageIndex"] = value; }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Ul;
            }
        }

        protected override void CreateChildControls()
        {
            switch (RequiredMethod)
            {
                case RequiredMethodEnum.get:
                    CreateChildControls<CustomGetLi>(RequiredMethod);
                    foreach (CustomGetLi item in this.Controls)
                    {
                        item.SearchCondition = this.SearchCondition;
                    }
                    break;
                case RequiredMethodEnum.post:
                    CreatePostControls();
                    break;
                default:
                    break;
            }
        }

        private void CreatePostControls()
        {
            PostLilist = new List<CustomPostLi>();
            CreateChildControls<CustomPostLi>(RequiredMethod);
            foreach (CustomPostLi item in PostLilist)
            {
                string strNum = item.IsShowNumber ? item.Number.ToString() : string.Empty;
                LinkButton btn = new LinkButton();
                btn.Command += new CommandEventHandler(btn_Command);
                btn.CommandArgument = item.Number.ToString();
                btn.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
                if (item.IsShowNumber)
                {
                    btn.Text = item.Number.ToString();
                }
                if (!string.IsNullOrEmpty(item.CssClass))
                {
                    this.Controls.Add(new LiteralControl("<li class='" + item.CssClass + "'>"));
                }
                else
                {
                    this.Controls.Add(new LiteralControl("<li >"));
                }
                this.Controls.Add(btn);
                this.Controls.Add(new LiteralControl("</li>"));
            }
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (this.RequiredMethod == RequiredMethodEnum.post)
            {
                this.Controls.Clear();
                CreatePostControls();
            }
            base.RenderChildren(writer);
        }

      

        private void CreateChildControls<T>(RequiredMethodEnum method) where T : CustomLi, new()
        {
            if (PageCount != 0)
            {
                int curIndex = CurrentPageIndex;
                this.Controls.Add(new T {CssClass = "first", Number=1, IsShowNumber = false });
                this.Controls.Add(new T { CssClass = "prev", Number = curIndex == 1 ? 1 : curIndex - 1, IsShowNumber = false });
                int j = 0;

                if (CurrentPageIndex % PageSize == (PageSize - 1) && CurrentPageIndex > PageSize)
                {
                    j = (CurrentPageIndex / PageSize) * PageSize + (PageSize - 1);
                }
                else
                {
                    j = (CurrentPageIndex / PageSize) * PageSize;
                }

                bool isEnd = false;
                j = j == 0 ? 1 : j;
                for (int i = j; i < j + PageSize; i++)
                {
                    if (curIndex <= PageCount && !isEnd)
                    {
                        T li = new T();
                        li.Number = i;
                        if (curIndex.Equals(i))
                            li.CssClass = "active";
                        this.Controls.Add(li);
                    }
                    if (i == PageCount)
                        isEnd = true;
                }
                if (!isEnd)
                {
                    this.Controls.Add(new T { CssClass = "spaces", IsShowNumber = false });
                    this.Controls.Add(new T { Number = PageCount });
                }
                this.Controls.Add(new T { CssClass = "next", Number = curIndex == PageCount ? PageCount : curIndex + 1, IsShowNumber = false });
                this.Controls.Add(new T { CssClass = "last", Number = PageCount, IsShowNumber = false });
            }

            if (method == RequiredMethodEnum.post)
            {
                foreach (CustomPostLi item in this.Controls)
                {
                    PostLilist.Add(item);
                }
                this.Controls.Clear();
            }
        }
    }

    public class CustomLi : WebControl, INamingContainer
    {
        public int Number
        {
            get { return ViewState["Number"] == null ? 0 : (int)ViewState["Number"]; }
            set { ViewState["Number"] = value; }
        }

        public bool IsShowNumber
        {
            get { return ViewState["IsShowNumber"] == null ? true : (bool)ViewState["IsShowNumber"]; }
            set { ViewState["IsShowNumber"] = value; }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Li;
            }
        }

        public CustomLi() { }

    }

    public class CustomGetLi : CustomLi, INamingContainer
    {
        public Dictionary<string, string> SearchCondition { get; set; }

        public CustomGetLi()
        {
            this.SearchCondition = new Dictionary<string, string>();
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (Number != 0)
            {
                string hrefValue = string.Empty;
                if (SearchCondition != null)
                {
                    foreach (string key in SearchCondition.Keys)
                    {
                        hrefValue += string.Format("{0}={1}&", key, SearchCondition[key]);
                    }
                }
                hrefValue += "curpageindex=" + Number;
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "?" + hrefValue);
            }

            writer.RenderBeginTag(HtmlTextWriterTag.A);
            if (IsShowNumber)
            {
                writer.Write(Number);
            }

            if (CssClass.Equals("spaces"))
            {
                writer.Write("...");
            }
            writer.RenderEndTag();

            base.RenderChildren(writer);
        }
    }

    public class CustomPostLi : CustomLi, INamingContainer
    {

    }

    [ToolboxData("<{0}:SearchConditionList runat=\"server\"></{0}:SearchConditionList>")]
    public class SearchConditionList : WebControl, INamingContainer
    {
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        public string Category = string.Empty;
        public string Region = string.Empty;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "page-conditions");
            base.AddAttributesToRender(writer);
        }

        public event CommandEventHandler Command
        {
            add
            {
                Events.AddHandler(this, value);
            }
            remove
            {
                Events.RemoveHandler(this, value);
            }
        }

        void btn_Command(object sender, CommandEventArgs e)
        {
            OnCommand(sender, e);
        }

        protected void OnCommand(object sender, CommandEventArgs e)
        {
            CommandEventHandler cmd = (CommandEventHandler)base.Events[this];
            if (cmd != null)
            {
                cmd(sender, e);
            }
        }

        protected override void CreateChildControls()
        {
            if (!string.IsNullOrEmpty(Region))
            {
                foreach (var item in Region.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        LinkButton btn = new LinkButton();
                        btn.Command += new CommandEventHandler(btn_Command);
                        btn.CommandArgument = item;
                        btn.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
                        this.Controls.Add(new LiteralControl("<span rel='" + item + "' data-category='region' class='page-condbox ibk bg-color-orangeDark'>"));
                        this.Controls.Add(new LiteralControl("<ins class='ibk fg-color-white'>region: " + item + "</ins>"));
                        this.Controls.Add(btn);
                        btn.Controls.Add(new LiteralControl("<i class='icon-cancel-2 fg-color-white'></i>"));
                        this.Controls.Add(new LiteralControl("</span>"));
                    }
                }
            }

            if (!string.IsNullOrEmpty(Category))
            {
                foreach (var item in Category.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        LinkButton btn = new LinkButton();
                        btn.Command += new CommandEventHandler(btn_Command);
                        btn.CommandArgument = item;
                        btn.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
                        this.Controls.Add(new LiteralControl("<span rel='" + item + "' data-category='sport' class='page-condbox ibk bg-color-orangeDark'>"));
                        this.Controls.Add(new LiteralControl("<ins class='ibk fg-color-white'>sport: " + item + "</ins>"));
                        this.Controls.Add(btn);
                        btn.Controls.Add(new LiteralControl("<i class='icon-cancel-2 fg-color-white'></i>"));
                        this.Controls.Add(new LiteralControl("</span>"));
                    }
                }
            }
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "page-condtitle");
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write("Search Conditions:");
            writer.RenderEndTag();
            base.RenderChildren(writer);
        }
    }
}