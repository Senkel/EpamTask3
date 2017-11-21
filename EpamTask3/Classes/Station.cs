using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    abstract class Station : IStation
    {
       private ICollection<CallInfo> _connectionCollection;
       private ICollection<ITerminal> _terminalCollection;
       private ICollection<IPort> _portCollection;
       private ICollection<CallInfo> _callCollection;
       private IDictionary<PhoneNumber, IPort> _callMapping;

        public Station(ICollection<IPort> portCollection, ICollection<ITerminal> terminalCollection)
        {
            _portCollection = portCollection;
            _terminalCollection = terminalCollection;
            _callMapping = new Dictionary<PhoneNumber, IPort>();
            _callCollection = new List<CallInfo>();
            _connectionCollection = new List<CallInfo>();
        }

        protected ITerminal GetTerminalByPhoneNumber(PhoneNumber number)
        {
            return _terminalCollection.FirstOrDefault(x => x.Number == number);
        }

        public IPort GetPortByPhoneNumber(PhoneNumber number)
        {
            var t = _callMapping.TryGetValue(number, out IPort port);
            return port;
        }

        public void RegisterOutgoingRequest(OutGoingCalls outGoingCalls)
        {
            if (outGoingCalls.Target != default(PhoneNumber) && outGoingCalls.Source != default(PhoneNumber) && (GetCallInfo(outGoingCalls.Source) == null && GetConnectionInfo(outGoingCalls.Source) == null))
            {
                var time = DateTime.Now;
                var callinfo = new CallInfo()
                {
                    Source = outGoingCalls.Source,
                    Target = outGoingCalls.Target,
                    Started = time,
                    Ended = time
                };

                ITerminal targetTerminal = GetTerminalByPhoneNumber(outGoingCalls.Target);
                IPort targetPort = GetPortByPhoneNumber(outGoingCalls.Target);

                if (targetPort != null && targetPort.Condition == PortCondition.Free)
                {
                    _connectionCollection.Add(callinfo);
                    targetPort.Condition = PortCondition.Busy;
                    targetTerminal.IncomingCallFrom(outGoingCalls.Source);
                }
                else
                    OnNewCallInfo(this, callinfo);
            }
        }

        public abstract void RegisterEventHandlersForPort(IPort port);
        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);

        private void MapTerminalToPort(ITerminal terminal, IPort port)
        {
            _callMapping.Add(terminal.Number, port);
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
                RegisterEventHandlersForPort(portFree);
                RegisterEventHandlersForTerminal(terminal);
                OnTerminalRegistered(this, terminal);
            }

        }

        private void UnMapTerminalToPort(ITerminal terminal, IPort port)
        {
            _callMapping.Remove(terminal.Number);
            terminal.ClearEvents();
            port.ClearEvents();
        }

        public event EventHandler<CallInfo> NewCallInfo;//  when the station creates a new CallInfo for billing 

        public event EventHandler<ITerminal> TerminalRegistered;

        protected virtual void OnTerminalRegistered(object sender, ITerminal terminal)
        {
            TerminalRegistered?.Invoke(sender, terminal);
        }

        protected virtual void OnNewCallInfo(object sender, CallInfo callInfo)
        {
            NewCallInfo?.Invoke(sender, callInfo);
        }

        public void ActiveCall(CallInfo callInfo)
        {
            _callCollection.Remove(callInfo);
            callInfo.Started = DateTime.Now;
            _callCollection.Add(callInfo);
        }

        protected void InterruptConnection(CallInfo callInfo)
        {
            callInfo.Duration = DateTime.Now - callInfo.Started;
            callInfo.Ended = DateTime.Now;
            _callCollection.Remove(callInfo);
            _connectionCollection.Remove(callInfo);
            SetPortConditionWhenConnectionInterrupted(callInfo.Source, callInfo.Target);
            OnNewCallInfo(this, callInfo);
        }

        private void SetPortConditionWhenConnectionInterrupted(PhoneNumber source, PhoneNumber target)
        {
            var sourcePort = GetPortByPhoneNumber(source);
            if (sourcePort != null)
            {
                sourcePort.Condition = PortCondition.Free;
            }

            var targetPort = GetPortByPhoneNumber(target);
            if (targetPort != null)
            {
                targetPort.Condition = PortCondition.Free;
            }
        }

        protected CallInfo GetConnectionInfo(PhoneNumber number)
        {
            return _callCollection.FirstOrDefault(x => (x.Source.Phone == number.Phone || x.Target.Phone == number.Phone));
        }

        protected CallInfo GetCallInfo(PhoneNumber num)
        {
            return _callCollection.FirstOrDefault(x => (x.Source.Phone == num.Phone || x.Target.Phone == num.Phone));
        }

        public void OnIncomingCallRespond(object sender, Respond respond)
        {
            var registeredCallInfo = GetConnectionInfo(respond.Source);
            if (registeredCallInfo != null)
            {
                switch (respond.Condition)
                {
                    case RespondCondition.Reset:
                        {
                            InterruptConnection(registeredCallInfo);
                            break;
                        }
                    case RespondCondition.Accept:
                        {
                            ActiveCall(registeredCallInfo);
                            break;
                        }
                }
            }
            else
            {
                CallInfo currentCallInfo = GetCallInfo(respond.Source);
                if (currentCallInfo != null)
                {
                    InterruptConnection(currentCallInfo);
                }
            }
        }

        public abstract void RegisterEventHandlersForBilling(Billing billing);

        public void ClearEvents()
        {
            NewCallInfo = null;
        }
    }
}
