using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.InterfaceRepository;
using Models.Models;

namespace DataLayer.GenericRepository
{
    public class OtherPersonInsuranceRepository: GenericRepository<OtherPersonInsurance>, IOtherPersonInsuranceRepository
    {
        public OtherPersonInsuranceRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext => _context;
    }
}