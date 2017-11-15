using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Interfaces
{
    interface IPort:IClearEventHandlers
    {
         void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}
