using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Interfaces
{
    interface IStation:IClearEventHandlers
    {
        void RegisterEventHandlersForTerminal(ITerminal terminal);

        void RegisterEventHandlersForPort(IPort port);

        event EventHandler<CallInfo> NewCallInfo;

        event EventHandler<ITerminal> TerminalRegistered;

        void RegisterEventHandlersForBilling(Billing billing);
    }
}
