using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Models;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}
