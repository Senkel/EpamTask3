using EpamTask3.Interfaces;
using EpamTask3.MTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Billing
    {
        private ICollection<BillingInfo> _repositiryOfBillingInfo;
        private Dictionary<PhoneNumber, Dictionary<ITariffPlan, DateTime>> _repositoryOfTerminalTarifPlan;

        public Billing(IStation station)
        {
            _repositiryOfBillingInfo = new List<BillingInfo>();
            _repositoryOfTerminalTarifPlan = new Dictionary<PhoneNumber, Dictionary<ITariffPlan, DateTime>>();
            station.RegisterEventHandlersForBilling(this);
            RegisterEventHandlersForStation(station);
        }

        //public Dictionary<ITariffPlan, DateTime> GetTariffs(PhoneNumber number)
        //{
        //    var tmp = new Dictionary<ITariffPlan, DateTime>();
        //    if (_repositoryOfTerminalTarifPlan.TryGetValue(number, out tmp))
        //        return tmp;
        //    else return null;
        //}

        public ITariffPlan GetTariff(PhoneNumber number, DateTime date)
        {
            var tariffs = new Dictionary<ITariffPlan, DateTime>();
            var tmp = tariffs.Where(x => x.Value <= date).OrderByDescending(x => x.Value).LastOrDefault();
            return tmp.Key;
        }

        public void AddCallInfo(CallInfo callInfo)
        {
            var date = callInfo.Started;
            var phone = callInfo.Source;
            var tariff = GetTariff(phone, date);
            var info = new BillingInfo(tariff, callInfo);
            _repositiryOfBillingInfo.Add(info);
        }

        public ICollection<BillingInfo> GetBillingInfo(PhoneNumber number)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number).ToList();
        }

        public bool AddTerminal(PhoneNumber number, ITariffPlan tariffPlan)
        {
            if (_repositoryOfTerminalTarifPlan.ContainsKey(number))
                return false;
            else
            {
                var tarifHistory = new Dictionary<ITariffPlan, DateTime>
                {
                    { tariffPlan, DateTime.Now }
                };

                _repositoryOfTerminalTarifPlan.Add(number, tarifHistory);

                CreateTerminal(number);
                return true;
            }
        }

        public bool ChangeTariff(PhoneNumber number, ITariffPlan tariff)
        {
            if (_repositoryOfTerminalTarifPlan.TryGetValue(number, out Dictionary<ITariffPlan, DateTime> tariffHistory))
            {
                var lastChange = tariffHistory.LastOrDefault().Value;
                if (((DateTime.Now.Year - lastChange.Year) * 12 + (DateTime.Now.Month - lastChange.Month)) < 1)
                {
                    tariffHistory.Add(tariff, DateTime.Now);
                    return true;
                }
            }

            return false;
        }

        public ICollection<BillingInfo> GetDurationInfo(PhoneNumber number, DateTime start, DateTime end)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number && x.Duration == end - start).ToList();
        }

        public ICollection<BillingInfo> GetCostInfo(PhoneNumber number, TariffPlan tariffPlan,DateTime start,DateTime end)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number && x.Cost == tariffPlan.GetCost(start, end)).ToList();
        }

        public ICollection<BillingInfo> GetCallInfoForPeriod(PhoneNumber number, DateTime start, DateTime end)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number && x.Started >= start && x.Ended <= end).OrderBy(y => y.Started).ToList();
        }

        public ICollection<BillingInfo> GetCallInfoForPeriodOrderedByPrice(PhoneNumber number, DateTime start, DateTime end)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number && x.Started >= start && x.Ended <= end).OrderBy(y => y.Cost).ToList();
        }

        public ICollection<BillingInfo> GetCallInfoForPeriodOrderedByTarget(PhoneNumber number, DateTime start, DateTime end)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number && x.Started >= start && x.Ended <= end).OrderBy(y => y.Target).ToList();
        }

        public void RegisterEventHandlersForStation(IStation station)
        {
            station.NewCallInfo += (sender, callInfo) =>
            {
                var temp = callInfo;
                if (temp != null)
                {
                    AddCallInfo(temp);
                }
            };
        }

        public event EventHandler<ITerminal> TerminalRegistered;

        protected virtual void OnTerminalRegistered(object sender, ITerminal terminal)
        {
            TerminalRegistered?.Invoke(sender, terminal);
        }

        private void CreateTerminal(PhoneNumber number)
        {
            var terminal = new MTSTerminal(number);

            OnTerminalRegistered(this, terminal);
        }


    }
}
