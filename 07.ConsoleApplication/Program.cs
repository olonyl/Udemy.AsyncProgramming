using System;
using System.Threading;
using System.Threading.Tasks;

namespace _07.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // "normal" thread
            var thread = new Thread(() => RunOnThreadpool("Normal-Thread"));

            // 1 Use ThreadPool Enqueuing
            ThreadPool.QueueUserWorkItem(param => RunOnThreadpool("Queued Item"));

            // 2 Task (chapter 4)
            Task.Run(() => RunOnThreadpool("From the task"));

            // 3 APM is another one, but obsolete
            // usually BeginInvove and EndInvoke methods
            thread.Start();


            Console.Read();


        }

        private static void RunOnThreadpool(string threadName)//not allowed on thread pool
        {
            // hot to find out? --> Thread Pool thread is automatic background thread!
            bool isBackground = Thread.CurrentThread.IsBackground;

            string background = isBackground ? "a background thread" : "a foreground thread";
            Console.WriteLine($"Now running thread is {background} with name {threadName}");
        }
    }
}
