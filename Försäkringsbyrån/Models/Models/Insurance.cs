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
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public string SerialNumber { get; set; } 
        public InsuredPerson InsuredID { get; set; }
        public SalesMen AgentNo { get; set; }
        public DateTime? PayDate { get; set; }
        public Status InsuranceStatus { get; set; }
        public ICollection<OptionalType> OptionalTypes { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PaymentForm { get; set; } 
        public string Table { get; set; }
        public int Premie { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public string CompanyInsuranceType { get; set; }
        public int BaseAmountValue { get; set; }


        #region InsuranceTypes
        public SAInsurance SAI { get; set; }
        public LifeInsurance LIFE { get; set; }
        public CompanyInsurance COI { get; set; }
        public OtherPersonInsurance OPI { get; set; }

        #endregion

        #region Takers
        public Person PersonTaker { get; set; }
        public Company CompanyTaker { get; set; }
        #endregion

        #region SingedInsurance
        public string InsuranceNumber { get; set; }
        public int? PossibleBaseAmount { get; set; }
        public int? PossibleComisson { get; set; }
        public CustomerProspect CustomerProspectId { get; set; }
        #endregion
    }

    public enum Status
    {
        [Description("Tecknad")] Tecknad,
        [Description("Otecknad")] Otecknad
    }

}

