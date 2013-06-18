using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.Model;
using System.Web;
using System.Web.Caching;
namespace MEDIA.BLL
{
    public class DonationProjectService : IDonationProjectService
    {
        public bool Add(DonationProject newModel,string goodyName, string goodyDesc, string goodyNum, string goodyPrice, string ddlCurrency)
        {
            if (null == newModel)
            {
                return false;
            }
            else
            {
                IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
                AddProjectGoody(goodyName, goodyDesc, goodyNum, goodyPrice, ddlCurrency, newModel);
                int count = iProjectRepos.Add(newModel, false);
                return count > 0;
            }
        }

        internal void AddProjectGoody(string goodyName, string goodyDesc, string goodyNum, string goodyPrice, string ddlCurrency,DonationProject projectModel)
        {
            
            if (!string.IsNullOrEmpty(goodyName)
                && !string.IsNullOrEmpty(goodyDesc)
                && !string.IsNullOrEmpty(goodyNum)
                && !string.IsNullOrEmpty(goodyPrice)
                && !string.IsNullOrEmpty(ddlCurrency)
               )
            {
               
                string[] arrayName = goodyName.Split(',');
                string[] arrayDesc = goodyDesc.Split(',');
                string[] arrayNum = goodyNum.Split(',');
                string[] arrayPrice = goodyPrice.Split(',');

                for (int i = 0; i < arrayNum.Length; i++)
                {
                    int num = 0;
                    bool isLimited = false;
                    if (int.TryParse(arrayNum[i], out num))
                    {
                        isLimited = true;
                    }
                    Goody model = new Goody
                    {

                        Num = num,
                        Price = decimal.Parse(arrayPrice[i]),
                        Title = arrayName[i],
                        CurrencyStr = ddlCurrency,
                        ProjectId = projectModel.ProjectId,
                        Description = arrayDesc[i],
                        SaleNum = 0,
                        IsLimit = isLimited
                    };
                    projectModel.Goodies.Add(model);
                }
            }
        }

        public DonationProject GetModelById(int projectId)
        {
            if (projectId > 0)
            {
                IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
                return iProjectRepos.GetSingle(model => model.ProjectId.Equals(projectId));
            }
            else
            {
                return null;
            }
        }

        public int GetEndedCount()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            IQueryable<DonationProject> list = iProjectRepos.Find(model => model.IsFinished == true);
            return list.Count();
        }

        public int GetGoingCount()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            IQueryable<DonationProject> list = iProjectRepos.Find(model => model.IsFinished == false && model.IsCheck ==true);
            return list.Count();
        }

        public List<DonationProject> GetNewlyProject()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            List<DonationProject> list = iProjectRepos
                .GetQuery()
                .Where(model=>model.IsCheck == false && model.IsFinished == false)
                .OrderByDescending(model => model.CreateDate)
                .ToList();
            return list;
        }

        public bool ReceiveFunding(string goodyIds, int projectId, int userId, string orderId)
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            DonationProject project =  iProjectRepos.GetSingle(model => model.ProjectId.Equals(projectId));
            if (null == project)
            {
                return false;
            }
            else
            {
                IEnumerable<Goody> goodys = project.Goodies.Where(goody => goodyIds.Contains(goody.Id.ToString()));
                foreach (Goody item in goodys)
                {
                    item.SaleNum++;
                    project.User.DonateRecords.Add(new DonateRecord { Id = item.Id, OrderId= orderId, ProjectId= projectId, CurrencyStr = item.CurrencyStr, DonateDate = System.DateTime.Now, DonateFunding = item.Price, IsPayment = false, UserId = userId });
                }
                return iProjectRepos.Update(project, false) > 0;
            }
        }

        public List<DonationProject> GetFoundedProjectsByUserId(int userId)
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            return iProjectRepos.Find(model => model.UserId ==userId).ToList();
        }

        public bool IsProjectNameExist(string projectName)
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            return iProjectRepos.GetSingle(model => model.ProjectName.Equals(projectName)) == null ? false : true;
        }

        public List<DonationProject> GetCheckProject()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            return iProjectRepos.Find(model => model.IsCheck == true).ToList();
        }

        public List<DonationProject> GetAllProject()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            return iProjectRepos.GetAll().ToList();
        }

        public List<DonationProject> GetRecentRandomProject(int count)
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            List<DonationProject> result = iProjectRepos
                .GetQuery()
                .Where(m=>m.IsCheck == true && m.IsFinished == false)
                .OrderByDescending(model => model.CreateDate)
                .Take(count)
                .ToList();
            return result;
        }

        public List<DonationProject> GetOnGoingProject()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            List<DonationProject> list = iProjectRepos
                .GetQuery()
                .Where(model => model.IsCheck == true && model.IsFinished == false)
                .OrderByDescending(model => model.CreateDate)
                .ToList();
            return list;
        }

        public List<DonationProject> GetEndingProject()
        {
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            List<DonationProject> list = iProjectRepos
                .GetQuery()
                .Where(model => model.IsFinished == true)
                .OrderByDescending(model => model.CreateDate)
                .ToList();
            return list;
        }

        public List<DonationProject> GetSearchList(string serachName, string categoryName, string regionName)
        {
             IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
             IList<DonationProject> list = null;
             if (HttpRuntime.Cache["ProjectList"] == null)
             {
                 list = iProjectRepos.Find(m=>m.IsCheck ==true && m.IsFinished ==false).ToList();
                 HttpRuntime.Cache.Add("ProjectList", list, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null
                     );
             }
             else
             {
                 list = HttpRuntime.Cache["ProjectList"] as IList<DonationProject>;
                 
             }
            if (!string.IsNullOrEmpty(serachName))
            {
                list = list.Where(model => model.ProjectName.ToLower().Contains(serachName.ToLower()) || model.ProjectSummary.ToLower().Contains(serachName.ToLower())).ToList(); ;
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                list = list.Where(model => categoryName.Contains(model.CategoryName)).ToList();
            }
            if (!string.IsNullOrEmpty(regionName))
            {
                list = list.Where(model => regionName.Contains(model.AreaName)).ToList();
            }
            return list.ToList();
        }

        public bool ReceiveFunding(decimal currency, string orderID, out int? projectId)
        {
            IDonateRecordRepository iRecordRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            DonateRecord model = iRecordRepos.Find(m => m.OrderId == orderID).FirstOrDefault();
            projectId = model.ProjectId;
            IDonationProjectRepository iProjectRepos = IoCContext.Container.Resolve<IDonationProjectRepository>();
            DonationProject project = iProjectRepos.GetSingle(m => m.ProjectId == model.ProjectId);
            project.ReceivedFunding += currency;
            project.DonateNum++;
            return iProjectRepos.SaveChanges() > 0;
        }
    }
}
