using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("FFförsäkringstyp", Schema = "dbo")]
    public class CompanyInsuranceType
    {
        [Key]
        public int FFTypeId { get; set; }
        public string CITypeName { get; set; }

    }
  
}
