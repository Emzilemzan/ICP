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
    /// ProvisionsANDEL.
    /// </summary>
    /// 
    [Table("Provisionsandel", Schema = "dbo")]
    public class ComissionShare
    {
        [Key]
        public int PAId { get; set; }
        public int CalenderYear { get; set; }
        public int TotalMinAckValue { get; set; }
        public int TotalMaxAckValue { get; set; }
        public int CommissionShareChildren { get; set; }
        public int ComissionShareAdults { get; set; }

         
    }
}
