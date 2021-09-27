using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class InsuredPersonRepository : GenericRepository<InsuredPerson>, IInsuredPersonRepository
    {

        public InsuredPersonRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}
