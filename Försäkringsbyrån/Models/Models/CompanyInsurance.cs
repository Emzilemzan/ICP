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
    [Table("Företagsförsäkring", Schema = "dbo")]
    public class CompanyInsurance : InsuranceType
    {
        [Key]
        public int FFId { get; set; } 
        public DateTime StartDate { get; set; }
        public int Premie { get; set; }
        public string FFName { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public CompanyInsuranceType CIType { get; set; }
        
    }
 
}

