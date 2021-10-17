using BussinessLayer.Seed;
using DataLayer;
using DataLayer.UnitOfWork;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// keep businesslogic localized, future changes will be easier.
    /// </summary>
    public class BusinessController
    {
        public static readonly BusinessController Instance = new BusinessController();
        internal IUnitOfWork Context { get; }
        public SalesMenController SMController { get; }
        public InsuranceTakerController ITController { get; }
        public InsuranceController IController { get; }
        public InsuredPersonController IPController { get; }
        public UserAccess CurrentUser { get; set; } = null;
        public UserAccessController UAController { get;} = null;
        public CommissionController CMController { get; set; }
        public BasedataController BDController { get; set; }
        
        private BusinessController()
        {
            Context = new UnitOfWork();
            SMController = new SalesMenController();
            UAController = new UserAccessController();
            ITController = new InsuranceTakerController();
            IController = new InsuranceController();
            IPController = new InsuredPersonController();
            CMController = new CommissionController();
            BDController = new BasedataController();
           
        }

        public void Save() => Context.Complete();

        public void GenerateData() => new SeedDatabase();


    }
}
