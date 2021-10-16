using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class SignedLifeInsuranceViewModel: BaseViewModel
    {
        public static readonly SignedLifeInsuranceViewModel Instance = new SignedLifeInsuranceViewModel();
        public SignedLifeInsuranceViewModel()
        {

        }

        private ICommand _addSignedLife;
        public ICommand AddSignedLife
        {
            get => _addSignedLife ?? (_addSignedLife = new RelayCommand(x => { AddSigned(); }));
        }

       private void AddSigned()
       {

       }

    }
}
