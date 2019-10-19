using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A4
{
    public class Q4CollectingSignatures : Processor
    {
        public Q4CollectingSignatures(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)Solve);


        public virtual long Solve(long tenantCount, long[] startTimes, long[] endTimes)
        {
            long result = 0;
            while (tenantCount > 0)
            {
                var idx = FindMin(endTimes);
                for (int i = 0; i < endTimes.Length; i++)
                {
                    if (endTimes[idx] >= startTimes[i] && endTimes[idx] <= endTimes[i] && idx != i)
                    {
                        tenantCount--;
                        startTimes[i] = long.MaxValue;
                        endTimes[i] = long.MaxValue;
                    }
                    if (idx == i)
                        tenantCount--;
                }
                result++;
                startTimes[idx] = long.MaxValue;
                endTimes[idx] = long.MaxValue;

            }

            return result;


        }



        public static long FindMin(long[] a)
        {
            var min = a[0];
            var idx = 0;
            for (int i = 1; i < a.Length; i++)
                if (a[i] < min && a[i] != 0)
                {
                    min = a[i];
                    idx = i;
                }
            return idx;

        }




    }
}
