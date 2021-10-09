using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    /// <summary>
    /// TillvalsTyp. 
    /// </summary>
    /// 
    [Table("Tillvalstyp", Schema = "dbo")]
    public class OptionalType
    {
        [Key]
        public int OptionalTypeId { get; set; }
        public string OptionalName { get; set; }
        public double BaseAmount { get; set; }
        public DateTime Date { get; set; }

        public OptionalType(int optionaltypeid, string optionalname)
        {
            this.OptionalTypeId = optionaltypeid;
            this.OptionalName = optionalname;
        }
    } 
}
