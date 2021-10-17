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
    /// Grundbelopp för både tyillvalstyper och Livförsäkring.
    /// </summary>
    [Table("Grundbelopp", Schema = "dbo")] // Anses klar. 
    public class BaseAmount
    {
        [Key]
        public int BaseAmountId { get; set; }
        public DateTime Date { get; set; }
        public int Baseamount{ get; set; }
        public LifeInsurance LIFEID { get; set; }
    }
}
