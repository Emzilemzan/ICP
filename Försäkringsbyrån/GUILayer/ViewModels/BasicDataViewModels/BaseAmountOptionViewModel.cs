using BussinessLayer;
using GUILayer.Commands;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    public class BaseAmountOptionViewModel : BaseViewModel
    {
        public static readonly BaseAmountOptionViewModel Instance = new BaseAmountOptionViewModel();

        BasedataController controller = new BasedataController();

        public BaseAmountOptionViewModel()
        {
            //AllOptionTypes = new ObservableCollection<OptionalType>();
        }

        //public ObservableCollection<OptionalType> AllOptionTypes { get; set; }

        private ICommand _addBtn;
        public ICommand AddBaseAmountOption_Btn
        {
            get => _addBtn ?? (_addBtn = new RelayCommand(x => { AddBaseAmountOption(); CanCreate(); }));
        }

        public bool CanCreate() => true;

        public static void AddBaseAmountOption()
        {
            // Code to actualy add baseamount to the database.
        }

        private ICommand remove_Btn;
        public ICommand RemoveBaseAmountOption_Btn
        {
            get => remove_Btn ?? (remove_Btn = new RelayCommand(x => { RemoveBaseAmountOption(); CanCreate(); }));
        }

        public static void RemoveBaseAmountOption()
        {
            // Code to actualy add baseamount to the database.
        }

    }
}
