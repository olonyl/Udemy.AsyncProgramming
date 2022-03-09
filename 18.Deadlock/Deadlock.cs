using System;
using System.Threading;
using System.Threading.Tasks;

namespace _18.Deadlock
{
    internal class Deadlock
    {
        private static readonly object lock_A = new object();
        private static readonly object lock_B = new object();
        public static void LockWithTimedMonitor()
        {
            new Thread(() =>
            {
                try
                {
                    using (lock_A.Lock(200))
                    {
                        Console.WriteLine("Thread 1, acquired Lock A");
                        using (lock_B.Lock(200))
                        {
                            Console.WriteLine("Thread 1, acquired Lock B");
                        }
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Catch exception on thread 1");
                }
            }).Start();

            new Thread(() =>
            {
                try
                {
                    using (lock_B.Lock(300))
                    {
                        Console.WriteLine("Thread 2, acquired Lock B");
                        using (lock_A.Lock(500))
                        {
                            Console.WriteLine("Thread 2, acquired Lock A");
                        }
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Catch exception on thread 2");
                }
            }).Start();
        }
        public static Task SelectDeadLock(string x)
        {
            new Thread(AcquireOne).Start();
            new Thread(AcquireTwo).Start();

            return Task.CompletedTask;
        }

        public static void Improved()
        {
            var thread1 = new Thread(AcquireOne) { IsBackground = true };
            var thread2 = new Thread(AcquireTwo) { IsBackground = true };

            thread1.Start();
            thread2.Start();

            string timeout1 = thread1.Join(400) ? "joined" : "time out";
            string timeout2 = thread2.Join(400) ? "joined" : "time out";

            Console.WriteLine($"tried to join thread --> 1:{timeout1} 2:{timeout2}");

        }

        private static void AcquireTwo()
        {
            lock (lock_A)
            {
                Console.WriteLine("Acquire A");
                Thread.Sleep(200);
                lock (lock_B)
                {
                    Console.WriteLine(" B lock Never reached from A");
                }
            }
        }

        private static void AcquireOne()
        {
            lock (lock_B)
            {
                Console.WriteLine("Acquire B");
                Thread.Sleep(200);
                lock (lock_A)
                {
                    Console.WriteLine("A lock never reached from B");
                }
            }
        }
    }
}
