using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface IAreaService
    {
        bool Add(string[] names,int countryId);
        bool Del(string[] ids);
        bool Modify(string[] models);
        List<Area> GetUnDelAreas(int countryId);
    }
}
