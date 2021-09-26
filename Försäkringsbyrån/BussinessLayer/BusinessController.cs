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
        public Employee CurrentEmployee { get; set; } = null;

        private BusinessController()
        {
            Context = new UnitOfWork();
            EController = new EmployeeController();
            ITController = new InsuranceTakerController();
        }

        public void Save() => Context.Complete();



    }
}
