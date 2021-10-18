using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
    //customerprospect
    public class GetandexportCustomerLeadsViewModel : BaseViewModel
    {
        public static readonly GetandexportCustomerLeadsViewModel Instance = new GetandexportCustomerLeadsViewModel();

        public GetandexportCustomerLeadsViewModel()
        {
            People = FillPeople();
        }
        private ICommand Searchleads_Btn;
        public ICommand GetandexportCustomerLeadsViewModel_Btn
        {
            get => Searchleads_Btn ?? (Searchleads_Btn = new RelayCommand(x => { GetandexportCustomerLeads(); }));
        }
        private void GetandexportCustomerLeads()
        {

        }

        public ObservableCollection<Person> People { get; set; }

        public ObservableCollection<Person> FillPeople()
        {
            ObservableCollection<Person> x = new ObservableCollection<Person>();
            foreach(var y in Context.IController.GetProspects())
            {
                x?.Add(y);
            }
            People = x;
            return People;
        }

    }


}
