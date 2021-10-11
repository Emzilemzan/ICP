using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [Table("Säljare", Schema = "dbo")]
    public class SalesMen
    {
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public int AgentNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int Postalcode { get; set; }
        public double TaxRate { get; set; }
        public virtual ICollection <Insurance> Insurances { get; set;}
    }
}
