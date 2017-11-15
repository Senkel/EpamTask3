using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Terminal : ITerminal
    {
        public Terminal(PhoneNumber number)
        {
            Number = number;
        }

        public PhoneNumber Number { get; private set; }

        public bool IsOnline { get; private set; }

        public event EventHandler Online;
        public event EventHandler Offline;
        public event EventHandler Plugging;
        public event EventHandler UnPlugging;

        public void Answer()
        {
            throw new NotImplementedException();
        }

        public void ClearEvents()
        {
            this.Online = null;
            this.Offline = null;
            this.Plugging = null;   
            this.UnPlugging = null;
        }

        public void Drop()
        {
            throw new NotImplementedException();
        }

        public void OnPlugging(object sender , EventArgs args)
        {
            Plugging?.Invoke(sender, args);
        }

        public void Plug()
        {
            OnPlugging(this, null);
        }

        public void RegisterEventHandlersForPort(IPort port)
        {
            throw new NotImplementedException();
        }

        public void OnUnPlugging(object sender, EventArgs args)
        {
            UnPlugging?.Invoke(sender, args);
        }

        public void Unplug()
        {
            if (IsOnline)
            {
                OnUnPlugging(this, null);
            }
        }
    }
}
