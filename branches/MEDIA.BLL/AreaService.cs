using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Ioc;
using MEDIA.IDAL;
using Microsoft.Practices.Unity;

namespace MEDIA.BLL
{
    public class AreaService : IAreaService
    {
        public bool Add(string[] names, int countryId)
        {
            if (names != null)
            {
                IAreaRepository iAreaRepos = IoCContext.Container.Resolve<IAreaRepository>();
                foreach (string areaName in names)
                {
                    if (!string.IsNullOrEmpty(areaName))
                    {
                        iAreaRepos.Add(new Model.Area { AreaName = areaName, CountryId = countryId, IsDel = false });
                    }
                }
                return iAreaRepos.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool Modify(string[] models)
        {
            if (models != null)
            {
                IAreaRepository iAreaRepos = IoCContext.Container.Resolve<IAreaRepository>();
                foreach (string item in models)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] Keys = item.Split(',');
                        int id;
                        if (int.TryParse(Keys[0], out id))
                        {
                            Model.Area area = iAreaRepos.GetSingle(model => model.AreaId == id);
                            area.AreaName = Keys[1];
                        }
                    }
                }
                return iAreaRepos.SaveChanges() > 0;
            }
            return false;
        }

        public bool Del(string[] ids)
        {
            if (ids != null)
            {
                IAreaRepository iAreaRepos = IoCContext.Container.Resolve<IAreaRepository>();
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int areaId = int.Parse(id);
                        Model.Area area = iAreaRepos.GetSingle(model => model.AreaId == areaId);
                        area.IsDel = true;
                    }
                }
                return iAreaRepos.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public List<Model.Area> GetUnDelAreas(int countryId)
        {
            IAreaRepository iAreaRepos = IoCContext.Container.Resolve<IAreaRepository>();
            return iAreaRepos.Find(model => model.CountryId == countryId && model.IsDel != true).ToList();
        }
    }
}
