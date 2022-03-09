using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _17.Parallels
{
    internal class ForTask
    {
        public static void LinearExecution()
        {
            Console.WriteLine("For Loop");
            for (int i = 0; i < 10; i++)
            {
                DoWait(i);
            }
        }

        public static void ParallelExecutionUsingFor()
        {
            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, 10, index => Decorate(index, DoWait));

        }

        public static void ParallelExecutionUsingForEach()
        {
            Console.WriteLine("Parallel ForEach Loop");
            Parallel.ForEach(Enumerable.Range(0, 10), index => Decorate(index, DoWait));
        }

        public static void ParallelExecutionUsingForEachWithBreak()
        {
            Console.WriteLine("Parallel ForEach Loop with Break");
            Parallel.ForEach(Enumerable.Range(0, 40), (index, loopstate) =>
              {
                  Decorate(index, DoWait);
                  if (index == 5)
                  {
                      loopstate.Stop();
                      Console.WriteLine("Break Condition was signaled");
                  }
              });
        }
        private static void Decorate(int index, Action<int> doWait)
        {
            Console.WriteLine($"next index {index}");
            doWait(index);
        }

        private static void DoWait(int i)
        {
            Thread.Sleep(i * 10);
        }

    }
}
