using System;
using System.Threading;

namespace _10.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            NonExclusive.RunClub();
            Console.Read();
        }
    }

    internal class NonExclusive
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);
        public static void RunClub()
        {
            for (int i = 0; i <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }
        }

        private static void Enter(object id)
        {
            Console.WriteLine($"{id} wants to enter");

            _semaphore.Wait();
            Console.WriteLine($"{id} walked in");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine($"{id} is leaving already");
            _semaphore.Release();
        }
    }
}
