using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Main ViewModel that Creates the fundation for all viewModels
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public BaseViewModel()
        {

        }
        /// <summary>
        /// Informs everyone that is subscribed to a property that the property has uppdated its value
        /// </summary>
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
