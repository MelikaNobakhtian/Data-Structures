using System;
using TestCommon;

namespace E1a
{
    public class Q1Stones : Processor
    {
        public Q1Stones(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);


        public virtual long Solve(long n, long[] stones)
        {
            long sum = 0;
            for(int i = 0; i < stones.Length; i++)
            {
                if (sum < n && n <= sum + stones[i])
                    return i + 1;
                sum += stones[i];
            }
            return 0;
        }
    }
}
