using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Provision
    /// </summary>
   public class Comission
    {
        public int ComissionID { get; set; }
        public DateTime Date { get; set; }
        //public SalesMen AgentNumber { get; set; }
        public ComissionShare PaId { get; set; }
        public VacationPay SEId { get; set; }
        public SignedInsurance InsuranceNumber { get; set; }
        public InsuranceApplication SerialNumber { get; set; }

    }
}
