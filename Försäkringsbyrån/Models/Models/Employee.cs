using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Models.Models
{
    [Table("Anställd", Schema = "dbo")]
    public class Employee
    {
        [Key]
        public int EmploymentNo { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int Postalcode { get; set; }
        public double TaxRate { get; set; }
        public double FormOfEmployment { get; set; }
        public virtual ICollection <Access> Accesses { get; set; } // is used to modified and alowed to be overriden by another class who is inheriting. 

        public Employee()
        {
        }
    }
  
}
