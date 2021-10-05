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
    /// Befattning
    /// </summary>
   // [Table("Befattning", Schema = "dbo")]
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Employee EmployeeId { get; set; }

    }
}
