using GUILayer.Commands;
using GUILayer.ViewModels.SearchViewModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUILayer.ViewModels.EmployeeManagementViewModels
{
    /// <summary>
    /// AddSalesMen 
    /// </summary>
    public class AddEmployeeViewModel : BaseViewModel
    {
        public static readonly AddEmployeeViewModel Instance = new AddEmployeeViewModel();

        public AddEmployeeViewModel()
        {
            
        }

        #region properties
        

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        private string _streetAddress;
        public string StreetAddress
        {
            get => _streetAddress;
            set
            {
                _streetAddress = value;
                OnPropertyChanged("StreetAddress");
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
                _city = value;
                OnPropertyChanged("City");
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
       
      

        #region Manages GUI happenings. 
        
        private ICommand _addEmployeeBtn;
        public ICommand AddEmployeeBtn
        {
            get => _addEmployeeBtn ?? (_addEmployeeBtn = new RelayCommand(x => { InsertSalesMen(); CanCommand(); }));
        }

        public bool CanCommand()
        {
            return !string.IsNullOrWhiteSpace(Instance.AgentNumber);
        }


        private void InsertSalesMen()
        {
            if (Instance._agentNumber != 0)
            {
                SalesMen salesMen = new SalesMen()
                {
                    AgentNumber = Instance._agentNumber,
                    Firstname = Instance._firstname,
                    Lastname = Instance._lastname,
                    StreetAddress = Instance._streetAddress,
                    City = Instance._city,
                    Postalcode = Instance._postalCode,
                    TaxRate = Instance._taxRate,
                };
                Context.SMController.CheckExistingSalesMen(Instance._agentNumber, salesMen);
                MainViewModel.Instance.ToolsVisibility = Visibility.Collapsed;
                MainViewModel.Instance.CurrentTool = "";
                HandleEmployeeViewModel.Instance.UpdateSM();
                MainViewModel.Instance.SelectedViewModel = HandleEmployeeViewModel.Instance;
            }
            else
            {
                MessageBox.Show("Anställningsnummer får inte lämnas tomt");
            }
        }
        #endregion
    }

}


