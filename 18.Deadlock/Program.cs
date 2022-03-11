using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

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

            //DelegateProcessor.Run();

            //Console.WriteLine(SyncApi.GetWebResource("https://google.com"));
            //Console.WriteLine("test01");
            //CallAsyncOperation();
            //Console.WriteLine("test02");
            //Console.WriteLine();
            //Console.WriteLine();

            using (var watcher = new Watcher())
            {
                try
                {

                    Thread.Sleep(2000);
                    throw new Exception();

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.Read();

        }

        private static async void CallAsyncOperation()
        {
            var result = await AsynApi.GetWebResource("https://google.com");
            Console.WriteLine(result);
        }
    }

    internal class Watcher : IDisposable
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly ILogger _logger = new Logger();

        public Watcher()
        {
            _stopwatch.Start();
            _logger.Log($"Process Started");
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.Log($"Elapsed Time {_stopwatch.ElapsedMilliseconds}ms");

            bool ExceptionOccurred = Marshal.GetExceptionPointers() != IntPtr.Zero
                            || Marshal.GetExceptionCode() != 0;
            if (ExceptionOccurred)
            {
                try
                {

                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine("We had an exception");
                }
            }
        }
    }

    internal interface ILogger
    {
        void Log(string message);
    }

    internal class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal class SyncApi
    {
        public static string GetWebResource(string url)
        {
            try
            {
                var client = WebRequest.Create(url);
                var response = client.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var strResponse = reader.ReadToEnd();
                    return strResponse;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return null;
            }
        }
    }

    internal class AsynApi
    {
        public static async Task<string> GetWebResource(string url)
        {
            var task = await Task.Run(() => SyncApi.GetWebResource(url));
            return task;
        }
    }
}
