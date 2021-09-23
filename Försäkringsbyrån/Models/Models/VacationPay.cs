using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{/// <summary>
/// Semesterersättning.
/// </summary>
    public class VacationPay
    {
        public int SEId { get; set; }
        public double AdditionalPercentage { get; set; } // Pålägg %
        public double DiscountPercentage { get; set; } // Avdrag %
        public  int Year { get; set; }


    }
}
