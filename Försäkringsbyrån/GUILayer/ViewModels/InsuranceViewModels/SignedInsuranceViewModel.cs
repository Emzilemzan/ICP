using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    public class SignedInsuranceViewModel : BaseViewModel
    {
        public static readonly SignedInsuranceViewModel Instance = new SignedInsuranceViewModel();
        public SignedInsuranceViewModel()
        {
            Applications = Update();
            UpdateAC();
            Years = GetYears();
            Months = new List<string>() { "Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December" };
        }

        private ICommand _signInsurance;
        public ICommand SignInsurance
        {
            get => _signInsurance ?? (_signInsurance = new RelayCommand(x => { SignInsuranceMethod(); }));
        }

        private void SignInsuranceMethod()
        {
            if (SelectedInsurance != null && Month != null && Year != null)
            {
                if (INumber != null && SelectedInsurance.SerialNumber != null)
                {
                    string SN = SerialNumber;
                    string str = Regex.Replace(SN, @"\d", "");

                    if (str == "LIV" || str == "SOV" || str == "SOB")
                    {
                        if (_pBC == 0 && _pBA != 0)
                        {
                            SelectedInsurance.InsuranceNumber = INumber;
                            SelectedInsurance.SerialNumber = SerialNumber;
                            SelectedInsurance.PayMonth = GetMonth(Month);
                            SelectedInsurance.PayYear = Year;
                            SelectedInsurance.PossibleBaseAmount = _pBA;
                            SelectedInsurance.InsuranceStatus = Status.Tecknad;
                            SelectedInsurance.Prospect = false;
                            Context.IController.Edit(SelectedInsurance);

                            MessageBox.Show($"Registreringen lyckades av: {SelectedInsurance.InsuranceNumber}", "Lyckad registrering", MessageBoxButton.OK, MessageBoxImage.Information);
                            Applications.Clear();
                            foreach (var e in Context.IController.GetAllInsurances())
                            {
                                if (e.InsuranceStatus == Status.Otecknad)
                                    Applications?.Add(e);
                            }
                            MakeSearchWordEmpty();
                        }
                        else
                        {
                            MessageBox.Show("För LIV och SO försäkringar, ska bara grundbelopp fyllas i");
                        }
                    }
                    else
                    {
                        if (_pBA == 0 && _pBC != 0)
                        {
                            SelectedInsurance.InsuranceNumber = INumber;
                            SelectedInsurance.SerialNumber = SerialNumber;
                            SelectedInsurance.PayMonth = GetMonth(Month);
                            SelectedInsurance.PayYear = Year;
                            SelectedInsurance.InsuranceStatus = Status.Tecknad;
                            SelectedInsurance.PossibleComisson = _pBC;
                            Context.IController.Edit(SelectedInsurance);
                            MessageBox.Show($"Registreringen lyckades av: {SelectedInsurance.InsuranceNumber}", "Lyckad registrering", MessageBoxButton.OK, MessageBoxImage.Information);
                            Applications.Clear();
                            foreach (var e in Context.IController.GetAllInsurances())
                            {
                                if (e.InsuranceStatus == Status.Otecknad)
                                    Applications?.Add(e);
                            }
                            MakeSearchWordEmpty();
                        }
                        else
                        {
                            MessageBox.Show("För FF och Övriga försäkringar, ska bara provision fyllas i");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Försäkringsnumret måste fyllas i, samt provision eller grundbelopp.");
                }
            }
            else
            {
                MessageBox.Show("Försäkring måste ha markerats. Månad och år måste ha valts. Försäkringsnummer måste vara ifyllt, samt anitngen provision eller grundbelopp.");
            }
        }

        private int? GetMonth(string month)
        {
            int? x = 0;
            switch (month)
            {
                case "Januari":
                    x = 1;
                    break;
                case "Februari":
                    x = 2;
                    break;
                case "Mars":
                    x = 3;
                    break;
                case "April":
                    x = 4;
                    break;
                case "Maj":
                    x = 5;
                    break;
                case "Juni":
                    x = 6;
                    break;
                case "Juli":
                    x = 7;
                    break;
                case "Augusti":
                    x = 8;
                    break;
                case "September":
                    x = 9;
                    break;
                case "Oktober":
                    x = 10;
                    break;
                case "November":
                    x = 11;
                    break;
                case "December":
                    x = 12;
                    break;
            }
            return x;
        }

        #region Insurance properties
        private string _month;
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }

        public int? PayMonth
        {
            get => SelectedInsurance.PayMonth;
            set
            {
                SelectedInsurance.PayMonth = value;
                OnPropertyChanged("PayMonth");
            }
        }

        private int? _year;
        public int? Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }
        public int? PayYear
        {
            get => SelectedInsurance.PayYear;
            set
            {
                SelectedInsurance.PayYear = value;
                OnPropertyChanged("PayYear");
            }
        }
        public List<int> Years { get; set; }
        public List<string> Months { get; set; }
        //För att visa årtal i combobox. 
        public List<int> GetYears()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }


        private Insurance _si;
        public Insurance SelectedInsurance
        {
            get => _si;
            set
            {
                _si = value;
                OnPropertyChanged("SelectedInsurance");
            }
        }
        public string SerialNumber
        {
            get => SelectedInsurance.SerialNumber;
            set
            {
                SelectedInsurance.SerialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }

        private string _iNumber;

        public string INumber
        {
            get => _iNumber;
            set
            {
                _iNumber = value;
                OnPropertyChanged("INumber");
            }
        }

        public string InsuranceNumber
        {
            get => SelectedInsurance.InsuranceNumber;
            set
            {
                SelectedInsurance.InsuranceNumber = value;
                OnPropertyChanged("InsuranceNumber");
            }
        }

        private int _pBA;
        public string PBA
        {
            get => _pBA > 0 ? _pBA.ToString() : "";
            set
            {
                _pBA = 0;
                if (int.TryParse(value, out _pBA))
                {
                }
                OnPropertyChanged("PBA");
            }
        }
        public int? PossibleBaseAmount
        {
            get => SelectedInsurance.PossibleBaseAmount;
            set
            {
                SelectedInsurance.PossibleBaseAmount = value;
                OnPropertyChanged("PossibleBaseAmount");
            }
        }
        private int _pBC;
        public string PBC
        {
            get => _pBC > 0 ? _pBC.ToString() : "";
            set
            {
                _pBC = 0;
                if (int.TryParse(value, out _pBC))
                {
                }
                OnPropertyChanged("PBC");
            }
        }
        public int? PossibleComisson
        {
            get => SelectedInsurance.PossibleComisson;
            set
            {
                SelectedInsurance.PossibleComisson = value;
                OnPropertyChanged("PossibleComisson");
            }
        }

        #endregion
        #region Collection

        public void MakeSearchWordEmpty()
        {
            Check = true;
            SearchInput = string.Empty;
            Year = null;
            Month = null;
            PBC = string.Empty;
            PBA = string.Empty;
            INumber = string.Empty;
        }

        //searchmethod. 
        public void UpdateAC(string filter = "")
        {
            Applications = new ObservableCollection<Insurance>();
            List<Insurance> x = new List<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.InsuranceStatus == Status.Otecknad)
                    x?.Add(e);
            }
            if (filter != "")
            {
                List<Insurance> y = x;
                x = new List<Insurance>();
                foreach (Insurance i in y)
                    if (i.SerialNumber.Contains(filter) || i.TypeName.Contains(filter) || i.TakerNbr.Contains(filter))
                        x.Add(i);
            }
            x?.ForEach(i => Applications.Add(i));
        }
        public ObservableCollection<Insurance> Update()
        {
            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if (e.InsuranceStatus == Status.Otecknad)
                    x?.Add(e);
            }
            return Applications;
        }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                UpdateAC(SearchInput);
                OnPropertyChanged("SearchInput");

            }
        }
        private ObservableCollection<Insurance> _applications;
        public ObservableCollection<Insurance> Applications
        {
            get => _applications;
            set
            {
                _applications = value;
                OnPropertyChanged("Applications");
            }
        }

        private bool _check;
        public bool Check
        {
            get => _check;
            set
            {
                _check = value;
                OnPropertyChanged("Check");
            }
        }

        #endregion

    }
}
