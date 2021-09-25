using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class AckValueVariable
    {
        public int AckValueID { get; set; }
        public  DateTime Date {get; set;} 
        public double AckValuevariable { get; set; }
       public LifeInsurance LIFEID {get; set;}
        public OptionalType OptionalTypeId {get; set;}

    }
}
