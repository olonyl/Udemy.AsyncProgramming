using System;
using System.Collections.Generic;
using System.Linq;

namespace _18.Deadlock
{
    class Program
    {
        static void Main(string[] args)
        {
            //Deadlock.SelectDeadLock("Test");
            // Deadlock.Improved();
            //Deadlock.LockWithTimedMonitor();

            //Barrirer.Run();
            //Semaphore.Run();

            DelegateProcessor.Run();

            Console.Read();
        }
    }

    internal class DelegateProcessor
    {
        public static void Run()
        {
            Delegate();
        }
        private static void Exit()
        {

        }
        private static void Delegate()
        {
            int[] items = Enumerable.Range(0, 7).ToArray();

            IEnumerable<int> result = items.Where(IsEven);


            Func<int, bool> even = IsEven;
            even = i => i % 2 == 0;

            string value = result.Flatten();
            Console.WriteLine(value);
        }

        private static bool IsEven(int i)
        {
            return i % 2 == 0;
        }
    }

    public static class Extensions
    {
        public static void Print(this object obj) => Console.WriteLine(obj);

        public static void PrettyPrint(this string[] array)
        {
            foreach (var item in array)
            {
                Console.WriteLine("\t" + item);
            }
        }

        public static string Flatten<T>(this IEnumerable<T> items)
        {
            return string.Join(",", items.Select(x => x.ToString()));
        }
    }
}
