using System;
using System.Threading;
using System.Threading.Tasks;

namespace _17.Parallels
{
    internal class SimpleTask
    {
        public static void LinearExecution()
        {
            var result = Sum1() + Sum2() + Sum3();
            Console.WriteLine($"result: {result}");
        }

        public static void ParallelExecution()
        {
            int sum1 = 0, sum2 = 0, sum3 = 0;
            Parallel.Invoke(
                () => sum1 = Sum1(),
                () => sum2 = Sum2(),
                () => sum3 = Sum3()
                );
            var result = sum1 + sum2 + sum3;
            Console.WriteLine($"result: {result}");

        }
        private static int Sum1()
        {
            Thread.Sleep(1000);
            return 20;
        }

        private static int Sum2()
        {
            Thread.Sleep(1000);
            return 30;
        }

        private static int Sum3()
        {
            Thread.Sleep(1000);
            return 40;
        }

    }

}
