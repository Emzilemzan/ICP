using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Models;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    /// <summary>
    /// Employee = SalesMen, 
    /// </summary>
    public class EmployeeRepository: GenericRepository<SalesMen>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}
