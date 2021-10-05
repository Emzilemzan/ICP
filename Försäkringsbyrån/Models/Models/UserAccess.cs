using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Models
{
    [Table("Användarbehörighet", Schema = "dbo")]
    public class UserAccess
    {
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public string Username { get; set; }
        public bool Search { get; set; }
        public bool Insurances { get; set; }
        public bool StatisticsAndProspects { get; set; }
        public bool EmployeeManagement { get; set; }
        public bool BasicData { get; set; }
        public bool Commission { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        

    }
}
