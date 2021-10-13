using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{

    [Table("Ackvärdevariabel", Schema = "dbo")]
    public class AckValueVariable
    {
        [Key]
        public int AckValueID { get; set; }
        public  DateTime Date {get; set;} 
        public double AckValue { get; set; }
        public LifeInsurance LIFEID {get; set;}
        public OptionalType OptionalTypeId {get; set;}
     

    }
}
