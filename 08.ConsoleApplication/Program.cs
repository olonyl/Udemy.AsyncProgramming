using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _08.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var waiter1 = new Thread(() => DeliverCake(1, distabceToBuffet: 50));
            var waiter2 = new Thread(() => DeliverCake(2, distabceToBuffet: 25));

            waiter1.Start();
            waiter2.Start();

            Console.Read();
        }
        private static List<Cake> _cakes = new List<Cake> {
            new Cake{Name="Apfelkuchen"},
            new Cake{Name="Torte"},
            new Cake{Name="Vanille Eis"}
        };
        private static object _token = new object();
        private static void DeliverCake(int v, int distabceToBuffet)
        {
            Monitor.Enter(_token);
            try
            {
                if (_cakes.Any(x => x.Name == "Apfelkuchen"))
                {
                    Console.WriteLine($"waiter-{v} Cake found");
                    Thread.Sleep(distabceToBuffet);

                    Cake taken = _cakes.FirstOrDefault(x => x.Name == "Apfelkuchen");
                    _cakes.Remove(taken);

                    string result = taken != null ?
                        $"{taken.Name } is of the menu"
                        : "Cake was taken before";
                    Console.WriteLine($"waiter-{v} - {result}");
                }
                else
                {
                    Console.WriteLine($"waiter-{v} - Cake not found");
                }
            }
            finally
            {
                Monitor.Exit(_token);
            }
        }
    }
    class Cake
    {
        public string Name { get; set; }
    }
}
