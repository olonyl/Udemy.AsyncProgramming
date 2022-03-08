using System;
using System.Threading;
using System.Threading.Tasks;

namespace _15.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcs = new CancellationTokenSource();
            CancellationToken token = tcs.Token;

            Task task = RunAsync(token);
            Thread.Sleep(2000);

            try
            {
                tcs.Cancel();
            }
            catch (Exception)
            {
                Console.WriteLine($"state of task is {task.Status}");
            }
            Console.Read();
        }

        private static Task RunAsync(CancellationToken token)
        {
            return Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                    Console.WriteLine("not cancelled yet");
                }
                Console.WriteLine("was cancelled");
                token.ThrowIfCancellationRequested();

                //or
                throw new OperationCanceledException("cancelled", token);
            });
        }
    }
}
