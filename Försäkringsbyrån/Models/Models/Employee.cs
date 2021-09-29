using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Employee
    {
        public int EmploymentNo { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public RoleType Role { get; set; }

        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int Postalcode { get; set; }
        public double TaxRate { get; set; }
        public double FormOfEmployment { get; set; }
        public Nullable < int > AgentNo { get; set; }
        
        
        public Employee()
        {
        }
    }
    public enum RoleType
    {
        Försäljningsassistent,
        Innesäljare,
        Utesäljare,
        Ekonomiassistent,
        Försäljningschef,
        VD
    }
}
