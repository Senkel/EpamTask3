using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class BillingInfo : CallInfo
    {
        public decimal Cost { get; private set; }
        public BillingInfo(ITariffPlan tariff,CallInfo callInfo)
        {
            Cost = tariff.GetCost(callInfo.Started, callInfo.Ended);
            Source = callInfo.Source;
            Target = callInfo.Target;
            Started = callInfo.Started;
            Ended = callInfo.Ended;
            Duration = callInfo.Duration;
        }
    }
}
