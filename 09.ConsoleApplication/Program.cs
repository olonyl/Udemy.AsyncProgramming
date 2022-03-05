using System;
using System.Threading;

namespace _09.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var flipchart = new FlipChart();

            for (int i = 0; i < 30; i++)
            {
                var thread = new Thread(() => Execute(i, flipchart));
                thread.Start();
                Thread.Sleep(50);
            }
            Console.Read();
        }
        static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        public static void Execute(int i, FlipChart flip)
        {
            if (i == 0 || i % 5 == 0)
            {
                try
                {
                    _rwLock.EnterWriteLock();
                    Console.WriteLine("entering write mode for thread-" + i);
                    flip.Write(i.ToString());
                }
                finally
                {
                    _rwLock.ExitWriteLock();
                }
            }
            else
            {
                try
                {
                    _rwLock.EnterReadLock();
                    Console.WriteLine("entering read mode for thread-" + i);
                    Console.WriteLine(flip.ReaAllText());
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }
            }
        }
    }

    public class FlipChart
    {
        private string Value;

        public void Write(string value)
        {
            Value += "-" + value;
        }

        public string ReaAllText()
        {
            return Value;
        }

    }
}
