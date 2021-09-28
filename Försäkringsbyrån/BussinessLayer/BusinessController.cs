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
        public EmployeeController EController { get; }
        public InsuranceTakerController ITController { get; }

        public InsuranceApplicationController IAController {get;}
        public InsuredPersonController IPController { get; }
        public Employee CurrentEmployee { get; set; } = null;
        public SignedInsuranceController SIController { get; }

        private BusinessController()
        {
            Context = new UnitOfWork();
            EController = new EmployeeController();
            ITController = new InsuranceTakerController();
            IAController = new InsuranceApplicationController();
            IPController = new InsuredPersonController();
            SIController = new SignedInsuranceController();
        }

        public void Save() => Context.Complete();



    }
}
