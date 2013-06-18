using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Infrastructure.Repositories;
using MEDIA.Model;
using MEDIA.IDAL;
using Microsoft.Practices.Unity;

namespace MEDIA.DAL
{
    public class DonationProjectRepository : BaseRepository<DonationProject>, IDonationProjectRepository
    {
       [InjectionConstructor]
        public DonationProjectRepository(Entities entities)
            : base(entities)
        { }
    }
}
