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
        /// <summary>
        /// check if id existis in database before remove. 
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="a"></param>
        public void CheckExistingTable(int id, BaseAmountTabel a)
        {
            BaseAmountTabel x = BusinessController.Instance.Context.Tables.GetById(id);
            if (x != null)
            {
                CheckBdIsInInsurance(a);
            }
            else
            {
                MessageBox.Show("Finns ingen grunddata med det id.t att ta bort");
            }
        }

        /// <summary>
        /// Check if you can remove baseamount, only if its not included in any insurance. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="a"></param>
        public void CheckBdIsInInsurance(BaseAmountTabel a)
        {
            foreach (var i in BusinessController.Instance.Context.Insurances.GetAll())
            {
                if (i.SAI != null)
                {
                    if (i.SAI.Tabels.Contains(a))
                    {
                        MessageBox.Show("Du kan inte ta bort denna grunddatan, då den finns registrerad på en ansökan eller en tecknad försäkring");
                    }
                }
              
                else
                {
                    RemoveBaseAmountTable(a);
                    MessageBox.Show("Grunddatan togs bort");
                }
            }
        }

        /// <summary>
        /// Check number of existing baseamounts, only allows a maxium per year. 
        /// </summary>
        public void CheckNbrOfBASA(SAInsurance s, DateTime d, BaseAmountTabel baseAmountTabel)
        {
            List<BaseAmountTabel> bases = new List<BaseAmountTabel>();
            List<BaseAmountTabel> bases2 = new List<BaseAmountTabel>();
            foreach (BaseAmountTabel b in GetAllTables())
            {
                if (b.SAID == s && b.Date.Year == d.Year)
                {
                    if(b.SAID.SAID == 2)
                    {
                        bases.Add(b);
                    }
                    else if(b.SAID.SAID == 1)
                    {
                        bases2.Add(b);
                    }
                }
            }
            if (bases.Count != 3 && s.SAID == 2 && s.SAID != 1 )
            {
                AddBaseAmountTable(baseAmountTabel);
                MessageBox.Show("Grundbeloppet las till.");
            }
            else if (bases2.Count != 4 && s.SAID == 1 && s.SAID != 2)
            {
                AddBaseAmountTable(baseAmountTabel);
                MessageBox.Show("Grundbeloppet las till.");
            }
            else
            {
                MessageBox.Show("Grundbeloppet las inte till då det får max finnas 4 st per år för SObarn och 3 st per år för SOvuxen");
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
                RemoveVPay(a);
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

        public double CountAckvalueOt(DateTime d, OptionalType ot, int i)
        {
            double y = 0;
            if (i == 0 && ot == null)
            {
                y = 0;
            }
            else
            {
                foreach (var av in GetAllAckValues())
                {
                    if (d.Year.Equals(av.Date.Year) && ot.Equals(av.OptionalTypeId))
                    {
                        y = i * av.AckValue;
                    }
                }
            }
            return y;
        }

        public double CountAckvalueLife(DateTime d, LifeInsurance l, int i)
        {
            double y = 0;
            if (i == 0 && l==null)
            {
                y = 0;
            }
            else
            {
                foreach (var av in GetAllAckValues())
                {
                    if (d.Year.Equals(av.Date.Year) && l.Equals(av.LIFEID))
                    {
                        y = i * av.AckValue;
                    }
                }
            }
            
            return y;
        }
        #endregion

        #region Controls for Commission
        public ComissionShare GetCommissionShare(int id) => BusinessController.Instance.Context.CommissionShares.Find(x => x.PAId == id).FirstOrDefault();
        public IEnumerable<ComissionShare> GetAllCommissionShares() => BusinessController.Instance.Context.CommissionShares.GetAll();

        public void AddCommissionShare (ComissionShare cs)
        {
            BusinessController.Instance.Context.CommissionShares.Add(cs);
            BusinessController.Instance.Save();
        }

        public void RemoveCommissionShare (ComissionShare cs)
        {
            BusinessController.Instance.Context.CommissionShares.Remove(cs);
            BusinessController.Instance.Save();

        }

        public void CheckExistingCommissionShare (int id, ComissionShare cs)
        {
            ComissionShare x = BusinessController.Instance.Context.CommissionShares.GetById(id);
            if (x !=null)
            {
                RemoveCommissionShare(cs);
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
