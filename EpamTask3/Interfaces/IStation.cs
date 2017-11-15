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
    }
}
