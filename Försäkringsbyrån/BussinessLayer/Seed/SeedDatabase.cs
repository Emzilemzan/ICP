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
        }

        private void GenerateSM()
        {
            List<SalesMen> salesMens = new List<SalesMen>();

            salesMens.Add(new SalesMen()
            {
                AgentNumber = 2547,
                Firstname = "Irene" ,
                Lastname = "Johansson",
                StreetAddress = "Krokusstigen 12",
                City = "Borås",
                Postalcode = 50330,
                TaxRate = 28,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 6423,
                Firstname = "Karin",
                Lastname = "Sundberg",
                StreetAddress = "Borgmästarevägen 73",
                City = "Allingsås",
                Postalcode = 44141,
                TaxRate = 30,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 2447,
                Firstname = "Vigo",
                Lastname = "Persson",
                StreetAddress = "Tallgatan 17",
                City = "Björvik",
                Postalcode = 43245,
                TaxRate = 29,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 5836,
                Firstname = "Birgitta",
                Lastname = "Frisk",
                StreetAddress = "Björkvägen 17",
                City = "Jönköping",
                Postalcode = 55315,
                TaxRate = 31,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 2264,
                Firstname = "Boris",
                Lastname = "Alm",
                StreetAddress = "Storgatan 44",
                City = "Borås",
                Postalcode = 50334,
                TaxRate = 28,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 1153,
                Firstname = "Linda",
                Lastname = "Jonsson",
                StreetAddress = "Herrelyckevägen 9",
                City = "Ulricehamn",
                Postalcode = 52335,
                TaxRate = 28,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 7473,
                Firstname = "Malin",
                Lastname = "Nilsdotter",
                StreetAddress = "Polgatan 13",
                City = "Landvetter",
                Postalcode = 43350,
                TaxRate = 30,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 4331,
                Firstname = "Mikael",
                Lastname = "Lund",
                StreetAddress = "Humlegården 19",
                City = "Borås",
                Postalcode = 50332,
                TaxRate = 29,
            });
            salesMens.Add(new SalesMen()
            {
                AgentNumber = 7337,
                Firstname = "Patrik",
                Lastname = "Hedman",
                StreetAddress = "Sandvägen 47",
                City = "Rångedala",
                Postalcode = 51693,
                TaxRate = 28,
            });

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
            List<UserAccess> users = new List<UserAccess>();
            users.Add(new UserAccess()
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
            });
            users.Add(new UserAccess()
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
            });
            users.Add(new UserAccess()
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
            });
            users.Add(new UserAccess()
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
            });
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
            
            List<BaseAmountTabel> bat = new List<BaseAmountTabel>();
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 700000,
                AckValue = 11000,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(1),
            });
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 900000,
                AckValue = 12600,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(1),
            });
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 1100000,
                AckValue = 14200,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(1),
            });
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 1300000,
                AckValue = 15800,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(1),
            });
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 300000,
                AckValue = 14200,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(2),
            });
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 400000,
                AckValue = 16900,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(2),
            });
            bat.Add(new BaseAmountTabel()
            {
                BaseAmount = 500000,
                AckValue = 19600,
                Date = DateTime.Now,
                SAID = BusinessController.Instance.IController.GetSA(2),
            });
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
            List<BaseAmount> bat = new List<BaseAmount>();
            bat.Add(new BaseAmount()
            {
                Baseamount = 300000,
                Date = DateTime.Now,
                LIFEID = BusinessController.Instance.IController.GetLIFE(1),
            });
            bat.Add(new BaseAmount()
            {
                Baseamount = 400000,
                Date = DateTime.Now,
                LIFEID = BusinessController.Instance.IController.GetLIFE(1),
            });
            bat.Add(new BaseAmount()
            {
                Baseamount = 500000,
                Date = DateTime.Now,
                LIFEID = BusinessController.Instance.IController.GetLIFE(1),
            });
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
            List<AckValueVariable> avv = new List<AckValueVariable>();
            avv.Add(new AckValueVariable()
            {
                Date = DateTime.Now,
                AckValue = 0.005,
                LIFEID = BusinessController.Instance.IController.GetLIFE(1),
                OptionalTypeId = null
            }); 
            avv.Add(new AckValueVariable()
            {
                Date = DateTime.Now,
                AckValue = 0.015,
                LIFEID = null,
                OptionalTypeId = BusinessController.Instance.IController.GetOPT(1),

            });
            avv.Add(new AckValueVariable()
            {
                Date = DateTime.Now,
                AckValue = 0.005,
                LIFEID = null,
                OptionalTypeId = BusinessController.Instance.IController.GetOPT(2),

            });
            avv.Add(new AckValueVariable()
            {
                Date = DateTime.Now,
                AckValue = 1.54,
                LIFEID = null,
                OptionalTypeId = BusinessController.Instance.IController.GetOPT(3),
                 
            });
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
            List<OptionalType> OptionList = new List<OptionalType>();


            OptionList.Add(new OptionalType { OptionalTypeId = 1, OptionalName = "Invaliditet vid olycksfall" });
            OptionList.Add(new OptionalType { OptionalTypeId = 2, OptionalName = "Höjning av livförsäkring" });
            OptionList.Add(new OptionalType { OptionalTypeId = 3, OptionalName = "Månadsersättning vid långvarig sjukskrivning" });

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
            List<SAInsurance> SAList = new List<SAInsurance>();
            SAList.Add(new SAInsurance { SAID = 1, SAInsuranceType = "Sjuk- och olycksfallsförsäkring för barn" });
            SAList.Add(new SAInsurance { SAID = 2, SAInsuranceType = "Sjuk- och olycksfallsförsäkring för vuxen" });

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
            List<LifeInsurance> LifeList = new List<LifeInsurance>();


            LifeList.Add(new LifeInsurance { LifeID = 1, LifeName = "Livförsäkring för vuxen" });

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
            List<CompanyInsurance> CompList = new List<CompanyInsurance>();


            CompList.Add(new CompanyInsurance { FFId = 1, COIName = "Företagsförsäkring" });


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
            List<OtherPersonInsurance> OPList = new List<OtherPersonInsurance>();


            OPList.Add(new OtherPersonInsurance { OPIId = 1, OPIName = "Övrig personförsäkring för vuxen" });

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

    }
}
