using System;
using System.Threading;
using System.Threading.Tasks;

namespace _14.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Run2();
            Run();

            Console.Read();
        }

        public static void Run()
        {
            RunTaskAsForeground();
            RunTaskAsBakcground();
            var task = Task.Factory.StartNew<string>(() => GetMessage());
            Console.WriteLine(task.Result);
        }
        public static void Run2()
        {
            var task = Task.Factory.StartNew((o) => WhatsMyHome(), CancellationToken.None, TaskCreationOptions.LongRunning);

            task.Wait();
        }

        private static void RunTaskAsForeground()
        {
            var tcs = new TaskCompletionSource<object>();
            new Thread(() =>
            {
                WhatsMyHome();
                tcs.SetResult(new object());
            }).Start();

            Task t = tcs.Task;
            t.Wait();
        }
        private static void RunTaskAsBakcground()
        {
            var tcs = new TaskCompletionSource<object>();
            new Task(() =>
            {
                WhatsMyHome();
                tcs.SetResult(new object());
            }).Start();

            Task t = tcs.Task;
            t.Wait();
        }
        private static string GetMessage()
        {
            return "task return value right there";
        }
        private static void WhatsMyHome()
        {
            Thread current = Thread.CurrentThread;
            bool isThreadPool = current.IsThreadPoolThread;
            bool isBackground = current.IsBackground;

            string threadType = isThreadPool ? "Threadpool" : "Custom";
            string threadKind = isBackground ? "Background" : "Foreground";

            Console.WriteLine($"I am a {threadType} thread in the kind {threadKind}");
        }
    }
}
