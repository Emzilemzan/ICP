using Models.Models;
using System;
using DataLayer.InterfaceRepository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
  public class BaseAmountRepository: GenericRepository<BaseAmount>, IBaseAmountRepository
    {
        public BaseAmountRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}
