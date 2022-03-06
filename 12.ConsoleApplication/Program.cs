using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _12.ConsoleApplication
{
    class Program
    {
        //private static readonly AutoResetEvent _turnstile = new AutoResetEvent(false);
        private static readonly ManualResetEvent _turnstile = new ManualResetEvent(false);
        private static Queue<int> _queue = new Queue<int>();
        private static object _token = new object();

        static void Main(string[] args)
        {
            var producer = new Thread(
                () =>
                {
                    while (true)
                    {
                        var rnd = new Random().Next(0, 42);
                        lock (_token)
                        {
                            _queue.Enqueue(rnd);
                            _turnstile.Set();
                            Console.WriteLine($"Enqueued: {rnd}");
                        }
                        Thread.Sleep(300);
                    }
                });

            producer.Start();
            new Thread(Consume).Start("first");
            new Thread(Consume).Start("snd");

            Console.Read();
        }

        private static void Consume(object threadName)
        {
            while (true)
            {
                _turnstile.WaitOne();
                lock (_token)
                {
                    if (_queue.Any())
                    {
                        int result = _queue.Dequeue();
                        Console.WriteLine($"{threadName} dequeued: {result}");
                    }
                }
            }
        }
    }
}
