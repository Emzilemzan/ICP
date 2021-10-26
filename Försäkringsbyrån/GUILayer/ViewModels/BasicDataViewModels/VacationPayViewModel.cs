using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class VacationPayViewModel : BaseViewModel
    {
        public static VacationPayViewModel Instance = new VacationPayViewModel();

        public VacationPayViewModel()
        {
            VPays = UpdateVPay();
            YearsString = GetYearsString();
        }

        #region commands
        private ICommand _addBtn;
        public ICommand AddVPayBtn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddVPay(); CanCreate(); }));
        }

        public bool CanCreate() => true;

        private void AddVPay()
        {
            if (Instance.Year != null && Instance._addPerc != 0)
            {
                VacationPay v = new VacationPay()
                {
                    AdditionalPercentage = Instance._addPerc,
                    Year = int.Parse(Instance.Year),
                };
                Context.BDController.CheckNbrOfVP(int.Parse(Instance.Year), v);
                VPays.Clear();
                foreach (var t in Context.BDController.GetAllVPays())
                {
                    VPays?.Add(t);
                }
                
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Inget fält får lämnas tomt!");
            }
        }
        public void EmptyAllChoices()
        {
            Check = true;
            Year = null;
            AdditionalPercentage = string.Empty;
            SEId = string.Empty;
        }
        private void RemoveVPay()
        {
            if (Instance._sEId != 0)
            {
                VacationPay vp = Context.BDController.GetVacationPay(_sEId);
                MessageBoxResult result = MessageBox.Show("Vill du ta bort grunddatan?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Context.BDController.CheckExistingVPay(Instance._sEId, vp);
                    VPays.Clear();
                    foreach (var t in Context.BDController.GetAllVPays())
                    {
                        VPays?.Add(t);
                    }
                }
                else
                {
                    MessageBox.Show("Grunddatan togs inte bort.");
                }
                EmptyAllChoices();
            }
            else
            {
                MessageBox.Show("Ett id måste fyllas i");
            }
        }

        private ICommand remove_Btn;
        public ICommand RemoveVPayBtn
        {
            get => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveVPay(); CanCreate(); }));
        }

        #endregion

        #region properties
        private ObservableCollection<VacationPay> UpdateVPay()
        {
            ObservableCollection<VacationPay> x = new ObservableCollection<VacationPay>();
            foreach (var t in Context.BDController.GetAllVPays())
            {
                x?.Add(t);
            }
            VPays = x;
            return VPays;
        }

        public List<int> Years { get; set; }
        public List<int> GetYears()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }
        public List<string> YearsString { get; set; }

        public List<string> GetYearsString()
        {
            Years = GetYears();
            List<string> strings = new List<string>();
            foreach (var i in Years)
            {
                strings.Add(i.ToString());
            }
            return strings;
        }
        #endregion

        #region properties
        public ObservableCollection<VacationPay> VPays { get; set; }
       
        private int _sEId;
        public string SEId
        {
            get => _sEId > 0 ? _sEId.ToString() : "";
            set
            {
                _sEId = 0;
                if (int.TryParse(value, out _sEId))
                {
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("SEId");
            }
        }

        private double _addPerc;
        public string AdditionalPercentage
        {
            get => _addPerc > 0 ? _addPerc.ToString() : "";
            set
            {
                _addPerc = 0;
                if (double.TryParse(value, out _addPerc))
                { 
                }
                else if (Check == false)
                {
                    MessageBox.Show("Måste vara en siffra");
                }
                OnPropertyChanged("AdditionalPercentage");
            }
        }

        private string _year;
        public string Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
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
