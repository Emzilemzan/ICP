using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class BaseAmountTableRepository : GenericRepository<BaseAmountTabel>, IBaseAmountTableRepository
    {
        public BaseAmountTableRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
    
}
