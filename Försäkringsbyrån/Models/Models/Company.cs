using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Companies that takes an insurance is also a InsuranceTaker. 
    /// </summary>
   public class Company : InsuranceTaker
    {
        public int OrganizationNumber { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string TelephoneNbr { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set;}

    }
}
