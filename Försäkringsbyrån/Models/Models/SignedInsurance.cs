using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Tecknade försäkringar! :)
    /// </summary>
   public class SignedInsurance
    {
        public int InsuranceNumber { get; set; }
        public DateTime PayDate { get; set; }
        public int PossibleBaseAmount { get; set; }
        public int PossibleComisson { get; set; }
        public virtual InsuranceApplication SerialNumber { get; set; }
        public virtual CustomerProspect CustomerProspectId { get; set; }
    }
}
