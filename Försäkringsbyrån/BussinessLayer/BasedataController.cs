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
        public BaseAmount GetBaseAmount(int id) => BusinessController.Instance.Context.BaseAmounts.Find(x => x.BaseAmountId == id).FirstOrDefault();

        public IEnumerable<BaseAmount> GetAllBaseAmount() => BusinessController.Instance.Context.BaseAmounts.GetAll();

        public void AddBaseAmountOption (BaseAmount optionalType)
        {
            BusinessController.Instance.Context.BaseAmounts.Add(optionalType);
            BusinessController.Instance.Save();
        }

        public void RemoveBaseAmountOption (BaseAmount optionalType)
        {
            BusinessController.Instance.Context.BaseAmounts.Remove(optionalType);
            BusinessController.Instance.Save();
        }

        public void CheckExistingBaseAmountOption(int id, BaseAmount o)
        {
            BaseAmount x = BusinessController.Instance.Context.BaseAmounts.GetById(id);
            if (x != null)
            {
                RemoveBaseAmountOption(o);
                MessageBox.Show("Grunddatan togs bort");
            }
            else
            {
                MessageBox.Show("Finns ingen grunddata med det id.t att ta bort");
            }
        }
        #endregion

        #region Controls for AckValueVariable
        public AckValueVariable GetAckValue(int id) => BusinessController.Instance.Context.AckValues.Find(x => x.AckValueID == id).FirstOrDefault();
        public IEnumerable<AckValueVariable> GetAllAckValues() => BusinessController.Instance.Context.AckValues.GetAll();

        public void AddAckValue (AckValueVariable av)
        {
            BusinessController.Instance.Context.AckValues.Add(av);
            BusinessController.Instance.Save();
        }

        public void RemoveAckValue (AckValueVariable av)
        {
            BusinessController.Instance.Context.AckValues.Remove(av);
            BusinessController.Instance.Save();
        }

        public void CheckExistingAckValue(int id, AckValueVariable a)
        {
            AckValueVariable x = BusinessController.Instance.Context.AckValues.GetById(id);
            if (x !=null)
            {
                RemoveAckValue(a);
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
