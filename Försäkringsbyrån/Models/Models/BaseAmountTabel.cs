using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{ /// <summary>
  /// Grundbeloppstabell.
  /// </summary>
  /// 
    [Table("Grundbeloppstabell", Schema = "dbo")]
    public class BaseAmountTabel
    {
        [Key]
        public int BaseAmountTId { get; set; }
        public DateTime Date { get; set; } 
        public double BaseAmount { get; set; }
        public double AckValue { get; set; } 
        public SAInsurance SAID { get; set; } 

    }
}
