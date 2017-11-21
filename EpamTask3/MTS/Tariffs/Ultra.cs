using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.MTS.Tariffs
{
    class Ultra : TariffPlan
    {
        private decimal Cost { get; set; }

        public Ultra(string name, string description,int cost) : base(name, description)
        {
            Cost = cost;
        }

        public override decimal GetCost(DateTime start, DateTime end)
        {
            return Cost;
        }
    }
}
