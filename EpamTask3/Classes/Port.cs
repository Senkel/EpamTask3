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
        private PortCondition _condition;

        public PortCondition Condition
        {
            get { return _condition; }

            set
            {
                if (_condition != value)
                {
                    ConditionChanging(this, value);
                    _condition = value;
                    ConditionChanged(this, _condition);
                }
            }
        }

        public event EventHandler<PortCondition> ConditionChanged;

        private void OnStatusChanged(object sender, PortCondition condition)
        {
            ConditionChanged?.Invoke(sender, condition);
        }

        public event EventHandler<PortCondition> ConditionChanging;

        private void OnStatusChanging(object sender, PortCondition newStatus)
        {
            ConditionChanging?.Invoke(sender, newStatus);
        }

        public void ClearEvents()
        {
            ConditionChanged = null;
            ConditionChanging = null;
        }

        public void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            throw new NotImplementedException();
        }
    }
}
