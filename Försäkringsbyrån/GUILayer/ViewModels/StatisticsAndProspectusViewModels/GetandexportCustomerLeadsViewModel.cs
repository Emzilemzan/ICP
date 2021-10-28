using GUILayer.Commands;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.StatisticsAndProspectusViewModels
{
    /// <summary>
    /// customerprospect
    /// </summary>
    public class GetandexportCustomerLeadsViewModel : BaseViewModel
    {
        public static readonly GetandexportCustomerLeadsViewModel Instance = new GetandexportCustomerLeadsViewModel();
        public GetandexportCustomerLeadsViewModel()
        {
            Update();
        }
        #region commands
        public void Update()
        {
            Insurances = GetProspects();
        }
        public ObservableCollection<Insurance> Insurances { get; set; }
        private void ExportToCsv()
        {
            if (Insurances != null)
            {
                foreach (var i in Insurances)
                {
                    string date = DateTime.Today.ToString("MM-dd-yyyy");
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter.GetInstance(document, new FileStream(i.PersonTaker.SocialSecurityNumber + "Kundprospekt.pdf", FileMode.Create));
                    BaseFont basefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                    Font times = new Font(basefont, 15);
                    document.Open();
                    document.Add(new Paragraph("Kundprospekt", times));
                    document.Add(new Paragraph("Utskriftsdatum: \t" + date));
                    document.Add(new Paragraph("Försäkringstagaren: \t"));
                    document.Add(new Paragraph("Personnummer: \t" + i.PersonTaker.SocialSecurityNumber));
                    document.Add(new Paragraph("Namn: \t" + i.PersonTaker.Firstname + " " + i.PersonTaker.Lastname));
                    document.Add(new Paragraph("Gatuadress: \t" + i.PersonTaker.StreetAddress));
                    document.Add(new Paragraph("Postnummer: \t" + i.PersonTaker.PostalCode));
                    document.Add(new Paragraph("Postort: \t" + i.PersonTaker.City));
                    document.Add(new Paragraph("Rikt- & telefonnummer bostad: \t" + i.PersonTaker.DiallingCodeHome + "-" + i.PersonTaker.TelephoneNbrHome));
                    document.Add(new Paragraph("Email: \t" + i.PersonTaker.EmailOne));
                    document.Add(new Paragraph("Email: \t" + i.PersonTaker.EmailTwo));
                    document.Add(new Paragraph("Agenturnummer: \t" + i.AgentNo.AgentNumber));

                    document.Add(new Paragraph("\t"));

                    document.Add(new Paragraph("Kontaktdatum: ________________________________"));
                    document.Add(new Paragraph("Utfall: ______________________________________"));
                    document.Add(new Paragraph("Säljare: _____________________________________"));
                    document.Add(new Paragraph("Agentur: _____________________________________"));
                    document.Add(new Paragraph("Notering: ____________________________________"));
                    document.Add(new Paragraph(" _____________________________________________"));
                    document.Add(new Paragraph(" _____________________________________________"));

                    document.Close();
                    Process.Start(i.PersonTaker.SocialSecurityNumber + "Kundprospekt.pdf");
                    i.Prospect = true;
                    Context.IController.Edit(i);
                }
                Insurances.Clear();
                Update();
            }
            else
            {
                MessageBox.Show("De finns inga kundprospekt att hämta. ");
            }
        }
        private ICommand exportProspects_Btn;
        public ICommand ExportProspects_Btn => exportProspects_Btn ?? (exportProspects_Btn = new RelayCommand(x => { ExportToCsv(); }));
        #endregion
        #region Methods to find customerprospects
        public ObservableCollection<Insurance> GetProspects()
        {
            ObservableCollection<Person> people = new ObservableCollection<Person>();
            ObservableCollection<Insurance> insurances = new ObservableCollection<Insurance>();
            List<string> nbr = new List<string>();
            foreach (var it in Context.ITController.GetAllPersons())
            {
                foreach (var i in Context.IController.GetInsuranceTakerIAS(it))
                {
                    if (i.InsuranceStatus == Status.Tecknad)
                    {
                        nbr?.Add(it.TelephoneNbrHome);
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
                insurances = GetInsurance(people, nbr1);
            }
            else if (nbr2.Count > 0)
            {
                insurances = GetInsurance2(people, nbr2);
            }
            return Insurances = insurances;
        }
        /// <summary>
        /// Method that runs when duplicates exist of numbers in dbo.Persons. 
        /// </summary>
        /// <param name="people"></param>
        /// <param name="nbr2"></param>
        /// <returns></returns>
        public ObservableCollection<Insurance> GetInsurance(ObservableCollection<Person> people, List<string> nbr2)
        {
            ObservableCollection<Person> people2 = new ObservableCollection<Person>(); //all persons with child insurances. 
            ObservableCollection<Person> people3 = new ObservableCollection<Person>(); //all persons with adult insurances 
            ObservableCollection<Person> people4 = new ObservableCollection<Person>(); //all persons with the same homenbr that doesn't have an adult insurance. 
            ObservableCollection<Insurance> insurances = new ObservableCollection<Insurance>();
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
            foreach (var p in p4)
            {
                foreach (var i in p.Insurances)
                {
                    if (i.InsuranceStatus == Status.Tecknad && i.Prospect == false)
                    {
                        insurances?.Add(i);
                    }
                }
            }
            return insurances;
        }

        public ObservableCollection<Insurance> GetInsurance2(ObservableCollection<Person> people, List<string> nbr2)
        {
            ObservableCollection<Person> people2 = new ObservableCollection<Person>();
            ObservableCollection<Person> people3 = new ObservableCollection<Person>();
            ObservableCollection<Person> people4 = new ObservableCollection<Person>();
            ObservableCollection<Insurance> insurances = new ObservableCollection<Insurance>();
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
                    if (i.TypeName != "Sjuk- och olycksfallsförsäkring för barn" && i.InsuranceStatus == Status.Tecknad)
                    {
                        people3?.Add(p);
                    }
                }
            }
            var p4 = people2.Except(people3).ToList();
            foreach (var p in p4)
            {
                foreach (var i in p.Insurances)
                {
                    if (i.InsuranceStatus == Status.Tecknad && i.Prospect == false)
                    {
                        insurances?.Add(i);
                    }
                }
            }
            return insurances;
        }
        #endregion
    }
}

