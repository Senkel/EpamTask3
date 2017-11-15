using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Station:IStation
    {

        public Station(ICollection<IPort> portCollection , ICollection<ITerminal> terminalCollection)
        {

        }

        public void ClearEvents()
        {
            throw new NotImplementedException();
        }

        public void RegisterEventHandlersForPort(IPort port)
        {
            throw new NotImplementedException();
        }

        public void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            throw new NotImplementedException();
        }
    }
}
