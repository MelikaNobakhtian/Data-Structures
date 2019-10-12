using System;
using System.Collections.Generic;
using TestCommon;

namespace A3
{
    public class Q8FibonacciPartialSum : Processor
    {
        public Q8FibonacciPartialSum(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long>)Solve);

        public long Solve(long a, long b)
        {
            if (a > b)
                (a, b) = (b, a);
            var last = LastDigit(b);
            var first = LastDigit(a - 1);
            var mod = last - first;
            if (mod >= 0)
                return mod;
            else
                return mod + 10;

        }

        public long LastDigit(long n)
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
