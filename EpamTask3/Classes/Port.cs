using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Port : IPort
    {
        private PortStatus _status;

        public PortStatus Status
        {
            get { return _status; }

            set
            {
                if (_status != value)
                {
                    StatusChanging(this, value);
                    _status = value;
                    StatusChanged(this, _status);
                }
            }
        }

        public event EventHandler<PortStatus> StatusChanged;

        private void OnStatusChnged(object sender, PortStatus status)
        {
            StatusChanged?.Invoke(sender, status);
        }

        public event EventHandler<PortStatus> StatusChanging;

        private void OnStatusChnging(object sender, PortStatus newStatus)
        {
            StatusChanging?.Invoke(sender, newStatus);
        }

        public void ClearEvents()
        {
            StatusChanged = null;
            StatusChanging = null;
        }

        public void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            throw new NotImplementedException();
        }
    }
}
