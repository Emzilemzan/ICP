using DataLayer.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    /// <summary>
    /// interface for unitofwork class. 
    /// </summary>
  public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IInsuranceApplicationRepository Applications { get; }
        IInsuranceTakerRepository InsuranceTakers { get; }
        IInsuredPersonRepository InsuredPersons { get; }
        ICompanyRepository Companies { get; }
        IPersonRepository Persons { get; }
        int Complete();

    }
}
