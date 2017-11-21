using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.MTS.Tariffs
{
    class Absolute : TariffPlan
    {
        public Absolute(string name, string description,int cost) : base(name, description)
        {
            Cost = cost;
        }

        private decimal Cost { get; set; }

        public override decimal GetCost(DateTime start, DateTime end)
        {
            var time = (decimal)(end - start).Minutes;
            if (time > 200)
                return time * 2 * Cost;
            else
                return time * Cost;
        }
    }
}
