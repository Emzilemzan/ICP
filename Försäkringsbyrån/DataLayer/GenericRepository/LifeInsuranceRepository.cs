using DataLayer.InterfaceRepository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class LifeInsuranceRepository: GenericRepository<LifeInsurance>, ILifeInsuranceRepository
    {
        public LifeInsuranceRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext => _context;
    }
}
