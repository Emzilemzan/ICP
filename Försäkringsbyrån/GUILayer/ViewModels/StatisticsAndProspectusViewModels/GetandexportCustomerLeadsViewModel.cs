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
            //Ppls = ExportToCsv();
        }
        //Hej Noah, när man klickat på export, då ska ett kundprospekt skapas. Alltså skapa en ny instans av klassen CustomerProspekt
        //Personen det berör ska således inte kunna finnas med i fler än ett kundprospekt. :) Behöver du hjälp me de säg till. 
        //Du kanske förstår lättare om du tittar i metoden GetPerson() längre ner. 

        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Person> Ppls { get; set; }

        public string ExportToCsv(ObservableCollection<Person> Ppls)
        {
            var output = new StringBuilder();
            // Add header if necessary
            output.Append("SocialSecurityNumber,");
            output.Append("Lastname ,");
            output.Append("Firstname ,");
            output.Append("StreetAddress ,");
            output.Append("PostalCode ,");
            output.Append("City ,");
            output.Append("DiallingCodeHome ,");
            output.Append("DiallingCodeWork ,");
            output.Append("TelephoneNbrHome ,");
            output.Append("TelephoneNbrWork ,");
            output.Append("EmailOne ,");
            output.Append("EmailTwo ,");
            output.AppendLine();
            // Add each row
            foreach (var person in Ppls)
            {
                output.AppendFormat("{0},", person.SocialSecurityNumber);
                output.AppendFormat("{0},", person.Lastname);
                output.AppendFormat("{0},", person.Firstname);
                output.AppendFormat("{0},", person.StreetAddress);
                output.AppendFormat("{0},", person.PostalCode);
                output.AppendFormat("{0},", person.City);
                output.AppendFormat("{0},", person.DiallingCodeHome);
                output.AppendFormat("{0},", person.DiallingCodeWork);
                output.AppendFormat("{0},", person.TelephoneNbrHome);
                output.AppendFormat("{0},", person.TelephoneNbrWork);
                output.AppendFormat("{0},", person.EmailOne);
                output.AppendFormat("{0},", person.EmailTwo);
                output.AppendLine();


            }

            System.IO.File.WriteAllText(@"c:\temp\output.txt", output.ToString());

            return output.ToString();
        }

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
                        nbr?.Add(it.TelephoneNbrHome);
                        insurances?.Add(i);
                        people?.Add(it);
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
        /// <summary>
        /// Method that runs when duplicates exist of numbers in dbo.Persons. 
        /// </summary>
        /// <param name="people"></param>
        /// <param name="nbr2"></param>
        /// <returns></returns>
        public ObservableCollection<Person> GetPerson(ObservableCollection<Person> people, List<string> nbr2)
        {
            ObservableCollection<Person> people2 = new ObservableCollection<Person>(); //all persons with child insurances. 
            ObservableCollection<Person> people3 = new ObservableCollection<Person>(); //all persons with adult insurances 
            ObservableCollection<Person> people4 = new ObservableCollection<Person>(); //all persons with the same homenbr that doesn't have an adult insurance. 
            
            List<string> nbr = new List<string>(); //all numbers connected to an insurance for adults. 
            foreach (var nr in nbr2)
            {
                foreach (var p in people)
                {
                    if (nr == p.TelephoneNbrHome)
                    {
                        foreach (var i in p.Insurances)
                        {
                            if (i.InsuranceStatus == Status.Tecknad && i.TypeName == "Sjuk- och olycksfallsförsäkring för barn")
                            {
                                people2?.Add(p);
                            }
                            else if (i.InsuranceStatus == Status.Tecknad && i.TypeName != "Sjuk - och olycksfallsförsäkring för barn")
                            {
                                nbr?.Add(nr);
                            }
                        }
                    }
                }
            }
            
            foreach (var nr in nbr)
            {
                foreach (var p in people2)
                {
                    if (nr == p.TelephoneNbrHome)
                    {
                        people3?.Add(p);
                    }
                }
            }
            var p4 = people2.Except(people3).ToList();

            foreach(var c in Context.ITController.GetProspects()) //Check if person already exists in a prospects, thus it can't be exported again.
            {
                foreach (var p in p4)
                {
                    if(!c.PersonProspect.Equals(p))
                    {
                        people4?.Add(p);
                    }
                }
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
                            if (i.TypeName == "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Tecknad)
                                people2?.Add(p);

                        }

                    }
                }
            }
            foreach (var p in people2)
            {
                foreach (var i in p.Insurances)
                {
                    if (i.TypeName != "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Tecknad) //alla som har tecknat barnförsäkring + vuxenförsäkringar
                        people3?.Add(p);

                }
            }

            var p4 = people2.Except(people3).ToList();

            foreach (var c in Context.ITController.GetProspects())
            {
                foreach (var p in p4)
                {
                    if (!c.PersonProspect.Equals(p))
                    {
                        people4?.Add(p);
                    }
                }
            }

            return people4;
        }
        #endregion

        //private ICommand exportProspects_Btn;
        //public ICommand ExportProspects_Btn
        //{
        //    get => exportProspects_Btn ?? (exportProspects_Btn = new RelayCommand(x => { ExportToCsv();  }));
        //}



    }


}

