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
            People = GetProspects();
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



        #region Methods to find customerprospects

        public ObservableCollection<Person> GetProspects()
        {
            ObservableCollection<Person> people = new ObservableCollection<Person>();
            List<Insurance> insurances = new List<Insurance>();
            List<string> nbr = new List<string>();
            foreach (var it in Context.ITController.GetAllPersons())
            {
                nbr?.Add(it.TelephoneNbrHome);

                foreach (var i in Context.IController.GetInsuranceTakerIAS(it))
                {
                    if (i.InsuranceStatus == Status.Tecknad)
                    {
                        insurances.Add(i); // tecknade försäkringar för alla personer. 
                    }
                }
                //alla nr som finns för alla personer. 

                people?.Add(it); //alla personer som finns.. 
            }

            List<string> nbr1 = new List<string>();
            List<string> nbr2 = new List<string>();
            foreach (var nr in nbr)
            {
                if (nbr.Count != nbr.Distinct().Count())
                {
                    nbr1?.Add(nr);
                    // Duplicates exist, flera personer med samma nr. 
                }
                else
                {
                    nbr2?.Add(nr);
                }
            }
            ObservableCollection<Person> people2 = new ObservableCollection<Person>();
            foreach (var nr in nbr2)   //bara för de personer som är ensamma om ett nr. 
            {
                foreach (var p in people)
                {
                    if (nr == p.TelephoneNbrHome && p.Insurances != null)
                    {
                        foreach (var i in p.Insurances)
                        {
                            if (i.InsuranceStatus == Status.Tecknad)
                            {
                                if (i.TypeName == "Sjuk- och olycksfallsförsäkring för barn") //om det finns tecknade försäkringar som är vuxenförsäkringar, skapa ej kundprospekt. 
                                    people2?.Add(p);
                            }
                        }
                    }
                }
            }
            foreach (var p in people2)
            {
                foreach (var i in p.Insurances)
                {
                    if (i.TypeName != "Sjuk- och olycksfallsförsäkring för barn")
                    {
                        people2?.Remove(p);
                    }
                }
            }

            People = people2;
            return People;
        }

        #endregion
    }
}