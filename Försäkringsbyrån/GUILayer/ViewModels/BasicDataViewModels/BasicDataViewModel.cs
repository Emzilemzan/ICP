using GUILayer.Commands.BasicDataCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels.BasicDataViewModels
{
    /// <summary>
    /// ViewModel for BasicDataView. 
    /// </summary>
    public class BasicDataViewModel: BaseViewModel
    {
        public static readonly BasicDataViewModel Instance = new BasicDataViewModel();

        public BaseAmountTableBtn AmountTable_Btn { get; }
        private BasicDataViewModel()
        {
            AmountTable_Btn = new BaseAmountTableBtn();
        }
    }
}
