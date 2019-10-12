using System;
using TestCommon;

namespace A3
{
    public class Q5LCM : Processor
    {
        public Q5LCM(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long>)Solve);

        public long Solve(long a, long b)
        {
            var gcd = GCD(a, b);
            return (a / gcd) * b;
        }

        public long GCD(long a, long b)
        {
            if (a < b)
                (b, a) = (a, b);
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
    }
}
