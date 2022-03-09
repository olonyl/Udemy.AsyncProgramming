using System;

namespace _17.Parallels
{
    class Program : ProgramBase
    {
        static void Main(string[] args)
        {

            Execute(SimpleTask.LinearExecution);
            Execute(SimpleTask.ParallelExecution);
            Execute(ForTask.LinearExecution);
            Execute(ForTask.ParallelExecutionUsingFor);
            Execute(ForTask.ParallelExecutionUsingForEach);
            Execute(ForTask.ParallelExecutionUsingForEachWithBreak);

            Console.Read();
        }
    }
}
