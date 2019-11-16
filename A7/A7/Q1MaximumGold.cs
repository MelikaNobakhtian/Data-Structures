using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A7
{
    public class Q1MaximumGold : Processor
    {
        public Q1MaximumGold(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long W, long[] goldBars)
        {
            long[,] weights = new long[W + 1, goldBars.Length+1];
            for(int i = 1; i <=goldBars.Length; i++)
            {
                for(int j = 1; j <= W; j++)
                {
                    var value = long.MinValue;
                    weights[j, i] = weights[j, i - 1];
                    if (goldBars[i-1] <= j)
                        value = weights[j - goldBars[i-1], i - 1] + goldBars[i-1];
                    if (value > weights[j, i])
                        weights[j, i] = value;
                }
            }
            return weights[W, goldBars.Length];
        }
    }
}
