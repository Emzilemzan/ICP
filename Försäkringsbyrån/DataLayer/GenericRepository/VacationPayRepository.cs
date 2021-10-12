using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.InterfaceRepository;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class VacationPayRepository: GenericRepository<VacationPay>, IVacationPayRepository
    {
        public VacationPayRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext => _context;
    }
}
