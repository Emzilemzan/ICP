using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataLayer.GenericRepository
{
    class AccessRepository: GenericRepository<UserAccess>, IAccessRepository
    {
        public AccessRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
           get { return _context; }
        }
    }
}
