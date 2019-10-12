using System;
using TestCommon;
using System.Linq;
using System.Collections.Generic;

namespace A3
{
    public class Q7FibonacciSum : Processor
    {
        public Q7FibonacciSum(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>)Solve);

        public long Solve(long n)
        {
            var Fib = new List<long>() { 0, 1 };
            var sumdigit = new List<long>() { 0, 1 };
            long sum = 1;
            int i = 1;
            while (true)
            {
                i++;
                Fib.Add(((Fib[i - 2]) + (Fib[i - 1])) % 10);
                sum = (sum + Fib[i]) % 10;
                sumdigit.Add(sum);
                if (sumdigit[i - 1] == 0 && sumdigit[i] == 1)
                    break;
            }
            var idx = n % (sumdigit.Count - 2);
            return sumdigit[(int)idx];
        }


    }
}
