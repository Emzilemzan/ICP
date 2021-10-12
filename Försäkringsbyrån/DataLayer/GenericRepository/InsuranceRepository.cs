using DataLayer.InterfaceRepository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    class InsuranceRepository : GenericRepository<Insurance>, IInsuranceRepository
    {

        public InsuranceRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}

