using GUILayer.ViewModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands.SearchIndexCommands
{
    public class RemovePersonBtn : BaseCommand
    {
        private Person _thisPerson;
       // public RemovePersonBtn() => _thisPerson
        public override bool CanExecute(object parameter) => true;
        public override void Execute(object parameter)
        {
            MainViewModel.Instance.Context.ITController.RemovePersonInsuranceTaker(_thisPerson);

        }
    }
}
