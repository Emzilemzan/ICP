using Models.Models;
using System;
using DataLayer.InterfaceRepository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class CustomerProspectRepository: GenericRepository<CustomerProspect>, ICustomerProspectRepository
    {
        public CustomerProspectRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext => _context;
    }
}
