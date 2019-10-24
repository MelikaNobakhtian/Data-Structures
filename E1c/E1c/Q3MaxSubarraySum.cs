using System;
using TestCommon;

namespace E1b
{
    public class Q3MaxSubarraySum : Processor
    {
        public Q3MaxSubarraySum(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);


        public virtual long Solve(long n, long[] numbers)
        {
            long sum_uptonow = 0;
            long maxsum = long.MinValue;
            for (int i = 0; i < n; i++)
            {
                sum_uptonow += numbers[i];
                if (sum_uptonow > maxsum)
                    maxsum = sum_uptonow;
                if (sum_uptonow < 0)
                    sum_uptonow = 0;
            }

            return maxsum;

        }
    }
}
