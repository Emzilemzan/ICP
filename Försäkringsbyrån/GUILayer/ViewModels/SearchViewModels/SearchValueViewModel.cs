using GUILayer.Commands.SearchIndexCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUILayer.ViewModels.SearchViewModels
{
    /// <summary>
    /// viewmodel for searchvalueview
    /// </summary>
    class SearchValueViewModel : BaseViewModel
    {
        public static readonly SearchValueViewModel Instance = new SearchValueViewModel();

        public SearchCompanyBtn SearchCompany_Btn { get; }
        public SearchPersonBtn SearchPerson_Btn { get; }
        public SearchSignedInsuranceBtn SearchSignedI_Btn { get; }
        public SearchEmployeeBtn SearchEmployee_Btn { get; }
        public SearchApplicationBtn SearchApplication_Btn { get; }


        private SearchValueViewModel()
        {
            SearchCompany_Btn = new SearchCompanyBtn();
            SearchPerson_Btn = new SearchPersonBtn();
            SearchEmployee_Btn = new SearchEmployeeBtn();
            SearchApplication_Btn = new SearchApplicationBtn();
            SearchSignedI_Btn = new SearchSignedInsuranceBtn();
        }
       
    }

   
}

