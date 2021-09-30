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
    }
}
