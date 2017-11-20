using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Billing
    {
        private ICollection<CallInfo> _repositiryOfCallInfo;
        private IDictionary<PhoneNumber, IDictionary<ITariffPlan, DateTime>> _repositoryOfTerminalTarifPlan;

        public Billing()
        {
            _repositiryOfCallInfo = new List<CallInfo>();
            _repositoryOfTerminalTarifPlan = new Dictionary<PhoneNumber,IDictionary<ITariffPlan, DateTime>>();
        }

        public void AddCallInfo(CallInfo callInfo)
        {
            _repositiryOfCallInfo.Add(callInfo);
        }

        public ICollection<CallInfo> GetCallInfo(PhoneNumber number)
        {
            return _repositiryOfCallInfo.Where(x => x.Source == number).ToList();
        }

        public bool AddTerminal(PhoneNumber number, ITariffPlan tariffPlan)
        {
            if (_repositoryOfTerminalTarifPlan.ContainsKey(number))
                return false;
            else
            {
                var tarifHistory = new Dictionary<ITariffPlan, DateTime>();
                tarifHistory.Add(tariffPlan, DateTime.Now);

                _repositoryOfTerminalTarifPlan.Add(number, tarifHistory);
                return true;
            }
        }

        public ICollection<CallInfo> GetCallInfoForPeriod(PhoneNumber number,DateTime start,DateTime end)
        {
            return _repositiryOfCallInfo.Where(x => x.Source == number && x.Started==start && x.Ended==end ).ToList();
        }
    }
}
