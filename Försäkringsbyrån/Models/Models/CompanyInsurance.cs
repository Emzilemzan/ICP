using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
   public class CompanyInsurance : InsuranceType
    {
        public int FFId { get; set; } 
        public DateTime StartDate { get; set; }
        public int Premie { get; set; }
        public string FFName { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        

    }
}
