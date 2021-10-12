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
    [Table("Företagsförsäkring", Schema = "dbo")]
    public class CompanyInsurance 
    {
        [Key]
        public int FFId { get; set; } 
        public string COIName { get; set; }
        
    }
 
}

