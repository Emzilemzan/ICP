using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [Table("Säljare", Schema = "dbo")] // anses klar.
    public class SalesMen 
    {
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public string AgentNumber { get; set; }
        public virtual ICollection <Insurance> Insurances { get; set;}

    }
}
