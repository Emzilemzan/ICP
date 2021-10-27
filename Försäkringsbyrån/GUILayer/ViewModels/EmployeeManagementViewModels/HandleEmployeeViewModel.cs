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
            
            
        }

        #region methods and commands
        //Method for filling datagrid with all existing salesmen in db. 
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

        public void UpdateGridToDb()
        {
            SalesMens = UpdateSM();
            if (SalesMens != null)
            {
                foreach (SalesMen sm in SalesMens)
                {
                    Context.SMController.Edit(sm);
                }
            }
        }

        private ICommand _deleteSMBtn;
        public ICommand DeleteSMBtn
        {
            get => _deleteSMBtn ?? (_deleteSMBtn = new RelayCommand(x => { DeleteSalesMen(); }));
        }
        //Method for deleting a salesmen
        private void DeleteSalesMen()
        {
            if (SelectedPerson != null && SelectedPerson.AgentNumber != 0 && SelectedPerson.City != null && SelectedPerson.Firstname != null && SelectedPerson.Lastname != null && SelectedPerson.StreetAddress != null
                && SelectedPerson.TaxRate != 0 && SelectedPerson.Postalcode != 0)
            {
                MessageBoxResult result = MessageBox.Show("Vill du ta bort säljaren?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool valid =Context.SMController.CheckSalesMenInInsurance(SelectedPerson);
                    if(valid == true)
                    {
                        MessageBox.Show("Det går inte att ta bort säljaren, då den finns registrerad på en försäkring.");
                    }
                    else
                    {
                        Context.SMController.RemoveSalesMen(SelectedPerson);
                        MessageBox.Show("Säljaren togs bort", "Lyckad borttagning", MessageBoxButton.OK, MessageBoxImage.Information);
                        SalesMens.Remove(SelectedPerson);
                        SalesMens.Clear();
                        foreach (var salesMen in Context.SMController.GetAllSalesMen())
                        {
                            SalesMens?.Add(salesMen);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"{SelectedPerson.AgentNumber} är inte borttaget");
                }
            }
            else
            {
                MessageBox.Show("Antingen har ingen säljare markerats i registret eller så har du lämnat något fält tomt! ", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region properties
        // SelectedPerson is used to select rowdata in datagrid. 
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

        public string Lastname
        {
            get => SelectedPerson.Lastname;
            set
            {
                if (SelectedPerson.Lastname != null)
                {
                    SelectedPerson.Lastname = value;
                    OnPropertyChanged("Lastname");
                }
                else
                {
                    MessageBox.Show("Ett efternamn måste skrivas in");
                }
            }
        }
        public string Firstname
        {
            get => SelectedPerson.Firstname;
            set
            {
                if (SelectedPerson.Firstname != null)
                {
                    SelectedPerson.Firstname = value;
                    OnPropertyChanged("Firstname");
                }
                else
                {
                    MessageBox.Show("Ett förnamn måste skrivas in");
                }
            }
        }
        public string StreetAddress
        {
            get => SelectedPerson.StreetAddress;
            set
            {
                if (SelectedPerson.StreetAddress.Length > 0)
                {
                    SelectedPerson.StreetAddress = value;
                    OnPropertyChanged("StreetAddress");
                }
                else
                {
                    MessageBox.Show("En gatuadress måste skrivas in");
                }
            }
        }
        public int Postalcode
        {
            get => SelectedPerson.Postalcode;
            set
            {
                    SelectedPerson.Postalcode = value;
                    OnPropertyChanged("PostalCode");
            }
        }
        public string City
        {
            get => SelectedPerson.City;
            set
            {
                if (SelectedPerson.City != null)
                {
                    SelectedPerson.City = value;
                    OnPropertyChanged("City");
                }
                else
                {
                    MessageBox.Show("En postort måste skrivas in");
                }
            }
        }

        public int AgentNumber
        {
            get => SelectedPerson.AgentNumber;
            set
            {
                    SelectedPerson.AgentNumber = value;
                    OnPropertyChanged("AgentNumber");
            }
        }

        public double TaxRate
        {
            get => SelectedPerson.TaxRate;
            set
            {
                    SelectedPerson.TaxRate = value;
                    OnPropertyChanged("TaxRate");
            }
        }


        #endregion
    }
}
