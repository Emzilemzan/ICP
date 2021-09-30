using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{/// <summary>
/// Semesterersättning.
/// </summary>
/// 
    [Table("Semesterersättning", Schema = "dbo")] 
    public class VacationPay
    {
        [Key]
        public int SEId { get; set; } 
        public double AdditionalPercentage { get; set; } 
        public  int Year { get; set; }


    }
}
