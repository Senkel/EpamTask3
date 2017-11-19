using EpamTask3.Classes;
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

        event EventHandler<Calls> OutgoingConnection; // when the terminal try to connect to the station

        event EventHandler<IncomingCalls> IncomingRequest;//  when station try to connect to terminal

        event EventHandler<Respond> IncomingRespond; // when terminal send respond to the station

        PhoneNumber Number { get; }

        void Call(PhoneNumber target);

        void Drop();

        void Answer();

        void Plug();

        void Unplug();

        void RegisterEventHandlersForPort(IPort port);

        void IncomingCallFrom(PhoneNumber source);
    }
}
