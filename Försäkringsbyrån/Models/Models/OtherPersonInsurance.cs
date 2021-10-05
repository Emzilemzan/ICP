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
    public class OtherPersonInsurance : InsuranceType
    {
        [Key]
        public int OPIId { get; set; } 
        public string Table { get; set; }
        public int Premie { get; set; }


    }
}
