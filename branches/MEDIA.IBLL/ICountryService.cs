using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface ICountryService
    {
        bool Add(string[] names);
        bool Del(string[] ids);
        bool Modify(string[] models);
    }
}
