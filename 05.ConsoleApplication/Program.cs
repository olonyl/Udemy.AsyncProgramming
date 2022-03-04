using System;
using System.Threading;

namespace _05.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            WithResultAndTimeOut();


            Console.Read();
        }
        static void JoinUnsafe()
        {
            var sndThread = new Thread(
                () =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("snd thread started");
                    Thread.Sleep(2000);
                    Console.WriteLine("snd thread stopped");
                }
                );
            sndThread.Start();

            Console.WriteLine("block main thread at: " + DateTime.Now.ToString("ss : fff"));
            sndThread.Join();
            Console.WriteLine("unblock main thread at: " + DateTime.Now.ToString("ss : fff"));

        }
        static void WithResultAndTimeOut()
        {
            var result = new Result { Value = "before" };
            var thread = new Thread(() =>
            {
                Thread.Sleep(500);
                result.Value = "after";
            });

            thread.Start();

            bool wasJoined = thread.Join(400);

            if (wasJoined)
            {
                Console.WriteLine("Operation completed: " + result.Value);
            }
            else
            {
                Console.WriteLine("Operation timed out! -  " + result.Value);
            }

        }
        class Result
        {
            public string Value { get; set; }
        }
    }
}
