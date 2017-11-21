using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    abstract class TariffPlan : ITariffPlan
    {
        public TariffPlan(string name, string description)
        {
            Name = name;
            Description = description;
            
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public abstract decimal GetCost(DateTime start, DateTime end);
    }
}
