using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface IAdminService
    {
        Administrator Login(string adminName, string adminPwd);

        bool Add(Administrator newModel);
    }
}
