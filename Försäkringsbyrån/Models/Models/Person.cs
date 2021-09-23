using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Persons who takes an insurance is also the InsuranceTaker. 
    /// </summary>
    public class Person : InsuranceTaker
    {
        public string SocialSecurityNumber { get; set; }
        public  string Surnamne { get; set; }
        public string Firstname { get; set; }
        public string StreetAdress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }



    }
}
