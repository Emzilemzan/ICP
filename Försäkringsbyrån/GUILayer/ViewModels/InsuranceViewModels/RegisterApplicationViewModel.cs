using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUILayer.Commands;
using Models.Models;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    /// <summary>
    /// FÖRETAGSFÖRSÄKRING!!!! REGISTRERA FÖRETAGSFÖRSÄKRING!!!!!!!
    /// </summary>
    public class RegisterApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterApplicationViewModel Instance = new RegisterApplicationViewModel();

        public RegisterApplicationViewModel()
        {
            AddCompanyApplication = new RelayCommand(RegisterCompanyApplication, CanAddCompanyApplication);
        }
       

        public RelayCommand AddCompanyApplication { get; set; }

        public bool CanAddCompanyApplication(object parameter)
        {
            return true;
        }

        public void RegisterCompanyApplication(object parameter)
        {
        }

        #region 
        public ObservableCollection<Company> Companies { get; set; }

        private string _searchFrase;
        public string SearchFrase
        {
            get => _searchFrase;
            set
            {
                _searchFrase = value;
                OnPropertyChanged("SearchFrase");
            }
        }
        public ObservableCollection<Company> UpdateCompanies()
        {
            ObservableCollection<Company> x = new ObservableCollection<Company>();
            foreach (var c in Context.ITController.GetAllCompanies())
            {
                x?.Add(c);
            }
            Companies = x;
            return Companies;
        }
       
    }
   
    #endregion
}

