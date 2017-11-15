using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class CallInfo
    {
        public PhoneNumber Source { get; set; }

        public PhoneNumber Target { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
