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
    public class Employee  /*IataErrorInfo*/
    {
        
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public int EmploymentNo { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string Postalcode { get; set; }
        public double TaxRate { get; set; }
        public double FormOfEmployment { get; set; }
        

        #region role bools. 
        public bool CEO { get; set; }

        public bool SalesAssistent { get; set; }

        public bool SalesManager { get; set; }

        public bool FieldSalesMen { get; set; }

        public bool OfficeSalesMen { get; set; }

        public bool EconomyAssistent { get; set; }
        #endregion

        #region access bools.
        public bool Search { get; set; }
        public bool Insurances { get; set; }
        public bool StatisticsAndProspects { get; set; }
        public bool EmployeeManagement { get; set; }
        public bool BasicData { get; set; }
        public bool Commission { get; set; }
        
        
        #endregion
    }
}


