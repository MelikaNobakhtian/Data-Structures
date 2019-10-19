using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A4
{
    public class Q2MaximizingLoot : Processor
    {
        public Q2MaximizingLoot(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)Solve);


        public virtual long Solve(long capacity, long[] weights, long[] values)
        {
            long value = 0;
            int length = weights.Length;
            var valueperweight = new double[length];
            for (int i = 0; i < length; i++)
            {
                valueperweight[i] = (double)values[i] / (double)weights[i];
            }
            for (int i = 0; capacity > 0; i++)
            {
                var best = FindMaxIdx(valueperweight);
                if (weights[best] <= capacity)
                {
                    capacity -= weights[best];
                    value += values[best];
                    valueperweight[best] = 0;
                }
                else
                {
                    var cap = capacity * valueperweight[best];
                    value += (long)cap;
                    capacity = 0;
                }

            }

            return value;
        }

        public static int FindMaxIdx(double[] a)
        {
            var max = a[0];
            int idx = 0;
            for (int i = 1; i < a.Length; i++)
                if (a[i] > max)
                {
                    max = a[i];
                    idx = i;
                }
            return idx;

        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.ApproximateLongVerifier;
    }
}
