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
    /// <summary>
    /// Sjuk o olycksfalls försäkring ---- SO! 
    /// </summary>
    /// 
    [Table("SOförsäkring", Schema = "dbo")]
    public class SAInsurance : PersonInsurance
    {
        [Key]
        public int SAID { get; set; } // SOID

        public string SAInsuranceType { get; set; }

        public SAInsurance(int id, string s)
        {
            this.SAID = id;
            this.SAInsuranceType = s;
        }

        public virtual ICollection<OptionalType> OptionalTypes { get; set; }
        

    }
   
}
