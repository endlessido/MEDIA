using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;
using MEDIA.Infrastructure.Repositories;
using MEDIA.IDAL;
using Microsoft.Practices.Unity;

namespace MEDIA.DAL
{
    public class OrderInfoRepository : BaseRepository<OrderInfo>, IOrderInfoRepository
    {
        [InjectionConstructor]
        public OrderInfoRepository(Entities entities)
            : base(entities)
        { }
    }
}
