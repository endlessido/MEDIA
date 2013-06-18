using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace MEDIA.WebUI
{

        [DefaultProperty("Text")]

        [ToolboxData("<{0}:EventTest runat=server></{0}:EventTest>")]

        public class EventTest : WebControl, INamingContainer, IPostBackEventHandler
        {

            public event EventHandler myClick
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



            protected void TestClick(object sender, EventArgs e)
            {

                EventHandler hd = (EventHandler)base.Events[this];

                if (hd != null)
                {

                    hd(sender, e);

                }

            }



            public void RaisePostBackEvent(string Index)
            {

                Label lbl = (Label)this.FindControl("lbl");

                lbl.Text += "自己的事件:" + Index;

            }



            protected override void CreateChildControls()
            {

                Label lbl = new Label();

                lbl.ID = "lbl";



                this.Controls.Add(lbl);

                this.Controls.Add(new LiteralControl("<BR>"));



                LinkButton btn = new LinkButton();

                btn.ID = "btn";

                btn.Text = "复合控件的事件测试";

                this.Controls.Add(btn);



                //给按钮添加内部事件



                btn.Click += new EventHandler(btn_Click);



                this.Controls.Add(new LiteralControl("<BR><a id=\"aa\" href=\"javascript:__doPostBack('EventTest1$btn','')\">aa</a>"));



                this.Controls.Add(new LiteralControl("<BR><a id=\"a1\" href=\"javascript:__doPostBack('" + this.ClientID + "','1')\">[1]</a>"));

                this.Controls.Add(new LiteralControl("&nbsp;<a id=\"a2\" href=\"javascript:__doPostBack('" + this.ClientID + "','2')\">[2]</a>"));

                this.Controls.Add(new LiteralControl("&nbsp;<a id=\"a3\" href=\"javascript:__doPostBack('" + this.ClientID + "','3')\">[3]</a>"));

                this.Controls.Add(new LiteralControl("&nbsp;<a id=\"a4\" href=\"javascript:__doPostBack('" + this.ClientID + "','4')\">[4]</a>"));



            }



            /**/
            /// 控件内部的事件，由现有的控件的事件触发

            void btn_Click(object sender, EventArgs e)
            {

                Label lbl = (Label)this.FindControl("lbl");

                lbl.Text += "控件内部的事件，hi";



                //调用外部事件

                TestClick(sender, e);

            }



            protected override void Render(HtmlTextWriter output)
            {

                if ((base.Site != null) && base.Site.DesignMode)
                {

                    output.Write("<div style='TEXT-ALIGN: center;width:100%'>事件测试</div>");

                }

                else
                {

                    //Page_Click();

                    //output.Write("<div id='" + this.ClientID + "Page' style='TEXT-ALIGN: center;width:90%'>");

                    base.Render(output);

                    //output.Write("</div>");

                }

            }

        }

}