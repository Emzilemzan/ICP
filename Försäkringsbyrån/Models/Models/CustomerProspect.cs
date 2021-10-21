using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("Kundprospekt", Schema = "dbo")]
    public class CustomerProspect
    {
        [Key]
        public int CustomerProspectID { get; set; }
        public Person PersonProspect { get; set; }
    }
}
