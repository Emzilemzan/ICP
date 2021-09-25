using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels.CommissionViewModels
{
    /// <summary>
    /// Viewmodel for commissionview. 
    /// </summary>
    public class CommissionViewModel: BaseViewModel
    {
        public static readonly CommissionViewModel Instance = new CommissionViewModel();


        private CommissionViewModel()
        {

        }
    }
}
