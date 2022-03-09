using System;

namespace _18.Deadlock
{
    class Program
    {
        static void Main(string[] args)
        {
            //Deadlock.SelectDeadLock("Test");
            // Deadlock.Improved();
            Deadlock.LockWithTimedMonitor();

            Console.Read();
        }


    }
}
