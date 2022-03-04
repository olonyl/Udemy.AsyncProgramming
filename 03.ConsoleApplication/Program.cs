using System;
using System.Threading;

namespace _03.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            NonHarmful_StopThreadOnDeletageReturn();

            Thread.Sleep(2000);

            Console.WriteLine();

            NonHarmful_StopByRequest_OR_Cooperation();

            Console.Read();
        }

        private static void NonHarmful_StopThreadOnDeletageReturn()
        {
            var thread = new Thread(() => Console.WriteLine("Stopped after this"));
            thread.Start();
            string state = thread.ThreadState == ThreadState.Stopped ? "was stopped" : "still running";
            Console.WriteLine(state);

        }
        static bool _shouldStop = false;

        private static void NonHarmful_StopByRequest_OR_Cooperation()
        {
            var thread = new Thread(StopPerRquest);
            thread.Start();
            Thread.Sleep(500);
            _shouldStop = true;
            Console.WriteLine("Finished business thread");
        }
        private static void StopPerRquest()
        {
            int i = 0;

            BusinessLogic(++i);

            if (_shouldStop)
                return;
            Console.WriteLine("Other business Logic");

            while (!_shouldStop)
            {
                i++;
                BusinessLogic(i);
            }
        }

        private static void BusinessLogic(int i)
        {
            Thread.Sleep(200);
            Console.WriteLine("Some business operation  - number: " + i);
        }
    }
}
