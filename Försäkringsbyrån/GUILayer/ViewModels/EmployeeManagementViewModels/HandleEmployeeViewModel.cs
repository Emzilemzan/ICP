using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    /// <summary>
    /// handle salesmen. 
    /// </summary>
    public class HandleEmployeeViewModel : BaseViewModel
    {
        public static readonly HandleEmployeeViewModel Instance = new HandleEmployeeViewModel();

        public HandleEmployeeViewModel()
        {
            SalesMens = UpdateSM();
            
        }

        #region methods
        public ObservableCollection<SalesMen> UpdateSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            foreach (var e in Context.SMController.GetAllSalesMen())
            {
                x?.Add(e);
            }
            SalesMens = x;
            return SalesMens;
        }
              
        #endregion

        private ICommand _updateEmployeeBtn;
        public ICommand UpdateEmployeeBtn
        {
            get => _updateEmployeeBtn ?? (_updateEmployeeBtn = new RelayCommand(x => { UpdateSalesMen(); CanCommand(); }));
        }


        public bool CanCommand() => true;

        private void UpdateSalesMen()
        {
            
        }

        #region properties
        private SalesMen _selectedPerson;

        public SalesMen SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public ObservableCollection<SalesMen> SalesMens { get; set; }


        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                if (_lastname.Length > 0)
                {
                    _lastname = value;
                    OnPropertyChanged("Lasttname");
                }
                else
                {
                    MessageBox.Show("Ett efternamn måste skrivas in");
                }
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (_firstname.Length > 0)
                {
                    _firstname = value;
                    OnPropertyChanged("Firstname");
                }
                else
                {
                    MessageBox.Show("Ett förnamn måste skrivas in");
                }
            }
        }
        private string _streetAddress;
        public string StreetAddress
        {
            get => _streetAddress;
            set
            {
                if (_streetAddress.Length > 0)
                {
                    _streetAddress = value;
                    OnPropertyChanged("StreetAddress");
                }
                else
                {
                    MessageBox.Show("En gatuadress måste skrivas in");
                }
            }
        }
        private int _postalCode;
        public string Postalcode
        {
            get => _postalCode > 0 ? _postalCode.ToString() : "";
            set
            {
                if (int.TryParse(value, out _postalCode) && _postalCode.ToString().Length < 6 && _postalCode != 0)
                {
                    OnPropertyChanged("PostalCode");
                }
                else
                {
                    MessageBox.Show("Postnummer måste bestå av 5 siffror och kan inte vara en text");
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (_city.Length > 0)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
                else
                {
                    MessageBox.Show("En postort måste skrivas in");
                }
            }
        }

        private int _agentNumber;
        public string AgentNumber
        {
            get => _agentNumber > 0 ? _agentNumber.ToString() : "";
            set
            {
                if (int.TryParse(value, out _agentNumber) && _agentNumber > 0 && _agentNumber != 0)
                {
                    OnPropertyChanged("AgentNumber");
                }
                else
                {
                    MessageBox.Show("Anställningsnummer måste vara ett nummer och får inte heller sättas till 0");
                }
            }
        }

        private double _taxRate;
        public string TaxRate
        {
            get => _taxRate > 0 ? _taxRate.ToString() : "";
            set
            {
                if (double.TryParse(value, out _taxRate) && _taxRate > 0 && _taxRate <= 100)
                {
                    OnPropertyChanged("TaxRate");
                }
                else
                {
                    MessageBox.Show("Skattesatsen måste vara ett nummer mellan 0 & 100");
                }
            }
        }


        #endregion
    }
}
