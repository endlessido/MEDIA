using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.IDAL;
using MEDIA.Model;
using MEDIA.BLL.Entity;

namespace MEDIA.BLL
{
    public class OrderInfoService : IOrderInfoService
    {
        public bool Add(OrderInfo model)
        {
            if (model != null)
            {
                IOrderInfoRepository iRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
                return iRepos.Add(model, false) > 0;
            }
            else 
            {
                return false;
            }
        }

        public bool ModifyPayment(string orderId, string pm)
        {
            IOrderInfoRepository iRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            OrderInfo model = iRepos.GetSingle(m => m.OrderId.Equals(orderId));
            foreach (var item in model.DonateRecords)
            {
                item.IsPayment = true;
            }
            model.IsPayment = true;
            model.PayType = pm;
            return iRepos.SaveChanges() >0;
        }

        public OrderInfo GetModel(string orderId)
        {
            IOrderInfoRepository iRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            return iRepos.GetSingle(m => m.OrderId.Equals(orderId));
        }

        public List<OrderBusiEntity> GetOrdersbyUserId(int userId)
        {
            IOrderInfoRepository iRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            List<OrderInfo> orderList = iRepos.Find(m => m.UserId == userId).ToList();
            List<OrderBusiEntity> reslut = new List<OrderBusiEntity>();
            foreach (OrderInfo item in orderList)
            {
                OrderBusiEntity model = new OrderBusiEntity { 
                    CurrencyStr = item.CurrencyStr, 
                    IsPayment = item.IsPayment, 
                    OrderDate =item.OrderDate.Value, 
                    TotalPrice = item.TotalPrice,
                     OrderId = item.OrderId};
                model.Goodies = GetGoodies(item);
                model.UnitPrices = GetUnitPrices(item);
                reslut.Add(model);
            }
            return reslut;
        }

        public int UnPaymentOrdersCount()
        {
            IOrderInfoRepository iRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            return iRepos.Find(m => m.IsPayment == false).Count();
        }

        public int CompletedOrdersCount()
        {
            IOrderInfoRepository iRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            return iRepos.Find(m => m.IsPayment == true).Count();
        }

        public List<OrderBusiEntity> GetUnPaymentOrders()
        {
            IOrderInfoRepository iOrderRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            List<string> orderIds = iOrderRepos.Find(m => m.IsPayment == false).Select(m => m.OrderId).Distinct().ToList();
            return GetOrderBusiEntityByIds(iOrderRepos, orderIds);
        }

        private List<OrderBusiEntity> GetOrderBusiEntityByIds(IOrderInfoRepository iOrderRepos, List<string> orderIds)
        {
            List<OrderInfo> orderList = iOrderRepos.Find(m => orderIds.Contains(m.OrderId)).OrderByDescending(m => m.OrderDate).ToList();
            List<OrderBusiEntity> reslut = new List<OrderBusiEntity>();
            foreach (OrderInfo item in orderList)
            {
                OrderBusiEntity model = new OrderBusiEntity
                {
                    CurrencyStr = item.CurrencyStr,
                    IsPayment = item.IsPayment,
                    OrderDate = item.OrderDate.Value,
                    TotalPrice = item.TotalPrice,
                    OrderId = item.OrderId,
                    Address = item.User ==null? string.Empty : item.User.Address,
                    CountryName = item.User ==null? string.Empty : item.User.CountryName,
                    FirstName = item.User ==null? string.Empty : item.User.FirstName,
                    EmailAddress = item.User == null ? string.Empty : item.User.EmailAddress,
                    Zip = item.User == null ? string.Empty : item.User.Zip,
                    Town = item.User == null ? string.Empty : item.User.Town,
                    PayType = item.PayType
                };

                foreach (var record in item.DonateRecords)
                {
                   model.ProjecName = record.DonationProject.ProjectName;
                   break;
                }

                if (item.User != null)
                {
                    model.Goodies = GetGoodies(item);
                    model.UnitPrices = GetUnitPrices(item);
                }
                reslut.Add(model);
            }
            return reslut;
        }

        public List<OrderBusiEntity> GetOrdersbyProjectId(int projectId)
        {
            IOrderInfoRepository iOrderRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            IDonateRecordRepository iRecordRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            List<string> orderIds = iRecordRepos.Find(m => m.ProjectId == projectId).Select(m=>m.OrderId).Distinct().ToList();
            return GetOrderBusiEntityByIds(iOrderRepos, orderIds);
        }

        public List<OrderBusiEntity> GetCompletedOrders()
        {
            IOrderInfoRepository iOrderRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            List<string> orderIds = iOrderRepos.Find(m => m.IsPayment == true).Select(m => m.OrderId).Distinct().ToList();
            return GetOrderBusiEntityByIds(iOrderRepos, orderIds);
        }

        private string GetGoodies(OrderInfo item)
        {
            string goodies = string.Empty;
            if (item.User != null)
            {
                foreach (var donateRecord in item.DonateRecords)
                {
                    if (donateRecord.Goody != null)
                    {
                        goodies += donateRecord.Goody.Title + ";";
                    }
                }
            }
            return goodies;
        }

        private string GetUnitPrices(OrderInfo item)
        {
            string unitPrices = string.Empty;
            if (item.User != null)
            {
                foreach (var donateRecord in item.DonateRecords)
                {
                    if (donateRecord.Goody != null)
                    {
                        unitPrices += string.Format("{0:N2}", donateRecord.Goody.Price.Value) + ";";
                    }
                }
            }
            return unitPrices;
        }

        public List<OrderBusiEntity> GetReceivedOrders(int userId)
        {
            IOrderInfoRepository iOrderRepos = IoCContext.Container.Resolve<IOrderInfoRepository>();
            IDonateRecordRepository iRecordRepos = IoCContext.Container.Resolve<IDonateRecordRepository>();
            List<string> orderIds = iRecordRepos.Find(m => m.DonationProject.UserId == userId && m.IsPayment == true).Select(m => m.OrderId).Distinct().ToList();
            return GetOrderBusiEntityByIds(iOrderRepos, orderIds);
        }
    }
}
