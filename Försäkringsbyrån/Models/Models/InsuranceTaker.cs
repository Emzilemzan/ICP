using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Försäkringstagare 
    /// </summary>
   public class InsuranceTaker
    {
        public int InsuranceTakerId { get; set; }
        public string InsuranceTakerName { get; set; }

        public virtual ICollection <InsuredPerson> InsuredPersons { get; set; }

        public virtual ICollection<InsuranceApplication> InsuranceApplications { get; set; }

    }
}
