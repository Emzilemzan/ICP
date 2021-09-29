using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{ /// <summary>
 /// Grundbeloppstabell.
/// </summary>
    public class BaseAmountTabel
    {
        public int BaseAmountTId { get; set; }
        public DateTime Date { get; set; } 
        public double BaseAmount { get; set; }
        public double AckValue { get; set; }

        public PersonInsuranceType insuranceType { get; set; }

    }
}
