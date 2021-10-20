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
                foreach (var i in Context.IController.GetInsuranceTakerIAS(it))
                {
                    if (i.InsuranceStatus == Status.Tecknad)
                    {
                        nbr?.Add(it.TelephoneNbrHome); //alla nr för personer med tecknade försäkringar
                        insurances?.Add(i); // tecknade försäkringar för alla personer. 
                        people?.Add(it); // alla personer som har tecknade försäkringar 
                    }
                }
            }
            #region Check dupplicates of nbrs
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

            #endregion
            if (nbr1.Count > 0)
            {
                People = GetPerson(people, nbr1);
            }
            else if (nbr2.Count > 0)
            {
                People = GetPerson2(people, nbr2);
            }
            return People;
        }

        public ObservableCollection<Person> GetPerson(ObservableCollection<Person> people, List<string> nbr2)
        {
            ObservableCollection<Person> people2 = new ObservableCollection<Person>();
            ObservableCollection<Person> people3 = new ObservableCollection<Person>();
            ObservableCollection<Person> people4 = new ObservableCollection<Person>();
            ObservableCollection<Person> people5 = new ObservableCollection<Person>();
            List<string> nbr = new List<string>();
            foreach (var nr in nbr2)
            {
                foreach (var p in people)
                {
                    if (nr == p.TelephoneNbrHome)
                    {
                        foreach (var i in p.Insurances)
                        {
                            if (i.InsuranceStatus == Status.Tecknad && i.TypeName == "Sjuk- och olycksfallsförsäkring för barn") //Lägger till alla som har tecknade försäkringar.
                               
                            {
                                people3?.Add(p);
                            }
                            else if(i.InsuranceStatus == Status.Tecknad && i.TypeName != "Sjuk - och olycksfallsförsäkring för barn") //Lista bara personer var försäkringar är tecknade dessa är ej barn
                            {
                                people2?.Add(p);
                            }
                        }

                    }
                }
            }
            //foreach (var x in people3)
            //{
            //    people5?.Add(x);
            //}
            //foreach (var nbrs in nbr2)
            //{
            //    nbr?.Add(nbrs);
            //}
            //foreach (var nb in nbr2)
            //{
            //    foreach (var n in nbr)
            //    {
            //        if (nb == n)
            //        {
            //            foreach (var s in people3)
            //            {
            //                foreach (var y in people5)
            //                {
            //                    if (s.TelephoneNbrHome == y.TelephoneNbrHome)
            //                    {
            //                        foreach (var i in s.Insurances)
            //                        {
            //                            foreach (var k in y.Insurances)
            //                            {
            //                                if (i.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && k.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Tecknad && k.InsuranceStatus == Status.Tecknad)
            //                                {
            //                                    people2?.Add(s);

            //                                }
            //                                else if (i.TypeName != "Sjuk- och olycksfallsförsäkring för barn" && k.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Otecknad && k.InsuranceStatus == Status.Tecknad)
            //                                {
            //                                    people2?.Add(s);
            //                                }
            //                                else if (i.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && k.TypeName != "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Otecknad && k.InsuranceStatus == Status.Otecknad)
            //                                {
            //                                    people2?.Add(s);
            //                                }

            //                            }
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //        else
            //        {
            //            foreach (var p in people)
            //            {
            //                if (n == p.TelephoneNbrHome)
            //                {
            //                    foreach (var i in p.Insurances)
            //                    {
            //                        if (i.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Tecknad) //om det finns tecknade försäkringar som är vuxenförsäkringar, skapa ej kundprospekt. 
            //                            people2?.Add(p);

            //                    }

            //                }
            //            }
            //        }
            //    }
            //}

            var p4 = people2.Except(people3).ToList();

            foreach (var p in p4)
            {
                people4?.Add(p);
            }
            return people4;
        }

        public ObservableCollection<Person> GetPerson2(ObservableCollection<Person> people, List<string> nbr2)
        {
            ObservableCollection<Person> people2 = new ObservableCollection<Person>();
            ObservableCollection<Person> people3 = new ObservableCollection<Person>();
            ObservableCollection<Person> people4 = new ObservableCollection<Person>();

            foreach (var nr in nbr2)
            {
                foreach (var p in people)
                {
                    if (nr == p.TelephoneNbrHome)
                    {
                        foreach (var i in p.Insurances)
                        {
                            if (i.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Tecknad) //om det finns tecknade försäkringar som är vuxenförsäkringar, skapa ej kundprospekt. 
                                people2?.Add(p);

                        }

                    }
                }
            }
            var p4 = people2.Except(people3).ToList();

            foreach (var p in p4)
            {
                people4?.Add(p);
            }

            return people4;
        }
        #endregion
    }


}

