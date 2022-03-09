using System;
using System.Diagnostics;

namespace _17.Parallels
{
    internal class ProgramBase
    {
        public static void Execute(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            action();

            stopwatch.Stop();
            Console.WriteLine($"elapsed time {stopwatch.ElapsedMilliseconds:#,0}ms");

        }
    }
}