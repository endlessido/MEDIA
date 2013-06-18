using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.Model;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.Infrastructure.Utilities;
namespace MEDIA.BLL
{
    /// <summary>
    /// Admin Service Class
    /// </summary>
    public class AdminService : IAdminService
    {
        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="adminName">admin name</param>
        /// <param name="adminPwd">admin pwd</param>
        /// <returns>Administrator Entity</returns>
        public Administrator Login(string adminName, string adminPwd)
        {
            if (string.IsNullOrEmpty(adminName) || string.IsNullOrEmpty(adminPwd))
            {
                return null;
            }
            else
            {
                IAdminRepository iAdminRepos = IoCContext.Container.Resolve<IAdminRepository>();
                string encyptPwd = EncryptUtil.Encrypt(adminPwd);
                Administrator entity = iAdminRepos
                    .Find(model => model.AdminName.Equals(adminName) && model.AdminPwd.Equals(encyptPwd))
                    .FirstOrDefault();
                return entity;
            }
        }

        /// <summary>
        /// Add New Administrator Entity
        /// </summary>
        /// <param name="newModel">new entity</param>
        /// <returns>is success</returns>
        public bool Add(Administrator newModel)
        {
            if (null == newModel)
            {
                return false;
            }

            if (string.IsNullOrEmpty(newModel.AdminName) || string.IsNullOrEmpty(newModel.AdminPwd))
            {
                return false;
            }

            IAdminRepository iAdminRepos = IoCContext.Container.Resolve<IAdminRepository>();
            if (!string.IsNullOrEmpty(newModel.AdminPwd))
            {
                newModel.AdminPwd = EncryptUtil.Encrypt(newModel.AdminPwd);
            }
            int count = iAdminRepos.Add(newModel, false);
            return count > 0;
        }
    }
}
