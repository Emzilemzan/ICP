using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Sjuk o olycksfalls försäkring ---- SO! 
    /// </summary>
    public class SAInsurance : PersonInsurance
    {
        public int SAID { get; set; } // SOID
        public string PersonalType { get; set; }
        public OptionalType OptionalTypdeId { get; set; }

    }
}
