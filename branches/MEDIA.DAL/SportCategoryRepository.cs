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
    public class SportCategoryRepository : BaseRepository<SportCategory>, ISportCategoryRepository
    {
        [InjectionConstructor]
        public SportCategoryRepository(Entities entities)
            : base(entities)
        { }
    }
}
