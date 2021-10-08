using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

    }
}
