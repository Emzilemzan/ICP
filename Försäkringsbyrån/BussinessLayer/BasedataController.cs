using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BussinessLayer
{
    public class BasedataController
    {
        #region Controls for BaseAmountTabel
        public BaseAmountTabel GetBaseAmountTable(int id) => BusinessController.Instance.Context.Tables.Find(x => x.BaseAmountTId == id).FirstOrDefault();

        public IEnumerable<BaseAmountTabel> GetAllTables() => BusinessController.Instance.Context.Tables.GetAll();
        public void AddBaseAmountTable(BaseAmountTabel baseAmountTabel)
        {
            BusinessController.Instance.Context.Tables.Add(baseAmountTabel);
            BusinessController.Instance.Save();
        }
        public void RemoveBaseAmountTable(BaseAmountTabel baseAmountTabel)
        {
            BusinessController.Instance.Context.Tables.Remove(baseAmountTabel);
            BusinessController.Instance.Save();
        }

        public void CheckExistingTable(int id, BaseAmountTabel a)
        {
            BaseAmountTabel x = BusinessController.Instance.Context.Tables.GetById(id);
            if (x != null)
            {
                RemoveBaseAmountTable(a);
                MessageBox.Show("Grunddatan togs bort");
            }
            else
            {
                MessageBox.Show("Finns ingen grunddata med det id.t att ta bort");
            }
        }


        #endregion

        #region Controls for VacationPay

        public VacationPay GetVacationPay(int id) => BusinessController.Instance.Context.VPays.Find(x => x.SEId == id).FirstOrDefault();

        public IEnumerable<VacationPay> GetAllVPays() => BusinessController.Instance.Context.VPays.GetAll();

        public void AddVPay(VacationPay vPay)
        {
            BusinessController.Instance.Context.VPays.Add(vPay);
            BusinessController.Instance.Save();
        }

        public void RemoveVPay(VacationPay vPay)
        {
            BusinessController.Instance.Context.VPays.Remove(vPay);
            BusinessController.Instance.Save();
        }

        public void CheckExistingVPay(int id, VacationPay a)
        {
            VacationPay x = BusinessController.Instance.Context.VPays.GetById(id);
            if (x != null)
            {
                AddVPay(a);
                MessageBox.Show("Grunddatan togs bort");
            }
            else
            {
                MessageBox.Show("Finns ingen grunddata med det id.t att ta bort");
            }
        }

        #endregion

        #region Controls for BaseAmountOption
        public OptionalType GetOptionalType(int id) => BusinessController.Instance.Context.OptionalTypes.Find(x => x.OptionalTypeId == id).FirstOrDefault();

        public IEnumerable<OptionalType> GetAllOptionalTypes() => BusinessController.Instance.Context.OptionalTypes.GetAll();

        public void AddBaseAmountOption (OptionalType optionalType)
        {
            BusinessController.Instance.Context.OptionalTypes.Add(optionalType);
            BusinessController.Instance.Save();
        }

        public void RemoveBaseAmountOption (OptionalType optionalType)
        {
            BusinessController.Instance.Context.OptionalTypes.Remove(optionalType);
            BusinessController.Instance.Save();
        }

        public void CheckExistingBaseAmountOption(int id, OptionalType o)
        {
            OptionalType x = BusinessController.Instance.Context.OptionalTypes.GetById(id);
            if (x != null)
            {
                AddBaseAmountOption(o);
                MessageBox.Show("Grunddatan togs bort");
            }
            else
            {
                MessageBox.Show("Finns ingen grunddata med det id.t att ta bort");
            }
        }
        #endregion
    }
}
