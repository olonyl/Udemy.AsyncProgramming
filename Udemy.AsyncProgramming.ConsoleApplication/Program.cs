using System;
using System.Threading;

namespace Udemy.AsyncProgramming.ConsoleApplication
{
    class Program
    {
        [ThreadStatic]
        private static int _local = 42;
        private static int _global = 42;

        private static ThreadLocal<int> _tLocal = new ThreadLocal<int>(() => 42);
        static void Main(string[] args)
        {
            var t = Thread.CurrentThread;

            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(Increment);

                thread.Name = $"Thread-Nr [{i}]";
                thread.Start();
                // Thread.Sleep(50);
            }

            Console.Read();
        }

        private static void Increment()
        {
            _local++;
            _tLocal.Value += 1;
            string threadName = Thread.CurrentThread.Name;
            Console.WriteLine($"{threadName} - Local: [{_local}]");
            Console.WriteLine($"{threadName} - ThradLocal: [{_tLocal.Value}]");
            Console.WriteLine($"{threadName} - Global: [{++_global}]");
        }
    }
}
