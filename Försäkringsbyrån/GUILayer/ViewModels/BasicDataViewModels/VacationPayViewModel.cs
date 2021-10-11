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
            Years = GetYears();
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
            if (Instance._year != 0 && Instance._addPerc != 0)
            {
                VacationPay v = new VacationPay()
                {
                    AdditionalPercentage = Instance._addPerc,
                    Year = Instance._year
                };
                Context.BDController.AddVPay(v);

                MessageBox.Show("Semesterersättning är uppdaterad");
                VPays.Clear();
                foreach (var t in Context.BDController.GetAllVPays())
                {
                    VPays?.Add(t);
                }
                MainViewModel.Instance.SelectedViewModel = null;
                MainViewModel.Instance.SelectedViewModel = Instance;
                
            }
            else
            {
                MessageBox.Show("Inget fält får lämnas tomt!");
            }
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
        //För att visa årtal i combobox. 
        public List<int> GetYears()
        {
            return Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).Reverse().ToList();
        }

        #region properties
        public ObservableCollection<VacationPay> VPays { get; set; }
       
        private int _sEId;
        public int SEId
        {
            get => _sEId;
            set
            {
                _sEId = value;
                OnPropertyChanged("SEId");
            }
        }

        private double _addPerc;
        public string AdditionalPercentage
        {
            get => _addPerc > 0 ? _addPerc.ToString() : "";
            set
            {
                if (double.TryParse(value, out _addPerc))
                {
                    OnPropertyChanged("AdditionalPercentage");
                }
            }
        }

        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }
        #endregion
    }
}
