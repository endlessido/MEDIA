using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEDIA.WebUI
{
    public partial class Terms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string language = Request.Cookies["my_Language"] == null ? "en-US" : Request.Cookies["my_Language"].Value;
            string path = string.Format("documents/terms_{0}.txt", language);
            if (Application[path] == null)
            {
                System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath(path), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                Application[path] = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            this.lblTerms.Text = Application[path].ToString();
        }
    }
}