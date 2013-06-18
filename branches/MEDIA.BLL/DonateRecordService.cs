using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.Model;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.BLL.Entity;
namespace MEDIA.BLL
{
    public class DonateRecordService : IDonateRecordService
    {
        public List<DonatedProjectBusiEntity> GetDonationProsByUserID(int userId)
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            List<DonateRecord> list = iDonateRecRepos.Find(model => model.UserId==userId).ToList();
            List<DonatedProjectBusiEntity> resultList = new List<DonatedProjectBusiEntity>();
            foreach (DonateRecord item in list)
            {
                if (resultList.Where(model => model.ProjectName.Equals(item.Goody.DonationProject.ProjectName)).Count() == 0)
                {
                    DonatedProjectBusiEntity entity = new DonatedProjectBusiEntity
                    {
                        ProjectId = item.Goody.DonationProject.ProjectId,
                        ProjectName = item.Goody.DonationProject.ProjectName,
                        ProjectSummary = item.Goody.DonationProject.ProjectSummary,
                        SumDonateFunding = GetSumDonateFunding(userId, item.Goody.DonationProject.ProjectId),
                        CurrencyStr = item.CurrencyStr,
                        ImgUrl = item.Goody.DonationProject.ImgUrl
                    };
                    resultList.Add(entity);
                }
            }
            return resultList;
        }

        private decimal? GetSumDonateFunding(int userId, int? projectId)
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            decimal? result = iDonateRecRepos.Find(model => model.UserId == userId && model.Goody.DonationProject.ProjectId == projectId)
                .Sum(model => model.DonateFunding);
            return result;
        }

        public bool Add(DonateRecord model)
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            return iDonateRecRepos.Add(model, false) >0;
        }

        public int GetGoodyDonatedCount()
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            return iDonateRecRepos.GetQuery().Where(model => model.Id != null).Count();
        }

        public List<Goody> GetGoodyDonated()
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            List<Goody> result = iDonateRecRepos
                .GetQuery()
                .Where(model => model.Id != null)
                .Select(model => model.Goody)
                .ToList();
            return result.Distinct().ToList();
        }

        public List<DonatedRecordBusiEntity> GetDonatorByGoodieId(int gId)
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            return iDonateRecRepos.Find(model => model.Id == gId).Select(model=>new DonatedRecordBusiEntity { 
                CurrencyStr = model.CurrencyStr, 
                DonateDate =model.DonateDate,  DonateFunding= model.DonateFunding, 
                EmailAddress = model.User==null?"":model.User.EmailAddress, IsPayment  =model.IsPayment, 
                LastName = model.User==null?"":model.User.LastName }).ToList();
        }

        public DonateRecord GetModelByIds(int uId, int gId)
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            return iDonateRecRepos.GetSingle(model => model.Id == gId && model.UserId == uId);
        }

        public List<Goody> GetGoodybyOrderId(string orderId)
        {
            IDonateRecordRepository iDonateRecRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            List<Goody> goodyList = new List<Goody>();
            var result = iDonateRecRepos.Find(m => m.OrderId == orderId).ToList();
            foreach (var item in result)
            {
                goodyList.Add(item.Goody);
            }
            return goodyList;
        }
    }
}
