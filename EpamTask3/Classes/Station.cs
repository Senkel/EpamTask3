using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    abstract class Station:IStation
    {
        ICollection<ITerminal> _connectionCollection;
        ICollection<ITerminal> _terminalCollection;
        ICollection<IPort> _portCollection;
        ICollection<CallInfo> _callCollection;
        IDictionary<PhoneNumber,IPort> _callMapping;

        public Station(ICollection<IPort> portCollection , ICollection<ITerminal> terminalCollection)
        {
            _portCollection = portCollection;
            _terminalCollection = terminalCollection;
            _callMapping = new Dictionary<PhoneNumber, IPort>();
            _callCollection = new List<CallInfo>();
            _connectionCollection = new List<ITerminal>();
        }

        public ITerminal GetTerminalByPhoneNumber(PhoneNumber number)
        {
            return _terminalCollection.FirstOrDefault(x => x.Number == number);
        }

        public IPort GetPortByPhoneNumber(PhoneNumber number)
        {
            return _callMapping[number];
        }

        public void ClearEvents()
        {
            throw new NotImplementedException();
        }

        public void RegisterOutgoingRequest(OutGoingCalls outGoingCalls, IncomingCalls incomingCalls)
        {
            if (outGoingCalls.Target != default(PhoneNumber) && incomingCalls.Source != default(PhoneNumber))
            {
                var callinfo = new CallInfo()
                {
                    Source = incomingCalls.Source,
                    Target = outGoingCalls.Target,
                    DateTime = DateTime.Now
                };

                ITerminal targetTerminal = GetTerminalByPhoneNumber(outGoingCalls.Target);
                IPort targetPort = GetPortByPhoneNumber(outGoingCalls.Target);

                if (targetPort.Condition == PortCondition.Free)
                {
                    _connectionCollection.Add(targetTerminal);
                    targetPort.Condition = PortCondition.Busy;
                   

                }
            }
        }

        public abstract void RegisterEventHandlersForPort(IPort port);
        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);

        private void MapTerminalToPort(ITerminal terminal,IPort port)
        {
            _callMapping.Add(terminal.Number,port);
            port.RegisterEventHandlersForTerminal(terminal);
            terminal.RegisterEventHandlersForPort(port);
        }

        public void Add(ITerminal terminal)
        {
            var portFree = _portCollection.Except(_callMapping.Values).FirstOrDefault();
            if (portFree != null)
            {
                _terminalCollection.Add(terminal);
                MapTerminalToPort(terminal, portFree);
                this.RegisterEventHandlersForPort(portFree);
                this.RegisterEventHandlersForTerminal(terminal);
            }
            
        }
        private void UnMapTerminalToPort(ITerminal terminal, IPort port)
        {
            _callMapping.Remove(terminal.Number);
            terminal.ClearEvents();
            port.ClearEvents();
        }

        public event EventHandler<CallInfo> NewCallInfo;//  when the station creates a new CallInfo for billing 
        

    }
}
