using System;
using System.Collections.Generic;
using System.Linq;

namespace _18.Deadlock
{
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
}
