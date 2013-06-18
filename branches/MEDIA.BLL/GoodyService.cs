using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.Model;
namespace MEDIA.BLL
{
    public class GoodyService : IGoodyService
    {
        public List<Goody> GetGoodysByProjectId(int projectId)
        {
            IGoodyRepository iGoodyRepos = IoCContext.Container.Resolve<IGoodyRepository>();
            List<Goody> list = iGoodyRepos.GetAll()
                .Where(model=>model.ProjectId.Equals(projectId))
                .ToList();
            return list;
        }

        public Goody GetGoodyById(int gId)
        {
            IGoodyRepository iGoodyRepos = IoCContext.Container.Resolve<IGoodyRepository>();
            return iGoodyRepos.GetSingle(model => model.Id == gId);
        }

        public List<Goody> GetGoodysByIds(string goodyIds)
        {
           List<Goody> list = new List<Goody>();
           string[] result = goodyIds.Split(',');
           foreach (var item in result)
           {
               if (!string.IsNullOrEmpty(item))
               {
                   list.Add(GetGoodyById(int.Parse(item)));
               }
           }
           return list;
        }

        public decimal? GetSumPrice(string goodyIds)
        {
            decimal? SumPrice = GetGoodysByIds(goodyIds).Sum(m => m.Price);
            return SumPrice;
        }
    }
}
