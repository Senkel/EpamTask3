using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpamTask3.Interfaces;

namespace EpamTask3.MTS
{
    class MTSStation : Station
    {
        public MTSStation(ICollection<IPort> portCollection, ICollection<ITerminal> terminalCollection) : base(portCollection, terminalCollection)
        {
        }


        public void OnOutgoingRequest(object sender, Calls request)
        {
            if (request.GetType() == typeof(OutGoingCalls))
            {
                RegisterOutgoingRequest(request as OutGoingCalls);
            }
        }

        public override void RegisterEventHandlersForBilling(Billing billing)
        {
            billing.TerminalRegistered += (sender, terminal) => { Add(terminal); };
        }



        public override void RegisterEventHandlersForPort(IPort port)
        {
            port.ConditionChanged += (sender, condition) => { Console.WriteLine("Station detected the port changed its State to {0}", condition); };
        }


        public override void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingConnection += OnOutgoingRequest;
            terminal.IncomingRespond += OnIncomingCallRespond;
        }
    }
}
