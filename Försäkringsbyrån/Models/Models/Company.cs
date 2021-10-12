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
    /// Companies that takes an insurance is also a InsuranceTaker. 
    /// </summary>
    [Table("Företag", Schema = "dbo")]
    public class Company
    {
        [Key, DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)] 
        public string OrganizationNumber { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string DiallingCode { get; set; }
        public string TelephoneNbr { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set;}
        public virtual ICollection<InsuredPerson> InsuredPersons { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }


    }
}
