using System;
using System.Threading;

namespace _06.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            var thread = new Thread(
                () =>
                {
                    try
                    {
                        throw new Exception(
                       "thread that crashed: " + Thread.CurrentThread.Name
                       );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                });
            thread.Name = "Named Thread";

            thread.Start();

            var background = new Thread(() => throw new InvalidOperationException("nothing shown"))
            {
                Name = "silently fails"
            };

            background.Start();

            Console.Read();
        }
    }
}
