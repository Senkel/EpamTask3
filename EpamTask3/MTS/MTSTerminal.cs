using EpamTask3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.MTS
{
    class MTSTerminal : Terminal
    {
        public MTSTerminal(PhoneNumber number) : base(number)
        {
            Plugging += (sender, args) => { Console.WriteLine("Terminal {0} Plugging", Number); };

            UnPlugging += (sender, args) => { Console.WriteLine("Terminal {0} UnPlugging", Number); };

            OutgoingConnection += (sender, args) => { Console.WriteLine("Terminal {0} OutgoingConnection", Number); };

            IncomingRequest += OnIncomingRequests;
            IncomingRespond += (sender, args) => { Console.WriteLine("Terminal {0} IncomingRespond", Number); };

            Online += (sender, args) => { Console.WriteLine("Terminal {0} turned to online mode", Number); };

            Offline += (sender, args) => { Console.WriteLine("Terminal {0} turned to offline mode", Number); };
        }

        public void OnIncomingRequests(object sender, IncomingCalls request)
        {
            Console.WriteLine("{0} received request for incoming connection from {1}", Number, request.Source);
        }
    }
}
