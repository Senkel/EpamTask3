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
        public TariffPlan(string name, string description, int cost)
        {
            Name = name;
            Description = description;
            Cost = cost;
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public int Cost { get; protected set; }

        public abstract int GetCost(int time);

    }
}
