using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUILayer.Commands
{
    public class BaseCommand : ICommand
    {
        public virtual event EventHandler CanExecuteChanged;
        public virtual bool CanExecute(object parameter) => true;
        public virtual void Execute(object parameter) { }
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
