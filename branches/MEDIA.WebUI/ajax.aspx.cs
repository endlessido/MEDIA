using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEDIA.IBLL;
using MEDIA.IDAL;
using MEDIA.Model;
using System.Web.Script.Serialization;

namespace MEDIA.WebUI
{
    public partial class ajax : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           string validateId = Request["fieldId"];
           string validateValue = Request["fieldValue"];
           if (!string.IsNullOrEmpty(validateValue))
           {
               switch (validateId)
               {
                   case "ContentPlaceHolder1_txtEmail":
                       ValidEmaillAddress(validateId, validateValue);
                       break;
                   case "txtProductName":
                       ValidProjectName(validateId, validateValue);
                       break;
                   case "ddlCountry":
                       GetAreaByCountryId(int.Parse(validateValue));
                       break;
                   default:
                       break;
               }
               Response.End();
           }
        }

        private void GetAreaByCountryId(int countryId)
        {
            IAreaService iAreaSec = GetBusinessInterface<IAreaService>();
            var result = iAreaSec.GetUnDelAreas(countryId)
                                   .Select(model => new { Id = model.AreaId, Name = model.AreaName });
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(result);
            Response.Write(json);
        }

        private void ValidProjectName(string validateId, string validateValue)
        {
            IDonationProjectService iProcSec = GetBusinessInterface<IDonationProjectService>();
            string resultMsg = string.Empty;
            if (iProcSec.IsProjectNameExist(validateValue))
            {
                resultMsg = string.Format("[\"{0}\", false, \"{1}\"]", validateId, "The Name of the project is already taken");
            }
            else
            {
                resultMsg = string.Format("[\"{0}\", true, \"{1}\"]", validateId, "* This Name of the project is available");
            }
            Response.Write(resultMsg);
        }

        private void ValidEmaillAddress(string validateId, string validateValue)
        {
            IUserService iUserSec = GetBusinessInterface<IUserService>();
            string resultMsg = string.Empty;
            if (iUserSec.IsEmailExist(validateValue))
            {
                resultMsg = string.Format("[\"{0}\", false, \"{1}\"]", validateId, "The email address is already taken");
            }
            else
            {
                resultMsg = string.Format("[\"{0}\", true, \"{1}\"]", validateId, "* This email address is available");
            }
            Response.Write(resultMsg);
        }
    }
}