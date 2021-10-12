using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUILayer.Views.BasicDataViews
{
    /// <summary>
    /// Interaction logic for VacationPayView.xaml
    /// </summary>
    public partial class VacationPayView : UserControl
    {
        public VacationPayView()
        {
            InitializeComponent();
        }
        private bool IsAlphabetic(string s)
        {
            Regex r = new Regex(@"^[a-zA-Z]+$");

            return r.IsMatch(s);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (!IsAlphabetic(e.Text))
            {
                foreach (char ch in e.Text)
                    if (!char.IsDigit(ch) && ch.Equals('.'))
                        e.Handled = true;
            }
            else
            {
                MessageBox.Show("Påläggsmarginalen måste vara en siffra med eller utan decimal");
            }
        }
    }
}
