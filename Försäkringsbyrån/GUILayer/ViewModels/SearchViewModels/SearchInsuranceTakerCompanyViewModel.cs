using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GUILayer.ViewModels.SearchViewModels
{
    public class SearchInsuranceTakerCompanyViewModel : BaseViewModel
    {
        public static readonly SearchInsuranceTakerCompanyViewModel Instance = new SearchInsuranceTakerCompanyViewModel();

        public SearchInsuranceTakerCompanyViewModel()
        {
            Companies = new ObservableCollection<Company>
            {
                new Company(){InsuranceTakerId=3, OrganizationNumber="991204-1222", CompanyName="IKEA AB", City="Ulricehamn", PostalCode=52335, StreetAddress="Källgatan 10", DiallingCode="xx", TelephoneNbr="0765566899", FaxNumber="334", Email="a@häst.se", ContactPerson="Emma",InsuranceApplications= new List<InsuranceApplication>(), InsuredPersons=new List<InsuredPerson>()},
                new Company(){InsuranceTakerId=4, OrganizationNumber="991204-1213", CompanyName="Häst AB", City="Ulricehamn", PostalCode=52335, StreetAddress="Källgatan 10",DiallingCode="xx", TelephoneNbr="0765566899",FaxNumber="334", Email="a@häst.se", ContactPerson="Emma", InsuranceApplications= new List<InsuranceApplication>(), InsuredPersons=new List<InsuredPerson>()},
            };
            CompanyGrid = CollectionViewSource.GetDefaultView(Companies);
            CompanyGrid.Filter = new Predicate<object>(o => Filter(o as Company));
        }
        #region Specific Porperties and methods for search in collection

        private ICollectionView _companyCollection;
        public ICollectionView CompanyGrid
        {
            get { return _companyCollection; }
            set { _companyCollection = value; OnPropertyChanged("CompanyGrid"); }
        }
        private bool Filter(Company company)
        {
            return SearchInput == null
                || company.OrganizationNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || company.CompanyName.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || company.ContactPerson.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string _searchInput;

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                CompanyGrid.Refresh();
            }
        }
        #endregion

        #region Properties

        public ObservableCollection<Company> Companies { get; set; }

        #endregion
    }
}
