using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class Respond:Calls
    {
        public Calls Request;

        public PhoneNumber Source { get; set; }

        public RespondCondition Condition { get; set; }
    }
}
