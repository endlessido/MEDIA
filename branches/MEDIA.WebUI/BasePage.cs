using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using log4net;
using System.Reflection;
using System.Globalization;
using System.Xml;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Utilities;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MEDIA.WebUI
{
    public class BasePage : System.Web.UI.Page
    {
        #region Property
        public string ImgUploadPath
        {
            get { return Server.MapPath(ConfigurationManager.AppSettings[SysConfigConst.ImgUpLoadPath]); }
        }

        public string SHAKEY
        {
            get { return ConfigurationManager.AppSettings[SysConfigConst.ShaKey]; }
        }

        public string ImgVirtualPath
        {
            get { return ConfigurationManager.AppSettings[SysConfigConst.ImgUpLoadPath]; }
        }

        public string ImgDefault
        {
            get { return ConfigurationManager.AppSettings[SysConfigConst.DefaultImage]; }
        }

        public string IndexPageUrl
        {
            get { return ConfigurationManager.AppSettings[SysConfigConst.IndexPageUrl]; }
        }

        public int FrontPageSize
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[SysConfigConst.FrontPageSize]))
                {
                    return int.Parse(ConfigurationManager.AppSettings[SysConfigConst.FrontPageSize]);
                }
                else
                {
                    return 5;
                }
            }
        }

        public int BackgroundPageSize
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[SysConfigConst.BackgroundPageSize]))
                {
                    return int.Parse(ConfigurationManager.AppSettings[SysConfigConst.BackgroundPageSize]);
                }
                else
                {
                    return 10;
                }
            }
        }

        public int ProjectOfDayCount
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[SysConfigConst.ProjectOfDayCount]))
                {
                    return int.Parse(ConfigurationManager.AppSettings[SysConfigConst.ProjectOfDayCount]);
                }
                else
                {
                    return 5;
                }
            }
        }

        public string ContactUsEmail
        {
            get { return ConfigurationManager.AppSettings[SysConfigConst.ContactUsEmail]; }
        }

        private string ConfigEmail
        {
            get { return Server.MapPath("~/ConfigEmail.xml"); }
        }
        #endregion

        public string GetCurrency(decimal money)
        {
            return string.Format("{0:N2}",money);
        }

        public string CreateOrderID()
        {
            Model.User User = Session[SysConfigConst.FrontLoginUser] as Model.User;
            int userId = User == null ? 0 : User.UserId;
            DateTime time = System.DateTime.Now;
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", time.Year , time.Month , time.Day , time.Hour , time.Minute , time.Second , time.Millisecond , userId);
        }

        public void UserLogin(string userName, string userPwd,string pageUrl, bool isRemember)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                IUserService iUserSec = GetBusinessInterface<IUserService>();
                MEDIA.Model.User entity = iUserSec.Login(userName);
                if (entity != null)
                {
                    if (entity.IsCheck == false)
                    {
                        msg = Resources.Resource.Alter_activeaccount;
                    }
                    else
                    {
                        if (EncryptUtil.Decrypt(entity.LoginPwd) == userPwd)
                        {
                            RememberLogin(isRemember, userName, this.Response);
                            SetSession(SysConfigConst.FrontLoginUser, entity);
                            Response.Redirect(pageUrl);
                        }
                        else
                        {
                            msg = Resources.Resource.Alter_Pwdwrong;
                        }
                    }
                }
                else
                {
                    msg = Resources.Resource.Alter_emailwrong;
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>showDialog('Attion','" + msg + "');</script>");
            }
        }

        public void RememberLogin(bool isRemember,string userName, HttpResponse response, bool isAdmin = false)
        {
            string cookieName = string.Empty;
            if (isAdmin)
            {
                cookieName = SysConfigConst.remeberLogin_Admin;
            }
            else
            {
                cookieName = SysConfigConst.remeberLogin;
            }
            if (isRemember)
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(3);
                cookie.Value = userName;
                response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = null;
                response.Cookies.Add(cookie);
            }
        }

        protected string[] GetEmailConfig(string node)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigEmail);
            XmlElement xmlNode = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode(node);
            string[] result = new string[2];
            result[0] = xmlNode.GetAttribute("Title");
            result[1] = xmlNode.GetAttribute("Content");
            return result;
        }

        protected void SetEmailConfig(string node, string[] newConfig)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigEmail);
            XmlElement xmlNode = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode(node);
            xmlNode.SetAttribute("Title", newConfig[0]);
            xmlNode.SetAttribute("Content", newConfig[1]);
            xmlDoc.Save(ConfigEmail);
        }

        protected override void InitializeCulture()
        {
            if (Request.Cookies["my_Language"] != null)
            {
                setCulture();
            }
            base.InitializeCulture();
        }

        public string GetUserNameFromCookie(HttpRequest request, bool isAdmin = false)
        {
            try
            {
                string cookieName = SysConfigConst.remeberLogin;
                if (isAdmin)
                {
                    cookieName = SysConfigConst.remeberLogin_Admin;
                }
                if (request.Cookies[cookieName] != null)
                {
                    return request.Cookies[cookieName].Value;
                }
                else
                    return string.Empty;
            }
            catch {
                return string.Empty;
            }
        }

        private void setCulture()
        {
            try
            {
                String selectedLanguage = Request.Cookies["my_Language"].Value;
                UICulture = selectedLanguage;
                Culture = selectedLanguage;


                System.Threading.Thread.CurrentThread.CurrentCulture =
                    System.Globalization.CultureInfo.CreateSpecificCulture(selectedLanguage);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new
                    System.Globalization.CultureInfo(selectedLanguage);
            }
            catch
            {
                Response.Cookies.Clear();
            }
        }

        public object GetSession(string key, string url=null)
        {
            if (Session[key] != null)
            {
                return Session[key];
            }
            else
            {
                try
                {
                   if(key.Equals(SysConfigConst.FrontLoginUser))
                    {
                        Uri uri = Request.Url;
                        if (string.IsNullOrEmpty(url))
                        {
                            Response.Redirect("UserLogin.aspx?url=" + uri.PathAndQuery);
                        }
                        else
                        {
                            Response.Redirect("UserLogin.aspx?url=" + url);
                        }
                    }
                    else if(key.Equals(SysConfigConst.BGLoginUser))
                    {
                         Response.Redirect("ManageLogin.aspx");
                    }
                }
                catch { }
                return null;
            }
        }

        public T GetBusinessInterface<T>()
        {
            return IoCContext.Container.Resolve<T>();
        }

        public void SetSession(string key, object value)
        {
            Session[key] = value;
        }

        public int GetPercent(object totalFunding, object receivedFunding)
        {
            if (totalFunding != null && receivedFunding != null)
            {
                decimal recFunding = decimal.Parse(receivedFunding.ToString());
                decimal toalFunding = decimal.Parse(totalFunding.ToString());
                if (toalFunding == 0)
                {
                    return 0;
                }
                else
                {
                    int result = (int)((recFunding / toalFunding) * 100);
                    return result > 100 ? 100 : result;
                }
            }
            return 0;
        }

        public string GetProjectImage(object imgUrl)
        {
            if (imgUrl != null)
            {
                string imgFile = imgUrl.ToString();
                string path = string.Format("{0}{1}", this.ImgUploadPath, imgFile);
                if (System.IO.File.Exists(path))
                {
                    return string.Format("{0}{1}", this.ImgVirtualPath, imgFile);
                }
            }
            return this.ImgDefault;
        }

        public int GetProjectLeaveDays(DateTime? endTime)
        {
            TimeSpan? leaveTime = (endTime - System.DateTime.Now);
            return leaveTime.Value.Days;
        }

        public string GetSex(bool sex)
        {
            return sex ? "Male" : "Female";
        }

        public string[] GetConfigClass(string configKey)
        {
            string profileClass = string.Empty;
            profileClass = ConfigurationManager.AppSettings[configKey];
            if (!string.IsNullOrEmpty(profileClass))
            {
                return profileClass.Split(',');
            }
            else
            {
                return null;
            }
        }

        public string GetRemainNum(object totalNum, object saleNum, object isLimit)
        {
            if (totalNum != null && saleNum != null && isLimit != null)
            {
                return ((bool)isLimit) ? ((int)totalNum - (int)saleNum).ToString() : "No limit";
            }
            else
            {
                return "some error";
            }
        }

        int num = 0;
        protected string GetDivCssClass(string configKey, int columnSize)
        {
            if (num >= columnSize)
            {
                num = 0;
            }
            string[] strAry = this.GetConfigClass(configKey);
            if (strAry != null)
            {
                string cssClass = strAry[num];
                num++;
                return cssClass;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public struct SysConfigConst
    {
        public const string BGLoginUser = "BGLoginUser";
        public const string FrontLoginUser = "FrontLoginUser";
        public const string ImgUpLoadPath = "ImgUpLoadPath";
        public const string DefaultImage = "DefaultImage";
        public const string IndexPageUrl = "IndexPageUrl";
        public const string FrontPageSize = "FrontPageSize";
        public const string BackgroundPageSize = "BackgroundPageSize";
        public const string TotalCount = "TotalCount";
        public const string MyProfileClass = "MyProfileClass";
        public const string GoodyClass = "GoodyClass";
        public const string DonateGoodyClass = "DonateGoodyClass";
        public const string ProjectOfDayCount = "ProjectOfDayCount";
        public const string ContactUsEmail = "ContactUsEmail";
        public const string ActiveEmail = "ActiveEmail";
        public const string ForgetPwdEmail = "ForgetPwdEmail";
        public const string ShaKey = "ShaKey";
        public const string remeberLogin = "remeberLogin";
        public const string remeberLogin_Admin = "remeberLogin_Admin";
    }
}