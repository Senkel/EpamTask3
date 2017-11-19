using EpamTask3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    abstract class Port : IPort
    {
        private PortCondition _condition;

        public PortCondition Condition
        {
            get { return _condition; }

            set
            {
                if (_condition != value)
                {
                    OnStatusChanging(this, value);
                    _condition = value;
                   OnStatusChanged(this, _condition);
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

        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);

    }
}
