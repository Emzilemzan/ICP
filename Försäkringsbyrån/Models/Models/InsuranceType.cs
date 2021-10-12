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
    /// Our main class for our differnt types of Insurances. 
    /// </summary>
    /// 
    [Table("Försäkringstyp", Schema = "dbo")]
    public class InsuranceType
    {
        [Key]
        public int InsuranceTypeId { get; set; }

   

    }
}
