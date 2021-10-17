using GUILayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels
{
    /// <summary>
    /// Welcome page
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        public static readonly HomeViewModel Instance = new HomeViewModel();

        private HomeViewModel()
        {
          
        }
        public string Fullname => Context.CurrentUser.Firstname + " " + Context.CurrentUser.Lastname + "!";
    }
}
