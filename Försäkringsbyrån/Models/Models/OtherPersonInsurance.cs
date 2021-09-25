using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
   public class OtherPersonInsurance : InsuranceType
    {
        public int OPIId { get; set; } // ÖFPID!!!!! 
        public string Table { get; set; }
        public int Premie { get; set; }


    }
}
