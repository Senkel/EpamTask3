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
            Billing billing = new Billing(); 
            List<IPort> ports = new List<IPort>() { new MTSPort(), new MTSPort() };
            List<ITerminal> terminals = new List<ITerminal>();
            MTSStation mts = new MTSStation(ports, terminals);

            var phN1 = new PhoneNumber("1234567");
            var phN2 = new PhoneNumber("7654321");

           

            var absolute= new Absolute ("Absolute", "", 10);
            var ultra = new Ultra("Ultra", "", 15);


            billing.AddTerminal(phN1, ultra);
            billing.AddTerminal(phN2, absolute);    
            foreach (var terminal in terminals)
            {
                terminal.Plug();
            }

            terminals[0].Call(new PhoneNumber("1234567"));
            terminals[1].Answer();
            terminals[0].Drop();
            
            Console.ReadKey();
        }
    }
}
