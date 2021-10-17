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
            Applications = UpdateAC();
            InsuranceGrid = CollectionViewSource.GetDefaultView(Applications);
            InsuranceGrid.Filter = new Predicate<object>(o => Filter(o as Insurance));
            Years = GetYears();
            Months = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12};
        }

        private ICommand _signInsurance;
        public ICommand SignInsurance
        {
            get=> _signInsurance ?? (_signInsurance = new RelayCommand(x => { SignInsuranceMethod(); }));
        }

        private void SignInsuranceMethod()
        {
            if (SelectedInsurance != null)
            {
                if(SelectedInsurance.InsuranceNumber != null && SelectedInsurance.SerialNumber != null)
                {
                    string SN = SerialNumber;
                    string str = Regex.Replace(SN, @"\d", "");

                    if (str == "LIV" || str == "SO")
                    {
                        if (SelectedInsurance.PossibleBaseAmount != null && SelectedInsurance.PossibleComisson == null)
                        {
                            SelectedInsurance.InsuranceNumber = InsuranceNumber;
                            SelectedInsurance.SerialNumber = SerialNumber;
                            SelectedInsurance.PayMonth = PayMonth;
                            SelectedInsurance.PayYear = PayYear;
                            SelectedInsurance.PossibleBaseAmount = PossibleBaseAmount;
                            SelectedInsurance.InsuranceStatus = Status.Tecknad;
                            Context.IController.Edit(SelectedInsurance);

                            MessageBox.Show($"Registreringen lyckades av: {SelectedInsurance.InsuranceNumber}", "Lyckad registrering", MessageBoxButton.OK, MessageBoxImage.Information);
                            Applications.Clear();
                            foreach (var e in Context.IController.GetAllInsurances())
                            {
                                if (e.InsuranceStatus == Status.Otecknad)
                                    Applications?.Add(e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("För LIV och SO försäkringar, ska bara grundbelopp fyllas i");
                        }
                    }
                    else
                    {
                        if (SelectedInsurance.PossibleComisson != null && SelectedInsurance.PossibleBaseAmount == null)
                        {
                            SelectedInsurance.InsuranceNumber = InsuranceNumber;
                            SelectedInsurance.SerialNumber = SerialNumber;
                            SelectedInsurance.PayMonth = PayMonth;
                            SelectedInsurance.PayYear = PayYear;
                            SelectedInsurance.InsuranceStatus = Status.Tecknad;
                            SelectedInsurance.PossibleComisson = PossibleComisson;
                            Context.IController.Edit(SelectedInsurance);
                            MessageBox.Show($"Registreringen lyckades av: {SelectedInsurance.InsuranceNumber}", "Lyckad registrering", MessageBoxButton.OK, MessageBoxImage.Information);
                            Applications.Clear();
                            foreach (var e in Context.IController.GetAllInsurances())
                            {
                                if (e.InsuranceStatus == Status.Otecknad)
                                    Applications?.Add(e);
                            }
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
                SelectedInsurance = null;
            }
        }
                                    
        #region Insurance properties
        public int? PayMonth
        {
            get => SelectedInsurance.PayMonth;
            set
            {
                SelectedInsurance.PayMonth = value;
                OnPropertyChanged("PayMonth");
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
        public List<int> Months { get; set; }
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

        public string InsuranceNumber
        {
            get => SelectedInsurance.InsuranceNumber;
            set
            {
                SelectedInsurance.InsuranceNumber = value;
                OnPropertyChanged("InsuranceNumber");
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
        public ObservableCollection<Insurance> UpdateAC()
        {
            ObservableCollection<Insurance> x = new ObservableCollection<Insurance>();
            foreach (var e in Context.IController.GetAllInsurances())
            {
                if(e.InsuranceStatus == Status.Otecknad)
                x?.Add(e);
            }
            Applications = x;
            return Applications;
        }

        private ICollectionView _applicationCollection;
        public ICollectionView InsuranceGrid
        {
            get => _applicationCollection;
            set 
            { 
                _applicationCollection = value; 
                OnPropertyChanged("InsuranceGrid"); 
            }
        }

        private bool Filter(Insurance application)
        {
            return SearchInput == null
                || application.SerialNumber.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || application.TypeName.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1
                || application.TakerNbr.ToString().IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
                InsuranceGrid.Refresh();
            }
        }

        public ObservableCollection<Insurance> Applications { get; set; }

        #endregion
        
    }
}
