using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BussinessLayer
{
    public class SalesMenController
    {
        public SalesMen GetSalesMen(int id) => BusinessController.Instance.Context.Employees.GetById(id);
        public IEnumerable<SalesMen> GetAllSalesMen() => BusinessController.Instance.Context.Employees.GetAll();
       
        public void AddSalesMen(SalesMen sm)
        {
            BusinessController.Instance.Context.Employees.Add(sm);
            BusinessController.Instance.Save();
        }


        public void RemoveSalesMen(SalesMen sm)
        {
            BusinessController.Instance.Context.Employees.Remove(sm);
            BusinessController.Instance.Save();
        }

        public void EditSalesMen(SalesMen sm)
        {
            SalesMen edit = BusinessController.Instance.Context.Employees.GetById(sm.AgentNumber);
            edit.Lastname = sm.Lastname;
            edit.Firstname = sm.Firstname;
            edit.TaxRate = sm.TaxRate;
            edit.StreetAddress = sm.StreetAddress;
            edit.City = sm.City;
            edit.Postalcode = sm.Postalcode;
            BusinessController.Instance.Save();
        }

        public void CheckExistingSalesMen(int id, SalesMen a)
        {
            SalesMen x = BusinessController.Instance.Context.Employees.GetById(id);
            if (x == null)
            {

                var msg = $"Agentnummer: {a.AgentNumber}";
                MessageBox.Show(msg, "Ny anställd att lägga till", MessageBoxButton.OK, MessageBoxImage.Information);
                AddSalesMen(a);
            }
            else
            {
                MessageBox.Show("Går ej lägga till ny anställd då anställningsnumret redan finns");
            }
        }
    }
}
