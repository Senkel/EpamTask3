﻿using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    abstract class Terminal : ITerminal
    {
        public Terminal(PhoneNumber number)
        {
            Number = number;
        }

        public PhoneNumber Number { get; private set; }

        public bool IsOnline { get; private set; }

        public Calls IncomingCall { get; set; }

        public event EventHandler Online;
        public event EventHandler Offline;
        public event EventHandler Plugging;
        public event EventHandler UnPlugging;
        public event EventHandler<Calls> OutgoingConnection;
        public event EventHandler<IncomingCalls> IncomingRequest;
        public event EventHandler<Respond> IncomingRespond;

        public void Answer()
        {
            if (!IsOnline && IncomingCall != null)
            {
                OnIncomingRespond(this, new Respond() { Source = Number, Condition = RespondCondition.Accept, Request = IncomingCall });
                OnOnline(this, null);
            }
        }

        public void Drop()
        {
            if (IncomingCall != null)
            {
                OnIncomingRespond(this, new Respond() { Source = Number, Condition = RespondCondition.Reset, Request = IncomingCall });

                if (IsOnline)
                {
                    OnOffline(this, null);
                }
            }
        }

        private void OnOnline(object sender, EventArgs args)
        {
            Online?.Invoke(sender, args);
            IsOnline = true;
        }

        protected virtual void OnIncomingRespond(object sender, Respond respond)
        {
            if (IncomingRespond != null && IncomingCall != null)
                IncomingRespond(sender, respond);

        }

        public void ClearEvents()
        {
            this.Online = null;
            this.Offline = null;
            this.Plugging = null;
            this.UnPlugging = null;
            this.IncomingRequest = null;
            this.IncomingRespond = null;
            this.OutgoingConnection = null;
        }

        public void OnPlugging(object sender, EventArgs args)
        {
            Plugging?.Invoke(sender, args);
        }

        public void Plug()
        {
            OnPlugging(this, null);
        }

        public void OnUnPlugging(object sender, EventArgs args)
        {
            UnPlugging?.Invoke(sender, args);
        }

        public void Unplug()
        {
            OnUnPlugging(this, null);
        }

        public void Call(PhoneNumber target)
        {
            if (!IsOnline)
            {
                OnOutgoingCall(this, target);
                OnOnline(this, null);
            }
        }

        protected void OnOutgoingCall(object sender, PhoneNumber target)
        {
            if (OutgoingConnection != null)
            {
                IncomingCall = new OutGoingCalls() { Source = Number, Target = target };
                OutgoingConnection(sender, IncomingCall);
            }
        }

        public void RegisterEventHandlersForPort(IPort port)
        {
            port.ConditionChanged += (sender, condition) =>
            {
                if (IsOnline && condition == PortCondition.Free)
                {
                    OnOffline(sender, null);
                }
            };
        }

        protected void OnOffline(object sender, EventArgs args)
        {
            if (Offline != null)
            {
                Offline(sender, args);
                IncomingCall = null;
            }
            IsOnline = false;
        }

        protected void OnIncomingRequest(object sender, IncomingCalls request)
        {
            IncomingRequest?.Invoke(sender, request);
            IncomingCall = request;
        }

        public void IncomingCallFrom(PhoneNumber source)
        {
            OnIncomingRequest(this, new IncomingCalls() { Source = source });
        }
    }
}
