using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("Personförsäkring", Schema = "dbo")]
    public class PersonInsurance : InsuranceType
    {
        [Key]
        public int PFId { get; set; }
    }
}
