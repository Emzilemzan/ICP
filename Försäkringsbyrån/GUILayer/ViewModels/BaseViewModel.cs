using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUILayer.ViewModels
{
    /// <summary>
    /// Base ViewModel that Creates the fundation for all viewModels
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BusinessController Context { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public BaseViewModel()
        {
            Context = BusinessController.Instance;
        }

        /// <summary>
        /// Informs everyone that is subscribed to a property that the property has uppdated its value
        /// </summary>
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
}
