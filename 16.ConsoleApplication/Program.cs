using System;
using System.Threading.Tasks;

namespace _16.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcs = new TaskCompletionSource<int>();

            Task<int> synth = tcs.Task;
            synth.ContinueWith(t => Console.WriteLine($"from synthetic task: {t.Result}"));
            Console.WriteLine("waiting for a result!");
            Console.ReadKey();

            tcs.SetResult(42);
            Console.ReadKey();


        }
    }
}
