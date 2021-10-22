using DataLayer.InterfaceRepository;
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
        ICustomerProspectRepository Prospects { get; }
        IInsuranceRepository Insurances { get; }
        IInsuredPersonRepository InsuredPersons { get; }
        ICompanyRepository Companies { get; }
        IPersonRepository Persons { get; }
        IAccessRepository Accesses { get; }
        IVacationPayRepository VPays { get; }
        IBaseAmountTableRepository Tables { get; }
        IBaseAmountRepository BaseAmounts { get; }
        IOptionalTypeRepository OptionalTypes { get; }
        IAckValueVariableRepository AckValues { get; }
        ICommissionRepository CommissionShares { get; }
        ILifeInsuranceRepository LifeInsurances { get; }
        IOtherPersonInsuranceRepository OPInsurances { get; }
        ICompanyInsuranceRepository CIInsurances { get; }

        ISAInsuranceRepository SAInsurances { get; }
        int Complete();

    }
}
