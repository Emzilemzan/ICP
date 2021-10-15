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
        public ICollection<AckValueVariable> Variables { get; set; }
    } 
}
