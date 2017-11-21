using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Interfaces
{
    interface ITariffPlan
    {
        string Name { get; }
        string Description { get;}
        
        decimal GetCost(DateTime start, DateTime end);

    }
}
