using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;

namespace MEDIA.BLL
{
    public class CountryService : ICountryService
    {
        public bool Add(string[] names)
        {
            if (names != null)
            {
                ICountryRepository iCountryRepos = IoCContext.Container.Resolve<ICountryRepository>();
                foreach (string countryName in names)
                {
                    if (!string.IsNullOrEmpty(countryName))
                    {
                        iCountryRepos.Add(new Model.Country { CountryName = countryName, IsDel = false });
                    }
                }
                return iCountryRepos.SaveChanges() > 0;
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
                ICountryRepository iCountryRepos = IoCContext.Container.Resolve<ICountryRepository>();
                foreach (string item in models)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] Keys =  item.Split(',');
                        int id ;
                        if (int.TryParse(Keys[0], out id))
                        {
                            Model.Country country = iCountryRepos.GetSingle(model => model.CountryId == id);
                            country.CountryName = Keys[1];
                        }
                    }
                }
               return iCountryRepos.SaveChanges() > 0;
            }
            return false;
        }

        public bool Del(string[] ids)
        {
            if (ids != null)
            {
                ICountryRepository iCountryRepos = IoCContext.Container.Resolve<ICountryRepository>();
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int countryId = int.Parse(id);
                        Model.Country country = iCountryRepos.GetSingle(model => model.CountryId == countryId);
                        country.IsDel = true;
                    }
                }
                return iCountryRepos.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
