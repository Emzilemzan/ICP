using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.Models
{
    /// <summary>
    /// Olika behörigheter i systemet. 
    /// </summary>
    [Table("Behörighet", Schema = "dbo")]
    public class Access
    {
        [Key]
        public int AccessId { get; set; }
        public Employee EmpId { get; set; }
        public bool Search { get; set; }
        public bool Insurances { get; set; }
        public bool StatisticsAndProspects { get; set; }
        public bool EmployeeManagement { get; set; }
        public bool BasicData { get; set; }
        public bool ComissionData { get; set; }

    }
}
