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
    public class GetandexportSalesstatisticsViewModel : BaseViewModel
    {
        public static readonly GetandexportSalesstatisticsViewModel Instance = new GetandexportSalesstatisticsViewModel();
        private GetandexportSalesstatisticsViewModel()
        {
            Months = GetMonth();
            SalesMens = UpdateSM();
        }

        private ICommand GetStatics_Btn;

        public ICommand GetandexportSalesstatisticsViewModel_Btn
        {
            get => GetStatics_Btn ?? (GetStatics_Btn = new RelayCommand(x => { GetandexportSalesstatistics();  }));
        }

        public void GetandexportSalesstatistics()
        {

        }

        
        private ObservableCollection<SalesMen> _sms;
        public ObservableCollection<SalesMen> SalesMens 
        { 
            get =>_sms;
            set
            {
                _sms = value;
                OnPropertyChanged("SalesMens ");
            }
        }
        private SalesMen _salesMen;
        public SalesMen Agent
        {
            get => _salesMen;
            set
            {
                _salesMen = value;
                OnPropertyChanged("Agent");
            }
        }

        public List<int?> Months { get; set; }
        private int? _month;
        public int? Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }
        //Get all salesmen. 
        public ObservableCollection<SalesMen> UpdateSM()
        {
            ObservableCollection<SalesMen> x = new ObservableCollection<SalesMen>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.InsuranceStatus == Status.Tecknad)
                {
                    x?.Add(i.AgentNo);
                }
            }
            SalesMens = x;
            return SalesMens;
        }
        public List<int?> GetMonth()
        {
            List<int?> M1 = new List<int?>();
            foreach (var i in Context.IController.GetAllInsurances())
            {
                if (i.PayMonth != null && i.SAI != null)
                {
                    M1?.Add(i.PayMonth);
                }
            }
            return Months = M1.Union(M1).ToList();
        }
        private int _SAC;
        public int CountSAC
        {
            get => _SAC;
            set
            {
                _SAC = value;
                OnPropertyChanged("CountSAC");
            }
        }

        private int UpdateCSAC()
        {
            List<Insurance> k = new List<Insurance>();
            foreach (var insurance in Context.IController.GetAllInsurances())
            {
                if(insurance.AgentNo.Equals(Instance.Agent) && insurance.PayMonth == Month && insurance.SAI.SAID.Equals(1))
                {
                    k?.Add(insurance);
                }
            }
            int i = k.Count;
            return i;
        }


    }
}
