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
            Run3();

            Console.Read();
        }

        public static void Run()
        {
            RunTaskAsForeground();
            RunTaskAsBakcground();
            var task = Task.Factory.StartNew<string>(() => GetMessage());
            Console.WriteLine(task.Result);
        }

        public static void Run3()
        {
            var string_ = Guid.NewGuid().ToString();
            var task = Task.Factory.StartNew((o) => WhatsMyHome2(string_), CancellationToken.None, TaskCreationOptions.LongRunning);
            task.Wait();
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
        private static void WhatsMyHome2(string message)
        {
            Thread current = Thread.CurrentThread;
            bool isThreadPool = current.IsThreadPoolThread;
            bool isBackground = current.IsBackground;

            string threadType = isThreadPool ? "Threadpool" : "Custom";
            string threadKind = isBackground ? "Background" : "Foreground";

            Console.WriteLine($"{message}");
        }
    }
}
