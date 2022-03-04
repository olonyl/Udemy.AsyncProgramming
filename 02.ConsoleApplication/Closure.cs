using System;

namespace _02.ConsoleApplication
{
    public class Closure
    {
        private readonly int _threeHundred;
        private readonly Action _other;

        public Closure(int threeHundred, Action other)
        {
            _threeHundred = threeHundred;
            _other = other;
        }

        public void Run()
        {
            _other();
            Console.WriteLine(_threeHundred);
        }
    }
}
