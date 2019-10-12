using System;
using System.Collections.Generic;
using TestCommon;

namespace A3
{
    public class Q6FibonacciMod : Processor
    {
        public Q6FibonacciMod(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long>)Solve);

        public long Solve(long a, long b)
        {
            var Fib = new List<long>() { 0, 1 };
            int i = 1;
            while (true)
            {
                i++;
                Fib.Add((Fib[i - 2] + Fib[i - 1]) % b);
                if (Fib[i - 1] == 0 && Fib[i] == 1)
                    break;
            }
            var idx = a % (Fib.Count - 2);
            return Fib[(int)idx];
        }
    }
}
