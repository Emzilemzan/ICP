using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string PostalCode { get; set; }
        public string StreetAdress  { get; set; }
        public string City { get; set; }
        public int TaxRate { get; set; }

    }
}
