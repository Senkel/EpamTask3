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
            //RegisterEventHandlersForStation(station);
        }

        public Dictionary<ITariffPlan, DateTime> GetTariffs(PhoneNumber number)
        {
            var tmp = new Dictionary<ITariffPlan, DateTime>();
            if (_repositoryOfTerminalTarifPlan.TryGetValue(number, out tmp))
                return tmp;
            else return null;
        }

        public ITariffPlan GetTariff(PhoneNumber number, DateTime date)
        {
            var tariffs = GetTariffs(number);
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

        public ICollection<BillingInfo> GetCallInfoForPeriod(PhoneNumber number, DateTime start, DateTime end)
        {
            return _repositiryOfBillingInfo.Where(x => x.Source == number && x.Started >= start && x.Ended <= end).OrderBy(y => y.Started).ToList();
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
