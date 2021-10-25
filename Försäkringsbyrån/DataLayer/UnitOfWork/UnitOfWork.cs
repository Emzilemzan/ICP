using DataLayer.GenericRepository;
using DataLayer.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    /// <summary>
    /// This is the main unitofwork that receives saved data, distributes the data and saves new data
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IEmployeeRepository Employees { get; }
        public IInsuranceRepository Insurances { get; }
        public IInsuredPersonRepository InsuredPersons { get; }
        public IPersonRepository Persons { get; }
        public ICompanyRepository Companies { get; }
        public IAccessRepository Accesses { get; }
        public IBaseAmountTableRepository Tables { get; }
        public ISAInsuranceRepository SAInsurances { get; }
        public IVacationPayRepository VPays { get; }
        public IBaseAmountRepository BaseAmounts { get; }
        public IOptionalTypeRepository OptionalTypes { get; }
        public ICustomerProspectRepository Prospects { get; }
        public IAckValueVariableRepository AckValues { get; }
        public ICommissionRepository CommissionShares { get; }
        public ILifeInsuranceRepository LifeInsurances { get; }
        public IOtherPersonInsuranceRepository OPInsurances { get; }
        public ICompanyInsuranceRepository CIInsurances { get; }
        public UnitOfWork()
        {
            _context = new ApplicationContext();
            Employees = new EmployeeRepository(_context);
            Insurances = new InsuranceRepository(_context);
            InsuredPersons = new InsuredPersonRepository(_context);
            Prospects = new CustomerProspectRepository(_context);
            Persons = new PersonRepository(_context);
            Companies = new CompanyRepository(_context);
            Accesses = new AccessRepository(_context);
            Tables = new BaseAmountTableRepository(_context);
            BaseAmounts = new BaseAmountRepository(_context);
            OptionalTypes = new OptionalTypeRepository(_context);
            VPays = new VacationPayRepository(_context);
            AckValues = new AckValueVariableRepository(_context);
            CommissionShares = new CommissionRepository(_context);
            SAInsurances = new SAInsuranceRepository(_context);
            LifeInsurances = new LifeInsuranceRepository(_context);
            OPInsurances = new OtherPersonInsuranceRepository(_context);
            CIInsurances = new CompanyInsuranceRepository(_context);
            Init();
        }
        public void Init() => _context.Reset();
        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
