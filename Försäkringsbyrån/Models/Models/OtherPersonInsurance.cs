using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("ÖFPFörsäkring", Schema = "dbo")]
    public class OtherPersonInsurance
    { 
        [Key]
        public int OPIId { get; set; } 
   
        public string OPIName { get; set; }

    }
}
