using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("Livförsäkring", Schema = "dbo")]
    public class LifeInsurance : PersonInsurance
    {
        [Key]
        public int LifeID { get; set; }
        public string LifeName { get; set; }

        public LifeInsurance(int id, string name)
        {
            this.LifeID = id;
            this.LifeName = name;
        }

    }
}
