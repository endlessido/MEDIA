using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Repositories;
using MEDIA.Model;
using Microsoft.Practices.Unity;

namespace MEDIA.DAL
{
    public class DonateRecordRepository : BaseRepository<DonateRecord>, IDonateRecordRepository
    {
        [InjectionConstructor]
        public DonateRecordRepository(Entities entities)
            : base(entities)
        { }
    }
}
