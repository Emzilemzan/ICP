using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace GUILayer.ViewModels.InsuranceViewModels
{
    /// <summary>
    /// FÖRETAGSFÖRSÄKRING!!!! REGISTRERA FÖRETAGSFÖRSÄKRING!!!!!!!
    /// </summary>
    public class RegisterApplicationViewModel : BaseViewModel
    {
        public static readonly RegisterApplicationViewModel Instance = new RegisterApplicationViewModel();

        /// Vad behövs här. properties för textboxarna...
        /// SSök möjlighet, skriver man i ett organisationummer som finns skall detta leveraeras och resultera i att textboxarna fylls i för försäkringstagerens uppgifter
        /// 
        /// Gör det int det ska användaren ha möjlighet att lägga till en ny. Alltså en Add InsuranceApplication metod som tar in alla ifyllda properties. 



    }
}
