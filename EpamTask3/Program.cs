using EpamTask3.Interfaces;
using EpamTask3.MTS;
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
            List<IPort> ports = new List<IPort>() { new MTSPort(), new MTSPort() };
            List<ITerminal> terminals = new List<ITerminal>();
            MTSStation mts = new MTSStation(ports, terminals);

            mts.Add(new MTSTerminal(new Classes.PhoneNumber("51-20-267")));
            mts.Add(new MTSTerminal(new Classes.PhoneNumber("51-20-767")));

            foreach (var terminal in terminals)
            {
                terminal.Plug();
            }
            terminals[0].Call(new Classes.PhoneNumber("51-20-767"));
            terminals[1].Answer();
            terminals[1].Drop();
            Console.ReadKey();
        }
    }
}
