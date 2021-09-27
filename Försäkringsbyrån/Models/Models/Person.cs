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
        public  string Lastname { get; set; }
        public string Firstname { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        public string DiallingCodeHome { get; set; }
        public string DiallingCodeWork { get; set; }
        public string TelephoneNbrHome { get; set; }
        public string TelephoneNbrWork { get; set; }
        public string EmailOne { get; set; }
        public string EmailTwo { get; set; }

    }
}
