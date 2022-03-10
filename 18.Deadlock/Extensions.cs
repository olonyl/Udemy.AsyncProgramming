using System;
using System.Collections.Generic;
using System.Linq;

namespace _18.Deadlock
{
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
