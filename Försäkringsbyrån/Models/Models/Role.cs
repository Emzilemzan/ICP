using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Befattning
    /// </summary>
  public  class Role
    {
        public int RoleId { get; set; }
        public Employee EmployeeId { get; set; }

        public bool CEO { get; set; }

        public bool SalesAssistent { get; set; }

        public bool SalesManager { get; set; }

        public bool FieldSalesMen { get; set; }

        public bool OfficeSalesMen { get; set; }

        public bool EconomyAssistent { get; set; }
    }
}
