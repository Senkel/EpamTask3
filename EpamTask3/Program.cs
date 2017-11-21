using EpamTask3.Classes;
using EpamTask3.Interfaces;
using EpamTask3.MTS;
using EpamTask3.MTS.Tariffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<IPort> ports = new List<IPort>() { new MTSPort(), new MTSPort(), new MTSPort(), new MTSPort(), new MTSPort() };
            List<ITerminal> terminals = new List<ITerminal>() { };
            MTSStation mts = new MTSStation(ports, terminals);
            Billing billing = new Billing(mts);
            List<Billing> bil = new List<Billing>();

            var absolute = new Absolute("Absolute", "", 10);
            var ultra = new Ultra("Ultra", "", 15);


            var p0 = new PhoneNumber("12-345-67");
            var p2 = new PhoneNumber("76-543-21");
            var p3 = new PhoneNumber("9327996");
           

            

            
            billing.AddTerminal(p0, ultra);
            billing.AddTerminal(p2, absolute);    
            foreach (var terminal in terminals)
            {
                terminal.Plug();
            }

            terminals[0].Call(new PhoneNumber("76-543-21"));

            terminals[1].Answer();

            

            terminals[0].Drop();

            foreach (var b in bil)
            {
                Console.WriteLine(b.GetCallInfoForPeriod(p0, new DateTime(2017, 11, 20, 16, 58, 52), DateTime.Now));
            }

            var billingInfo = billing.GetCallInfoForPeriod(p0, new DateTime(2017, 11, 20, 16, 58, 52), DateTime.Now);

            Console.ReadKey();
        }
    }
}
