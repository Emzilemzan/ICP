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
        public string InsuranceCompany { get; set; }

        #region different baseamount and ackvalue ints to get them in to the insurance, 4 thus in personapplication it can be up to 4.  
        public int BaseAmountValue { get; set; }
        public int BaseAmountValue2 { get; set; }
        public int BaseAmountValue3 { get; set; }
        public int BaseAmountValue4 { get; set; }
        public double AckValue { get; set; }
        public double AckValue2 { get; set; }
        public double AckValue3 { get; set; }
        public double AckValue4 { get; set; }
        #endregion

        #region InsuranceTypes
        public SAInsurance SAI { get; set; }
        public LifeInsurance LIFE { get; set; }
        public CompanyInsurance COI { get; set; }
        public OtherPersonInsurance OPI { get; set; }

        public string TypeName { get; set; }

        #endregion

        #region Takers
        public Person PersonTaker { get; set; }
        public Company CompanyTaker { get; set; }

        public string TakerNbr { get; set; }

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

