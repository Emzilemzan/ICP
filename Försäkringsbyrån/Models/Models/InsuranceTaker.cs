using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    /// <summary>
    /// Försäkringstagare 
    /// </summary>
    [Table("Försäkringstagare", Schema = "dbo")]
    public class InsuranceTaker
    {
        [Key]
        public int InsuranceTakerId { get; set; }

        public virtual ICollection <InsuredPerson> InsuredPersons { get; set; }

        public virtual ICollection<Insurance> Insurances { get; set; }

        

    }
}
