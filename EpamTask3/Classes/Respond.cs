using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Respond:IncomingCalls
    {
        public IncomingCalls Request;

       // public PhoneNumber Source { get; set; }

        public RespondCondition Condition { get; set; }
    }
}
