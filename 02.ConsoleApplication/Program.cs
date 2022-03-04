using System;
using System.Threading;

namespace _02.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart @start = StartThread;
            var thread = new Thread(@start);
            thread.Name = "Thread from ThreadStart Delegate";

            ParameterizedThreadStart param = _ParameterizedThreadStart;
            var thread2 = new Thread(param);
            thread2.Name = "Thread from ParameterizedStart Delegate";

            int threehundred = 300;
            string noClosure = "not in the closure space";

            var thread3 = new Thread(
                () =>
                {
                    Print();
                    Console.WriteLine(threehundred);
                }
                );
            thread3.Name = "Thread from Lambda with Closure";

            var closure = new Closure(threehundred, Print);
            var thread4 = new Thread(closure.Run);
            thread4.Name = "Thread from Lambda with Closure";

            thread4.Start();
            //thread.Start();
            //thread2.Start(42);
            //thread3.Start();

            Console.Read();
        }

        static void Print()
        {
            string threadName = Thread.CurrentThread.Name;
            Console.WriteLine(threadName);
        }

        static void StartThread()
        {
            Console.WriteLine("Started a thread");
            Print();
        }

        static void _ParameterizedThreadStart(object obj)
        {
            Console.WriteLine($"From parameter {obj}");
            Print();
        }
    }
}
