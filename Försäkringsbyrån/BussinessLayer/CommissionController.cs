using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// Manages commission counting. Get data from view mostly, since commission isn't a model. 
    /// </summary>
    public class CommissionController
    {
        public double CountCSumAck(SalesMen sm, List<string> Months, int Year, string month)
        {
            double sum = 0;
            if (sm.Insurances != null && month != null)
            {
                foreach (Insurance i in sm.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(month) + 1) % 12)
                    {
                        if (i.SAI != null)
                        {
                            if (i.SAI.SAID == 1)
                                sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                        }
                        else
                        {
                            sum = 0;
                        }
                    }
                }
            }
            return sum;
        }
        public double CountASumAck(SalesMen sm, List<string> Months, int Year, string month)
        {
            double sum = 0;
            if (sm.Insurances != null && month != null)
            {
                foreach (Insurance i in sm.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(month) + 1) % 12)
                    {
                        if (i.SAI != null)
                        {
                            if (i.SAI.SAID == 2)
                                sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4;
                        }
                        else
                        {
                            sum = 0;
                        }
                    }
                }
            }
            return sum;
        }
        public double CountLSumAck(SalesMen sm, List<string> Months, int Year, string month)
        {
            double sum = 0;
            if (sm.Insurances != null && month != null)
            {
                foreach (Insurance i in sm.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(month) + 1) % 12)
                    {
                        if (i.LIFE != null)
                            if (i.LIFE.LifeID == 1)
                            { sum += i.AckValue + i.AckValue2 + i.AckValue3 + i.AckValue4; }
                            else
                            {
                                sum = 0;
                            }
                    }
                }
            }
            return sum;
        }

        public double CountLsum(SalesMen sm, List<string> Months, int Year, string month)
        {
            double sum = 0;
            if (sm.Insurances != null && month != null)
            {
                foreach (Insurance i in sm.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(month) + 1) % 12)
                    {
                        if (i.LIFE != null)
                            if (i.LIFE.LifeID == 1)
                            { sum += i.BaseAmountValue; }
                            else
                            {
                                sum = 0;
                            }
                    }
                }
            }
            return sum;
        }

        public int CountOtherSumAck(SalesMen sm, List<string> Months, int Year, string month)
        {
            int? sum = 0;
            int sum1;
            if (sm.Insurances != null && month != null)
            {
                foreach (Insurance i in sm.Insurances)
                {
                    if (i.InsuranceStatus == 0 && i.PayYear == Year && i.PayMonth == (Months.IndexOf(month) + 1) % 12)
                    {
                        if (i.PossibleComisson != null)
                            sum += i.PossibleComisson;
                        else
                        {
                            sum = 0;
                        }
                    }
                }
            }
            sum1 = sum.Value;
            return sum1;
        }
        public double CountProvOther(int _otherCommission, VacationPay SelectedVPay)
        {
            double sum1;
            double sum;
            if (_otherCommission != 0)
            {
                double? newsum = _otherCommission * (1 - (SelectedVPay.AdditionalPercentage / 100));
                sum1 = newsum.Value;
                sum = Math.Round(sum1);
            }
            else
            {
                sum = 0;
            }
            return sum;
        }
        public double CountVPay(VacationPay SelectedVPay, double _provLiv, double _provSO, double _provOther)
        {
            double sum;
            double sum1;
            if (SelectedVPay != null)
            {
                sum1 = (_provLiv + _provSO + _provOther) * (SelectedVPay.AdditionalPercentage / 100);
                sum = Math.Round(sum1);
            }
            else
            {
                sum = 0;
            }
            return sum;
        }
        public double LeavingTax(double _sumCommission, SalesMen sm)
        {
            double? sum1 = _sumCommission * (sm.TaxRate / 100);
            double sum = sum1.Value;
            return Math.Round(sum);
        }
        public double CountProvLiv(SalesMen sm, List<string> Months, int Year, string month, VacationPay SelectedVPay)
        {
            double sum = CountLSumAck(sm, Months, Year, month) * (1 - (SelectedVPay.AdditionalPercentage / 100));
            return Math.Round(sum);
        }

        public double GetPermissionChild(double child, int Year)
        {
            double permission = 0;
            List<ComissionShare> cs = new List<ComissionShare>();
            foreach (ComissionShare c in BusinessController.Instance.BDController.GetAllCommissionShares())
            {
                if (c.CalenderYear == Year)
                {
                    cs?.Add(c);
                }
            }
            foreach (ComissionShare c in cs)
            {
                if (c.TotalMinAckValue <= child && c.TotalMaxAckValue >= child)
                {
                    permission = c.CommissionShareChildren;
                }
            }
            return permission;
        }
        public double GetPermissionAdult(double adult, int Year)
        {
            double permission = 0;
            List<ComissionShare> cs = new List<ComissionShare>();
            foreach (ComissionShare c in BusinessController.Instance.BDController.GetAllCommissionShares())
            {
                if (c.CalenderYear == Year)
                {
                    cs?.Add(c);
                }
            }
            foreach (ComissionShare c in cs)
            {
                if (c.TotalMinAckValue <= adult && c.TotalMaxAckValue >= adult)
                {
                    permission = c.ComissionShareAdults;
                }
            }
            return permission;
        }
        public double CountProvSo(double _cSumAck, double _aSumAck, VacationPay SelectedVPay, int year)
        {
            double pChild = GetPermissionChild(_cSumAck, year);
            double pAdult = GetPermissionAdult(_aSumAck, year);
            double sumChild = _cSumAck * pChild;
            double sumAdult = _aSumAck * pAdult;
            double sum = (sumChild + sumAdult) * (1 - (SelectedVPay.AdditionalPercentage / 100));
            return Math.Round(sum);
        }

    }
}
