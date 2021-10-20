using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Seed
{
    /// <summary>
    /// All methods here will only fire when the affected models are empty in the database
    /// </summary>
    public class SeedDatabase
    {
        public SeedDatabase()
        {
            CreateOptionalTypes();
            CreateSAInsurances();
            CreateOtherPersonInsurance();
            CreateCompanyInsurance();
            CreateLifeInsurance();
            GenerateSM();
            GenerateUA();
            GenerateBA();
            GenerateBAT();
            GenerateAV();
            GeneratePersonTaker();
            GenerateCompanyTaker();
            GenerateInsuredPerson();
            GenerateInsurances();

        }

        private void GenerateSM()
        {
            List<SalesMen> salesMens = new List<SalesMen>
            {
                new SalesMen()
                {
                    AgentNumber = 2547,
                    Firstname = "Irene",
                    Lastname = "Johansson",
                    StreetAddress = "Krokusstigen 12",
                    City = "Borås",
                    Postalcode = 50330,
                    TaxRate = 28,
                },
                new SalesMen()
                {
                    AgentNumber = 6423,
                    Firstname = "Karin",
                    Lastname = "Sundberg",
                    StreetAddress = "Borgmästarevägen 73",
                    City = "Allingsås",
                    Postalcode = 44141,
                    TaxRate = 30,
                },
                new SalesMen()
                {
                    AgentNumber = 2447,
                    Firstname = "Vigo",
                    Lastname = "Persson",
                    StreetAddress = "Tallgatan 17",
                    City = "Björvik",
                    Postalcode = 43245,
                    TaxRate = 29,
                },
                new SalesMen()
                {
                    AgentNumber = 5836,
                    Firstname = "Birgitta",
                    Lastname = "Frisk",
                    StreetAddress = "Björkvägen 17",
                    City = "Jönköping",
                    Postalcode = 55315,
                    TaxRate = 31,
                },
                new SalesMen()
                {
                    AgentNumber = 2264,
                    Firstname = "Boris",
                    Lastname = "Alm",
                    StreetAddress = "Storgatan 44",
                    City = "Borås",
                    Postalcode = 50334,
                    TaxRate = 28,
                },
                new SalesMen()
                {
                    AgentNumber = 1153,
                    Firstname = "Linda",
                    Lastname = "Jonsson",
                    StreetAddress = "Herrelyckevägen 9",
                    City = "Ulricehamn",
                    Postalcode = 52335,
                    TaxRate = 28,
                },
                new SalesMen()
                {
                    AgentNumber = 7473,
                    Firstname = "Malin",
                    Lastname = "Nilsdotter",
                    StreetAddress = "Polgatan 13",
                    City = "Landvetter",
                    Postalcode = 43350,
                    TaxRate = 30,
                },
                new SalesMen()
                {
                    AgentNumber = 4331,
                    Firstname = "Mikael",
                    Lastname = "Lund",
                    StreetAddress = "Humlegården 19",
                    City = "Borås",
                    Postalcode = 50332,
                    TaxRate = 29,
                },
                new SalesMen()
                {
                    AgentNumber = 7337,
                    Firstname = "Patrik",
                    Lastname = "Hedman",
                    StreetAddress = "Sandvägen 47",
                    City = "Rångedala",
                    Postalcode = 51693,
                    TaxRate = 28,
                }
            };

            List<SalesMen> NewList = new List<SalesMen>();
            foreach (var i in BusinessController.Instance.SMController.GetAllSalesMen())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in salesMens)
                {
                    BusinessController.Instance.SMController.AddSalesMen(item);
                }
            }
        }
        private void GenerateUA()
        {
            List<UserAccess> users = new List<UserAccess>
            {
                new UserAccess()
                {
                    Username = "KS",
                    Password = "1",
                    Firstname = "Karin",
                    Lastname = "Sundberg",
                    Search = true,
                    StatisticsAndProspects = false,
                    Insurances = true,
                    EmployeeManagement = false,
                    Commission = true,
                    BasicData = false,
                },
                new UserAccess()
                {
                    Username = "BA",
                    Password = "2",
                    Firstname = "Bill",
                    Lastname = "Andersson",
                    Search = true,
                    StatisticsAndProspects = true,
                    Insurances = true,
                    EmployeeManagement = true,
                    Commission = true,
                    BasicData = true,
                },
                new UserAccess()
                {
                    Username = "VP",
                    Password = "3",
                    Firstname = "Vigo",
                    Lastname = "Persson",
                    Search = true,
                    Insurances = false,
                    StatisticsAndProspects = false,
                    EmployeeManagement = false,
                    Commission = false,
                    BasicData = false,
                },
                new UserAccess()
                {
                    Username = "Admin",
                    Password = "Admin",
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Search = true,
                    StatisticsAndProspects = true,
                    Insurances = true,
                    EmployeeManagement = true,
                    Commission = true,
                    BasicData = true,
                }
            };
            List<UserAccess> NewList = new List<UserAccess>();
            foreach (var i in BusinessController.Instance.UAController.GetAllUsers())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in users)
                {
                    BusinessController.Instance.UAController.AddUser(item);
                }
            }
        }
        private void GenerateBAT()
        {

            List<BaseAmountTabel> bat = new List<BaseAmountTabel>
            {
                new BaseAmountTabel()
                {
                    BaseAmount = 700000,
                    AckValue = 11000,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(1),
                },
                new BaseAmountTabel()
                {
                    BaseAmount = 900000,
                    AckValue = 12600,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(1),
                },
                new BaseAmountTabel()
                {
                    BaseAmount = 1100000,
                    AckValue = 14200,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(1),
                },
                new BaseAmountTabel()
                {
                    BaseAmount = 1300000,
                    AckValue = 15800,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(1),
                },
                new BaseAmountTabel()
                {
                    BaseAmount = 300000,
                    AckValue = 14200,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(2),
                },
                new BaseAmountTabel()
                {
                    BaseAmount = 400000,
                    AckValue = 16900,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(2),
                },
                new BaseAmountTabel()
                {
                    BaseAmount = 500000,
                    AckValue = 19600,
                    Date = DateTime.Now,
                    SAID = BusinessController.Instance.IController.GetSA(2),
                }
            };
            List<BaseAmountTabel> newList = new List<BaseAmountTabel>();
            foreach (var i in BusinessController.Instance.BDController.GetAllTables())
            {
                newList.Add(i);
            }
            if (newList.Count == 0)
            {
                foreach (var item in bat)
                {
                    BusinessController.Instance.BDController.AddBaseAmountTable(item);
                }
            }
        }
        private void GenerateBA()
        {
            List<BaseAmount> bat = new List<BaseAmount>
            {
                new BaseAmount()
                {
                    Baseamount = 300000,
                    Date = DateTime.Now,
                    LIFEID = BusinessController.Instance.IController.GetLIFE(1),
                },
                new BaseAmount()
                {
                    Baseamount = 400000,
                    Date = DateTime.Now,
                    LIFEID = BusinessController.Instance.IController.GetLIFE(1),
                },
                new BaseAmount()
                {
                    Baseamount = 500000,
                    Date = DateTime.Now,
                    LIFEID = BusinessController.Instance.IController.GetLIFE(1),
                }
            };
            List<BaseAmount> newList = new List<BaseAmount>();
            foreach (var i in BusinessController.Instance.BDController.GetAllBaseAmount())
            {
                newList.Add(i);
            }
            if (newList.Count == 0)
            {
                foreach (var item in bat)
                {
                    BusinessController.Instance.BDController.AddBaseAmountOption(item);
                }
            }
        }
        private void GenerateAV()
        {
            List<AckValueVariable> avv = new List<AckValueVariable>
            {
                new AckValueVariable()
                {
                    Date = DateTime.Now,
                    AckValue = 0.005,
                    LIFEID = BusinessController.Instance.IController.GetLIFE(1),
                    OptionalTypeId = null
                },
                new AckValueVariable()
                {
                    Date = DateTime.Now,
                    AckValue = 0.015,
                    LIFEID = null,
                    OptionalTypeId = BusinessController.Instance.IController.GetOPT(1),

                },
                new AckValueVariable()
                {
                    Date = DateTime.Now,
                    AckValue = 0.005,
                    LIFEID = null,
                    OptionalTypeId = BusinessController.Instance.IController.GetOPT(2),

                },
                new AckValueVariable()
                {
                    Date = DateTime.Now,
                    AckValue = 1.54,
                    LIFEID = null,
                    OptionalTypeId = BusinessController.Instance.IController.GetOPT(3),

                }
            };
            List<AckValueVariable> newList = new List<AckValueVariable>();
            foreach (var i in BusinessController.Instance.BDController.GetAllAckValues())
            {
                newList.Add(i);
            }
            if (newList.Count == 0)
            {
                foreach (var item in avv)
                {
                    BusinessController.Instance.BDController.AddAckValue(item);
                }
            }
        }
        private void CreateOptionalTypes()
        {
            List<OptionalType> OptionList = new List<OptionalType>
            {
                new OptionalType { OptionalTypeId = 1, OptionalName = "Invaliditet vid olycksfall" },
                new OptionalType { OptionalTypeId = 2, OptionalName = "Höjning av livförsäkring" },
                new OptionalType { OptionalTypeId = 3, OptionalName = "Månadsersättning vid långvarig sjukskrivning" }
            };

            List<OptionalType> NewOptionList = new List<OptionalType>();

            foreach (var i in BusinessController.Instance.IController.GetAllOPT())
            {
                NewOptionList.Add(i);
            }
            if (NewOptionList.Count == 0)
            {
                foreach (var item in OptionList)
                {
                    BusinessController.Instance.IController.AddOptionalTypes(item);
                }
            }
        }
        private void CreateSAInsurances()
        {
            List<SAInsurance> SAList = new List<SAInsurance>
            {
                new SAInsurance { SAID = 1, SAInsuranceType = "Sjuk- och olycksfallsförsäkring för barn" },
                new SAInsurance { SAID = 2, SAInsuranceType = "Sjuk- och olycksfallsförsäkring för vuxen" }
            };

            List<SAInsurance> NewList = new List<SAInsurance>();
            foreach (var i in BusinessController.Instance.IController.GetAllSAI())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in SAList)
                {
                    BusinessController.Instance.IController.AddSaInsurances(item);
                }
            }
        }
        private void CreateLifeInsurance()
        {
            List<LifeInsurance> LifeList = new List<LifeInsurance>
            {
                new LifeInsurance { LifeID = 1, LifeName = "Livförsäkring för vuxen" }
            };

            List<LifeInsurance> NewList = new List<LifeInsurance>();
            foreach (var i in BusinessController.Instance.IController.GetAllLIFE())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in LifeList)
                {
                    BusinessController.Instance.IController.AddLifeInsurance(item);
                }
            }

        }
        private void CreateCompanyInsurance()
        {
            List<CompanyInsurance> CompList = new List<CompanyInsurance>
            {
                new CompanyInsurance { FFId = 1, COIName = "Företagsförsäkring" }
            };


            List<CompanyInsurance> NewList = new List<CompanyInsurance>();

            foreach (var i in BusinessController.Instance.IController.GetAllCAI())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in CompList)
                {
                    BusinessController.Instance.IController.AddCompanyInsurance(item);
                }
            }

        }
        private void CreateOtherPersonInsurance()
        {
            List<OtherPersonInsurance> OPList = new List<OtherPersonInsurance>
            {
                new OtherPersonInsurance { OPIId = 1, OPIName = "Övrig personförsäkring för vuxen" }
            };

            List<OtherPersonInsurance> NewList = new List<OtherPersonInsurance>();

            foreach (var i in BusinessController.Instance.IController.GetAllOPI())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in OPList)
                {
                    BusinessController.Instance.IController.AddOtherPersonInsurance(item);
                }
            }

        }
        private void GeneratePersonTaker()
        {
            List<Person> persons = new List<Person>
            {
                new Person()
                {
                    SocialSecurityNumber = "8711306789",
                    City = "Jönköping",
                    Firstname = "Elin",
                    Lastname = "Käll",
                    PostalCode = 55446,
                    EmailOne = "elin.k@hotmail.com",
                    EmailTwo = null,
                    StreetAddress = "Bosshagsgatan 35",
                    DiallingCodeHome = "032",
                    TelephoneNbrHome = "87653",
                    DiallingCodeWork = null,
                    TelephoneNbrWork = null,
                },
                new Person()
                {
                    SocialSecurityNumber = "7605206729",
                    City = "Borås",
                    Firstname = "Julia",
                    Lastname = "Hansson",
                    PostalCode = 50330,
                    EmailOne = "julia.hansson1@gmail.com",
                    EmailTwo = "j.k@gmail.com",
                    StreetAddress = "Säterivägen 12",
                    DiallingCodeHome = "037",
                    TelephoneNbrHome = "6785643",
                    DiallingCodeWork = "037",
                    TelephoneNbrWork = "76543",
                },
                new Person()
                {
                    SocialSecurityNumber = "7407016739",
                    City = "Lerum",
                    Firstname = "Peter",
                    Lastname = "Alm",
                    PostalCode = 44330,
                    EmailOne = "almPeter@gmail.com",
                    EmailTwo = "PAlm45@hotmail.com",
                    StreetAddress = "Kolgatan 23",
                    DiallingCodeHome = "0302",
                    TelephoneNbrHome = "799865",
                    DiallingCodeWork = "0304",
                    TelephoneNbrWork = "78657637",
                },
                new Person()
                {
                    SocialSecurityNumber = "6511156744",
                    City = "Göteborg",
                    Firstname = "Kristin",
                    Lastname = "Karlsson",
                    PostalCode = 41101,
                    EmailOne = "kk2@hotmail.com",
                    EmailTwo = "kristinkarlsson@live.se",
                    StreetAddress = "Vagngatan 56",
                    DiallingCodeHome = "031",
                    TelephoneNbrHome = "777473",
                    DiallingCodeWork = null,
                    TelephoneNbrWork = null,
                },
                new Person()
                {
                    SocialSecurityNumber = "6301134514",
                    City = "Göteborg",
                    Firstname = "Daniel",
                    Lastname = "Karlsson",
                    PostalCode = 41101,
                    EmailOne = "dk@hotmail.com",
                    EmailTwo = null,
                    StreetAddress = "Vagngatan 56",
                    DiallingCodeHome = "031",
                    TelephoneNbrHome = "777473",
                    DiallingCodeWork = null,
                    TelephoneNbrWork = null,
                }
            };
            List<Person> NewList = new List<Person>();
            foreach (var i in BusinessController.Instance.ITController.GetAllPersons())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in persons)
                {
                    BusinessController.Instance.ITController.AddPersonInsuranceTaker(item);
                }
            }
        }
        private void GenerateCompanyTaker()
        {
            List<Company> companies = new List<Company>
            {
                new Company()
                {
                    OrganizationNumber = "5567893531",
                    PostalCode = 50711,
                    StreetAddress = "Fallgatan 67",
                    City = "Borås",
                    CompanyName = "Borås Bilförvaltning AB",
                    DiallingCode = "03",
                    Email = "BorasBilForvaltning@gmail.com",
                    ContactPerson = "Anders Sundin",
                    FaxNumber = null,
                    TelephoneNbr = "7838637",
                },
                new Company()
                {
                    OrganizationNumber = "5563260701",
                    PostalCode = 16867,
                    StreetAddress = "Ranhammarsvägen 9",
                    City = "Bromma",
                    CompanyName = "Alviks Måleri AB",
                    DiallingCode = "08",
                    Email = "AlviksMaleriAB@hotmail.com",
                    ContactPerson = "Malin Larsson",
                    FaxNumber = "896784",
                    TelephoneNbr = "7047581",
                },
                new Company()
                {
                    OrganizationNumber = "5566925763",
                    PostalCode = 58278,
                    StreetAddress = "Finnögatan 10",
                    City = "Linköping",
                    CompanyName = "Belami Plåt & Lack AB",
                    DiallingCode = "01",
                    Email = null,
                    ContactPerson = null,
                    FaxNumber = null,
                    TelephoneNbr = "3102385",
                }
            };
            List<Company> NewList = new List<Company>();
            foreach (var i in BusinessController.Instance.ITController.GetAllCompanies())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in companies)
                {
                    BusinessController.Instance.ITController.AddCompanyInsuranceTaker(item);
                }
            }
        }
        private void GenerateInsuredPerson()
        {
            List<InsuredPerson> persons = new List<InsuredPerson>
            {
                new InsuredPerson()  //Id: 1
                {
                    FirstName = "Elin",
                    LastName = "Käll",
                    SocialSecurityNumberIP = "8711306789",
                    PersonType = "Vuxen",
                    PersonTaker = BusinessController.Instance.ITController.GetPerson(person1),
                },
                new InsuredPerson() //Id: 2
                {
                    FirstName = "Lisa",
                    LastName = "Hansson",
                    SocialSecurityNumberIP = "0304116723",
                    PersonType = "Barn",
                    PersonTaker = BusinessController.Instance.ITController.GetPerson(person2),
                },
                new InsuredPerson() //Id: 3
                {
                    FirstName = "Peter",
                    LastName = "Alm",
                    SocialSecurityNumberIP = "7407016739",
                    PersonType = "Vuxen",
                    PersonTaker = BusinessController.Instance.ITController.GetPerson(person3),
                },
                new InsuredPerson() //Id: 4
                {
                    FirstName = "Erik",
                    LastName = "Karlsson",
                    SocialSecurityNumberIP = "9909248997",
                    PersonType = "Barn",
                    PersonTaker = BusinessController.Instance.ITController.GetPerson(person4),
                },
                new InsuredPerson() //Id: 5
                {
                    FirstName = "Daniel",
                    LastName = "Karlsson",
                    SocialSecurityNumberIP = "6301134514",
                    PersonType = "Vuxen",
                    PersonTaker = BusinessController.Instance.ITController.GetPerson(person5),
                },
                new InsuredPerson() //Id: 6
                {
                    FirstName = "Bertil",
                    LastName = "Alm",
                    SocialSecurityNumberIP = "5712086219",
                    PersonType = "Vuxen",
                    PersonTaker = BusinessController.Instance.ITController.GetPerson(person3),
                },
                new InsuredPerson() //Id: 7
                {
                    FirstName = "Hans",
                    LastName = "Eriksson",
                    SocialSecurityNumberIP = "8407290999",
                    PersonType = "Vuxen",
                    CompanyTaker = BusinessController.Instance.ITController.GetCompany(company1),
                },
                new InsuredPerson() //Id: 8
                {
                    FirstName = "Bert",
                    LastName = "Henningsson",
                    SocialSecurityNumberIP = "9102190719",
                    PersonType = "Vuxen",
                    CompanyTaker = BusinessController.Instance.ITController.GetCompany(company1),
                },
                new InsuredPerson() //Id: 9
                {
                    FirstName = "Erika",
                    LastName = "Lundgren",
                    SocialSecurityNumberIP = "9503093321",
                    PersonType = "Vuxen",
                    CompanyTaker = BusinessController.Instance.ITController.GetCompany(company2),
                }
            };
            List<InsuredPerson> NewList = new List<InsuredPerson>();
            foreach (var i in BusinessController.Instance.IPController.GetAllInsuredPersons())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in persons)
                {
                    BusinessController.Instance.IPController.AddInsuredPerson(item);
                }
            }
        }
        private readonly string person1 = "8711306789";
        private readonly string person2 = "7605206729";
        private readonly string person3 = "7407016739";
        private readonly string person4 = "6511156744";
        private readonly string person5 = "6301134514";

        private readonly string company1 = "5567893531";
        private readonly string company2 = "5563260701";
        private readonly string company3 = "5566925763";

        private readonly int sm1 = 7337;
        private readonly int sm2 = 2264;
        private readonly int sm3 = 5836;
        private readonly int sm4 = 1153;

        private void GenerateInsurances()
        {
            List<Insurance> insurances = new List<Insurance>();
            string str = "31/12/2023";
            DateTime.TryParse(str, out DateTime dt);

            insurances.Add(new Insurance()
            {
                SerialNumber = "FF1",
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm1),
                TakerNbr = "5566925763",
                TypeName = "Företagsförsäkring",
                COI = BusinessController.Instance.IController.GetCI(1),
                PaymentForm = "Månad",
                InsuranceCompany = "Länsförsäkringar",
                CompanyTaker = BusinessController.Instance.ITController.GetCompany(company3),
                EndDate = dt,
                StartDate = DateTime.Today,
                Notes = null,
                Premie = 789,
                InsuranceStatus = Status.Otecknad,
                CompanyInsuranceType = "Fastighet",
            });

            insurances.Add(new Insurance()
            {
                SerialNumber = "LIV1",
                PersonTaker = BusinessController.Instance.ITController.GetPerson(person1),
                TakerNbr = "8711306789",
                TypeName = "Livförsäkring för vuxen",
                PaymentForm = "Halvår",
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = DateTime.Today,
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm3),
                InsuredID = BusinessController.Instance.IPController.GetIPerson(1),
                LIFE = BusinessController.Instance.IController.GetLIFE(1),
                BaseAmountValue = 400000,
                AckValue4 = 2000,
            });
            insurances.Add(new Insurance()
            {
                SerialNumber = "LIV2",
                PersonTaker = BusinessController.Instance.ITController.GetPerson(person3),
                TakerNbr = "7407016739",
                TypeName = "Livförsäkring för vuxen",
                PaymentForm = "Kvartal",
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = DateTime.Today,
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm1),
                InsuredID = BusinessController.Instance.IPController.GetIPerson(3),
                LIFE = BusinessController.Instance.IController.GetLIFE(1),
                BaseAmountValue = 400000,
                AckValue4 = 2000,
            });
            insurances.Add(new Insurance()
            {
                SerialNumber = "ÖPFV1",
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm1),
                TakerNbr = "5567893531",
                TypeName = "Övrig personförsäkring för vuxen",
                OPI = BusinessController.Instance.IController.GetOPI(1),
                PaymentForm = "Kvartal",
                InsuredID = BusinessController.Instance.IPController.GetIPerson(8),
                CompanyTaker = BusinessController.Instance.ITController.GetCompany(company1),
                DeliveryDate = DateTime.Today,
                Premie = 789,
                Table = "RT23H6",
                InsuranceStatus = Status.Otecknad,
            });
            insurances.Add(new Insurance()
            {
                SerialNumber = "ÖPFV2",
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm1),
                TakerNbr = "5567893531",
                TypeName = "Övrig personförsäkring för vuxen",
                OPI = BusinessController.Instance.IController.GetOPI(1),
                PaymentForm = "Kvartal",
                InsuredID = BusinessController.Instance.IPController.GetIPerson(7),
                CompanyTaker = BusinessController.Instance.ITController.GetCompany(company1),
                DeliveryDate = DateTime.Today,
                Premie = 789,
                Table = "RT23H6",
                InsuranceStatus = Status.Otecknad,
            });
            insurances.Add(new Insurance()
            {
                SerialNumber = "ÖPFV3",
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm2),
                TakerNbr = "5563260701",
                TypeName = "Övrig personförsäkring för vuxen",
                OPI = BusinessController.Instance.IController.GetOPI(1),
                PaymentForm = "Kvartal",
                InsuredID = BusinessController.Instance.IPController.GetIPerson(9),
                CompanyTaker = BusinessController.Instance.ITController.GetCompany(company2),
                DeliveryDate = DateTime.Today,
                Premie = 299,
                Table = "RK23H6",
                InsuranceStatus = Status.Otecknad,
            });
            insurances.Add(new Insurance()
            {
                SerialNumber = "ÖPFV4",
                PersonTaker = BusinessController.Instance.ITController.GetPerson(person4),
                TakerNbr = "6511156744",
                TypeName = "Övrig personförsäkring för vuxen",
                PaymentForm = "Månad",
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = DateTime.Today,
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm3),
                InsuredID = BusinessController.Instance.IPController.GetIPerson(6),
                Table = "78YT",
                Premie = 7867,
                OPI = BusinessController.Instance.IController.GetOPI(1),
            });
            insurances.Add(new Insurance()
            {
                SAI = BusinessController.Instance.IController.GetSA(1),
                SerialNumber = "SOB1",
                PersonTaker = BusinessController.Instance.ITController.GetPerson(person4),
                TakerNbr = "6511156744",
                TypeName = "Sjuk- och olycksfallsförsäkring för barn",
                PaymentForm = "Helår",
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = DateTime.Today,
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm4),
                InsuredID = BusinessController.Instance.IPController.GetIPerson(4),
                OptionalTypes = null,
                BaseAmountValue = 0,
                AckValue = 0,
                BaseAmountValue2 = 0,
                AckValue2 = 0,
                BaseAmountValue3 = 0,
                AckValue3 = 0,
                BaseAmountValue4 = 700000,
                AckValue4 = 11000,
            });
            insurances.Add(new Insurance()
            {
                SAI = BusinessController.Instance.IController.GetSA(1),
                SerialNumber = "SOB2",
                PersonTaker = BusinessController.Instance.ITController.GetPerson(person2),
                TakerNbr = "7605206729",
                TypeName = "Sjuk- och olycksfallsförsäkring för barn",
                PaymentForm = "Helår",
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = DateTime.Today,
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm4),
                InsuredID = BusinessController.Instance.IPController.GetIPerson(2),
                OptionalTypes = null,
                BaseAmountValue = 0,
                AckValue = 0,
                BaseAmountValue2 = 0,
                AckValue2 = 0,
                BaseAmountValue3 = 0,
                AckValue3 = 0,
                BaseAmountValue4 = 1100000,
                AckValue4 = 14200,
            });
            insurances.Add(new Insurance()
            {
                SAI = BusinessController.Instance.IController.GetSA(2),
                SerialNumber = "SOV1",
                PersonTaker = BusinessController.Instance.ITController.GetPerson(person5),
                TakerNbr = "6301134514",
                TypeName = "Sjuk- och olycksfallsförsäkring för vuxen",
                PaymentForm = "Halvår",
                InsuranceStatus = Status.Otecknad,
                DeliveryDate = DateTime.Today,
                AgentNo = BusinessController.Instance.SMController.GetSalesMen(sm3),
                InsuredID = BusinessController.Instance.IPController.GetIPerson(5),
                OptionalTypes = null,
                BaseAmountValue = 0,
                AckValue = 0,
                BaseAmountValue2 = 0,
                AckValue2 = 0,
                BaseAmountValue3 = 0,
                AckValue3 = 0,
                BaseAmountValue4 = 500000,
                AckValue4 = 19600,
            });
            List<Insurance> NewList = new List<Insurance>();
            foreach (var i in BusinessController.Instance.IController.GetAllInsurances())
            {
                NewList.Add(i);
            }
            if (NewList.Count == 0)
            {
                foreach (var item in insurances)
                {
                    BusinessController.Instance.IController.AddInsuranceApplication(item);
                }
            }
        }
    }
}
