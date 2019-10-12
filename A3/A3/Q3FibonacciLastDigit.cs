using System;
using System.Collections.Generic;
using TestCommon;

namespace A3
{
    public class Q3FibonacciLastDigit : Processor
    {
        public Q3FibonacciLastDigit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>)Solve);

        public long Solve(long n)
        {
            var Fib = new List<long>() { 0, 1 };
            int i = 1;
            while (true)
            {
                i++;
                Fib.Add(((Fib[i - 2]) + (Fib[i - 1])) % 10);
                if (Fib[i - 1] == 0 && Fib[i] == 1)
                    break;
            }
            var idx = n % (Fib.Count - 2);
            return Fib[(int)idx];
        }


    }
}
