using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands.SearchIndexCommands
{
    /// <summary>
    /// command for search employee
    /// </summary>
    public class SearchEmployeeBtn: BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {


        }
    }
}
