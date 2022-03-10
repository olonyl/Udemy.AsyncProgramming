using System;
using System.Threading;

namespace _18.Deadlock
{
    internal class Semaphore
    {
        public static void Run()
        {
            new Thread(Write) { Name = "thread_1" }.Start(1);
            new Thread(Write) { Name = "thread_2" }.Start(2);
            new Thread(Write) { Name = "thread_3" }.Start(3);
        }
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);
        [ThreadStatic]
        private static int _counter;

        private static void Write(object obj)
        {
            int step = (int)obj;
            string threadName = Thread.CurrentThread.Name;
            _semaphore.Wait();
            for (int i = 0; i < 10; i += step)
            {
                Thread.Sleep(500);
                Console.WriteLine($"{threadName} arriving - index {i} - pool {_counter++}");
            }
            _semaphore.Release();
        }
    }
}
