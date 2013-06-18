using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MEDIA.Model;
using MEDIA.Infrastructure.Repositories;
using MEDIA.IDAL;

namespace MEDIA.DAL
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        [InjectionConstructor]
        public AreaRepository(Entities entities)
            : base(entities)
        { }
    }
}
