using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// ProvisionsANDEL.
    /// </summary>
   public class ComissionShare
    {
        public int PAId { get; set; }
        public int CalenderYear { get; set; }
        public int TotalMinAckValue { get; set; }
        public int TotalMaxAckValue { get; set; }
        public int CommissionShareChildren { get; set; }
        public int ComissionShareAdults { get; set; }


    }
}
