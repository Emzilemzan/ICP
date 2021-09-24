using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUILayer.Commands.SearchIndexCommands 
{ 
    /// <summary>
    /// command for search company
    /// </summary>
    public class SearchCompanyBtn : BaseCommand
    {
        public override bool CanExecute(object parameter = null) => true;

        public override void Execute(object parameter)
        {


        }
    }
}
