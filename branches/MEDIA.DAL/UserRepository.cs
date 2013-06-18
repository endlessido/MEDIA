using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Infrastructure.Repositories;
using MEDIA.IDAL;
using MEDIA.Model;
using Microsoft.Practices.Unity;

namespace MEDIA.DAL
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        [InjectionConstructor]
        public UserRepository(Entities entities)
            : base(entities)
        { }
    }
}
