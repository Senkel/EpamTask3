using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Interfaces
{
   public interface IPort:IClearEventHandlers
    {
       PortCondition Condition { get; set; }

        event EventHandler<PortCondition> ConditionChanging;
        event EventHandler<PortCondition> ConditionChanged;

        void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}
