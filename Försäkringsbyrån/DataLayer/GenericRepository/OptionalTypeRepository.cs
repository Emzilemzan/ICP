using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.InterfaceRepository;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
   public class OptionalTypeRepository: GenericRepository<OptionalType>, IOptionalTypeRepository 
    {
        public OptionalTypeRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}
