using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [Table("Försäkring", Schema = "dbo")]
    public class Insurance
    {
        [Key]
        public int SerialNumber { get; set; }
        public InsuredPerson InsuredID { get; set; }
        public InsuranceType InsuranceTypeId { get; set; }
        public SalesMen AgentNo { get; set; }
        public virtual InsuranceTaker Taker { get; set; }
        public Nullable<int> InsuranceNumber { get; set; }
        public Nullable<DateTime> PayDate { get; set; }
        public Nullable<int> PossibleBaseAmount { get; set; }
        public Nullable<int> PossibleComisson { get; set; }
        public Status Signed { get; set; }
        public CustomerProspect CustomerProspectId { get; set; }


    }
    public enum Status
    {
        [Description("Tecknad")] Tecknad,
        [Description("Otecknad")] Otecknad
    }

}

