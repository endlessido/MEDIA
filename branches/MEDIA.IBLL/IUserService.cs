using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface IUserService
    {
        User Login(string userName);
        List<User> GetNewlyByCount();
        List<User> GetAll();
        bool Regist(User model);
        int GetTotalCount();
        int GetUnCheckCount();
        bool Active(int uId, Guid activekey);
        bool Modify(User model);
        bool IsEmailExist(string emailAddress);
        User GetModelByEmail(string emailAddress);
        User GetModelById(int userId);
    }
}
