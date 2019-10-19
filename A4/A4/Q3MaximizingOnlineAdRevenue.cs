using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A4
{
    public class Q3MaximizingOnlineAdRevenue : Processor
    {
        public Q3MaximizingOnlineAdRevenue(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)Solve);


        public virtual long Solve(long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
            long allvalue = 0;
            while (slotCount > 0)
            {
                var first = FindMaxIdx(adRevenue);
                var second = FindMaxIdx(averageDailyClick);
                allvalue += adRevenue[first] * averageDailyClick[second];
                adRevenue[first] = long.MinValue;
                averageDailyClick[second] = long.MinValue;
                slotCount--;
            }

            return allvalue;

        }

        public static int FindMaxIdx(long[] a)
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
    }
}
