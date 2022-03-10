using System;
using System.IO;
using System.Net;
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
            Console.WriteLine("test01");
            CallAsyncOperation();
            Console.WriteLine("test02");
            Console.WriteLine();
            Console.WriteLine();


            Console.Read();
        }

        private static async void CallAsyncOperation()
        {
            var result = await AsynApi.GetWebResource("https://google.com");
            Console.WriteLine(result);
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
