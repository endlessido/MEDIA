using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.Model;
using MEDIA.Infrastructure.Ioc;
using MEDIA.IDAL;
using Microsoft.Practices.Unity;
using MEDIA.Infrastructure.Utilities;
namespace MEDIA.BLL
{
    /// <summary>
    /// User BLL Service Class
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userName">User's Name,must be a email format</param>
        /// <param name="userPwd">User's password</param>
        /// <returns>User Entity</returns>
        public User Login(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }
            else
            {
                IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
                User entity = iUserRepos
                    .Find(model => model.EmailAddress.Equals(userName))
                    .FirstOrDefault();
                return entity;
            }
        }

        /// <summary>
        /// Regist a new user
        /// </summary>
        /// <param name="model">user entity</param>
        /// <returns>is regist success</returns>
        public bool Regist(User model)
        {
            if (null != model)
            {
                IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
                int count = iUserRepos.Add(model, false);
                return count > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// get total count of regist user
        /// </summary>
        /// <returns>count</returns>
        public int GetTotalCount()
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            return iUserRepos.GetAll().Count;
        }

        /// <summary>
        /// get count of the un check user
        /// </summary>
        /// <returns>count</returns>
        public int GetUnCheckCount()
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            return iUserRepos.Find(model =>model.IsCheck == false).Count();
        }

        /// <summary>
        /// get list of the newly regist users
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>user list</returns>
        public List<User> GetNewlyByCount()
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            IQueryable<User> list = iUserRepos
                .GetQuery()
                .Where(model=>model.IsCheck == false)
                .OrderByDescending(model => model.CreateDate);
            return list.ToList();
        }

        /// <summary>
        /// Check the ActiveKey & UserId to Active Account
        /// </summary>
        /// <param name="uId">PK</param>
        /// <param name="activekey">active key</param>
        /// <returns>is active success</returns>
        public bool Active(int uId, Guid activekey)
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            User model = iUserRepos.GetSingle(m => m.UserId.Equals(uId));
            if (null != model)
            {
                if (model.ActiveKey.Value.Equals(activekey))
                {
                    model.IsCheck = true;
                    return iUserRepos.DbContext.SaveChanges() >0;
                }
            }
            return false;
        }

        /// <summary>
        /// Modify User Profile Information
        /// </summary>
        /// <param name="model">User Entity</param>
        /// <returns>is modify success</returns>
        public bool Modify(User model)
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            User oldModel = iUserRepos.GetSingle(m => m.UserId == model.UserId);
            oldModel.LoginPwd = model.LoginPwd;
            oldModel.FirstName = model.FirstName;
            oldModel.LastName = model.LastName;
            oldModel.Address = model.Address;
            oldModel.CountryName = model.CountryName;
            oldModel.CountryId = model.CountryId;
            oldModel.Town = model.Town;
            oldModel.Birthday = model.Birthday;
            oldModel.Zip = model.Zip;
            return iUserRepos.Update(oldModel, false) > 0;
        }


        /// <summary>
        /// check the email address is exist
        /// </summary>
        /// <param name="emailAddress">email address</param>
        /// <returns>true or false</returns>
        public bool IsEmailExist(string emailAddress)
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            return iUserRepos.GetSingle(model => model.EmailAddress.Equals(emailAddress)) == null ? false : true;
        }


        public User GetModelByEmail(string emailAddress)
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            return iUserRepos.GetSingle(m => m.EmailAddress == emailAddress);
        }


        public User GetModelById(int userId)
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            return iUserRepos.GetSingle(m => m.UserId == userId);
        }


        public List<User> GetAll()
        {
            IUserRepository iUserRepos = IoCContext.Container.Resolve<IUserRepository>();
            return iUserRepos.GetAll().ToList();
        }
    }
}
