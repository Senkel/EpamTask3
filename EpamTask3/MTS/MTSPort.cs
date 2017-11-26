using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpamTask3.Interfaces;

namespace EpamTask3.MTS
{
    class MTSPort : Port
    {
        public void OnOutgoingCall(object sender, Calls calls)
        {
            if (calls.GetType() == typeof(OutGoingCalls) && Condition == PortCondition.Free)
            {
                Condition = PortCondition.Busy;
            }
        }

        public override void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.Plugging += (port, args) => { Condition = PortCondition.Free; };
            terminal.UnPlugging += (port, args) => { Condition = PortCondition.Unplagged; };
            terminal.OutgoingConnection += OnOutgoingCall;

        }

        public MTSPort()
        {
            ConditionChanged += (sender, condition) => { Console.WriteLine("Port detected the State is changed to {0}", condition); };
        }
    }
}
