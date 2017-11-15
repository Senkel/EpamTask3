using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Interfaces
{
    interface ITerminal:IClearEventHandlers
    {
        event EventHandler Online; // when terminal is going to call mode

        event EventHandler Offline; // when the connection is interrupted

        event EventHandler Plugging; // when user plug the device

        event EventHandler UnPlugging; // when user unplug the device

        void Drop();

        void Answer();

        void Plug();

        void Unplug();

        void RegisterEventHandlersForPort(IPort port);
    }
}
