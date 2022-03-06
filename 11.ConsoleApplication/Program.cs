using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _11.ConsoleApplication
{
    class Program
    {
        private readonly static Queue<int> _queue = new Queue<int>();
        private readonly static object _token = new object();
        static void Main(string[] args)
        {
            var producer = new Thread(
                () =>
                {
                    while (true)
                    {
                        lock (_token)
                        {
                            var rnd = new Random().Next(0, 100);
                            if (!_queue.Contains(rnd))
                            {
                                _queue.Enqueue(rnd);
                                Console.WriteLine("enqueued: " + rnd);
                                Monitor.Pulse(_token);
                                Thread.Sleep(1500);
                            }

                        }
                    }
                });
            string first = "first";
            string snd = "second";

            producer.Start();
            new Thread(Consume).Start(first);
            new Thread(Consume).Start(snd);
        }

        private static void Consume(object consumerName)
        {
            lock (_token)
            {
                while (true)
                {
                    Monitor.Wait(_token);
                    if (_queue.Any())
                    {
                        int result = _queue.Dequeue();
                        Console.WriteLine(consumerName + " dequeued: " + result);
                    }
                }
            }
        }
    }
}
