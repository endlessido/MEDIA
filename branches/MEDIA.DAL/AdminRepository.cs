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
    public class AdminRepository : BaseRepository<Administrator>, IAdminRepository
    {
        [InjectionConstructor]
        public AdminRepository(Entities entities)
            : base(entities)
        { }
    }
}
