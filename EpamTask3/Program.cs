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
            List<IPort> ports = new List<IPort>() { new MTSPort(), new MTSPort() };
            List<ITerminal> terminals = new List<ITerminal>() { };
            MTSStation mts = new MTSStation(ports, terminals);
            Billing billing = new Billing(mts);
            
            var absolute = new Absolute("Absolute", "", 10);
            var ultra = new Ultra("Ultra", "", 15);
            
            var p0 = new PhoneNumber("12-345-67");
            var p2 = new PhoneNumber("76-543-21");
            
            billing.AddTerminal(p0, ultra);
            billing.AddTerminal(p2, absolute);
           
                    foreach (var terminal in terminals)
                    {
                        terminal.Plug();
                    }
                    
                    terminals[0].Call(new PhoneNumber("76-543-21"));

                    terminals[1].Answer();


                    System.Threading.Thread.Sleep(5000);
                    terminals[1].Drop();
                
                    terminals[0].Unplug();

            var billingInfo = billing.GetCallInfoForPeriod(p0, new DateTime(2017, 11, 20, 16, 58, 52), DateTime.Now);

            var costInfo = billing.GetCostInfo(p0,absolute, new DateTime(2017, 11, 20, 16, 58, 52), DateTime.Now);
            Console.ReadKey();
            }
        }
    }

