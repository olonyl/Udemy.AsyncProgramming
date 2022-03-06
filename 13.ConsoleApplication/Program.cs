using System;
using System.Collections.Concurrent;
using System.Threading;

namespace _13.ConsoleApplication
{
    class Program
    {
        static Lazy<int> _lazy = new Lazy<int>(() =>
        {
            Console.WriteLine("Calling the factory method!");
            Thread.Sleep(200);
            return 42;
        });

        static void Main(string[] args)
        {
            UsingLazyObject();

            RunConcurrentQueue();

            Console.ReadKey();
        }

        static AutoResetEvent _event = new AutoResetEvent(false);
        static ConcurrentQueue<int> _queque = new ConcurrentQueue<int>();

        private static void RunConcurrentQueue()
        {
            var produce = new Thread(
                () =>
                {
                    while (true)
                    {
                        int rnd = new Random().Next(0, 100);

                        _queque.Enqueue(rnd);
                        Console.WriteLine("enqueued: " + rnd);
                        Thread.Sleep(500);
                        _event.Set();
                    }
                });
            produce.Start();
            new Thread(Consume).Start("first");
            new Thread(Consume).Start("second");

        }

        private static void Consume(object name)
        {
            while (true)
            {
                _event.WaitOne();
                UseQueueAtomic(name);
            }
        }

        private static void UseQueueAtomic(object name)
        {
            bool success = _queque.TryDequeue(out int i);
            if (success)
            {
                Console.WriteLine($"{name} has dequeued: {i}");
            }
            else
            {
                Console.WriteLine($"{name} has noting to dequeue: {i}");
            }
        }

        private static void UsingLazyObject()
        {
            var t = new Thread(PrintLazyAndThreadName);
            var t2 = new Thread(PrintLazyAndThreadName);

            t.Name = "1";
            t2.Name = "2";

            t.Start();
            t2.Start();
        }

        private static void PrintLazyAndThreadName()
        {
            var name = Thread.CurrentThread.Name;
            int result = _lazy.Value;
            Console.WriteLine($"Thread: {name} with Result: [{result}] ");
        }
    }
}
