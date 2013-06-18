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
    public class SportCategoryService : ISportCategoryService
    {

        public bool Add(string[] names)
        {
            if (names != null)
            {
                ISportCategoryRepository iSportRepos = IoCContext.Container.Resolve<ISportCategoryRepository>();
                foreach (string categoryName in names)
                {
                    if (!string.IsNullOrEmpty(categoryName))
                    {
                        iSportRepos.Add(new Model.SportCategory {  CategoryName = categoryName, IsDel = false });
                    }
                }
                return iSportRepos.SaveChanges() > 0;
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
                ISportCategoryRepository iSportRepos = IoCContext.Container.Resolve<ISportCategoryRepository>();
                foreach (string item in models)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] Keys = item.Split(',');
                        int id;
                        if (int.TryParse(Keys[0], out id))
                        {
                            Model.SportCategory category = iSportRepos.GetSingle(model => model.CategoryId == id);
                            category.CategoryName = Keys[1];
                        }
                    }
                }
                return iSportRepos.SaveChanges() > 0;
            }
            return false;
        }

        public bool Del(string[] ids)
        {
            if (ids != null)
            {
                ISportCategoryRepository iSportRepos = IoCContext.Container.Resolve<ISportCategoryRepository>();
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        int categoryId = int.Parse(id);
                        Model.SportCategory category = iSportRepos.GetSingle(model => model.CategoryId == categoryId);
                        category.IsDel = true;
                    }
                }
                return iSportRepos.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public List<Model.SportCategory> GetUnDels()
        {
            ISportCategoryRepository iSportRepos = IoCContext.Container.Resolve<ISportCategoryRepository>();
            return iSportRepos.Find(model => model.IsDel != true).ToList();
        }
    }
}
