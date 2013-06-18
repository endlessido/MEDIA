using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;
using MEDIA.BLL.Entity;

namespace MEDIA.IBLL
{
    public interface IOrderInfoService
    {
        bool Add(OrderInfo model);
        bool ModifyPayment(string orderId,string pm);
        OrderInfo GetModel(string orderId);
        List<OrderBusiEntity> GetOrdersbyUserId(int userId);
        List<OrderBusiEntity> GetOrdersbyProjectId(int projectId);
        int UnPaymentOrdersCount();
        int CompletedOrdersCount();
        List<OrderBusiEntity> GetUnPaymentOrders();
        List<OrderBusiEntity> GetCompletedOrders();
        List<OrderBusiEntity> GetReceivedOrders(int userId);
    }
}
