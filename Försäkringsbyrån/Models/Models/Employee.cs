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
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public string EmploymentNo { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Role Roles { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string Postalcode { get; set; }
        public double TaxRate { get; set; }
        public double FormOfEmployment { get; set; }
        public Access Accesses { get; set; } 
        public SalesMen SalesMen { get; set; }
    }
  
}
