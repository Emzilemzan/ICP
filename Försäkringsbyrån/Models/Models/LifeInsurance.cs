using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("Livförsäkring", Schema = "dbo")]
    public class LifeInsurance
    {
        [Key]
        public int LifeID { get; set; } 
        public string LifeName { get; set; }
        public ICollection<BaseAmount> Amounts { get; set; }
        public ICollection<AckValueVariable> Variables { get; set; }
    }
}
