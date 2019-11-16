using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A7
{
    public class Q2PartitioningSouvenirs : Processor
    {
        public Q2PartitioningSouvenirs(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long souvenirsCount, long[] souvenirs)
        {
            long sum = 0;
            for (int i = 0; i < souvenirs.Length; i++)
            {
                sum += souvenirs[i];
            }
            if (sum % 3 != 0 || souvenirsCount<3)
                return 0;
            else
                sum /= 3;

            Dictionary<string, long> allsubsets = new Dictionary<string, long>();
            return IsthereSubset(souvenirs, souvenirsCount - 1, sum, sum, sum, allsubsets);
            //long[,] sumofsubsets = new long[souvenirs.Length, souvenirs.Length];
            //for (int i = 0; i < souvenirs.Length; i++)
            //    sumofsubsets[i, i] = souvenirs[i];
            //for(int i = 0; i < souvenirs.Length; i++)
            //{
            //    for (int j = i + 1; j < souvenirs.Length; j++)
            //        sumofsubsets[i, j] = sumofsubsets[i, j - 1] + souvenirs[j];
            //}

            ////for(int i = 1; i < souvenirs.Length; i++)
            ////{
            ////    for(int j=0;j<i;j++)
            ////}

            //for(int i = 1; i < souvenirs.Length-1; i++)
            //{
            //    for(int j = i; j < souvenirs.Length - 1; j++)
            //    {
            //        if (sumofsubsets[0, i-1] == sumofsubsets[i, j] && sumofsubsets[i, j] == sumofsubsets[j+1, souvenirs.Length - 1] && sum==sumofsubsets[0,i-1])
            //            return 1;
            //    }
            //}

            //return 0;
        }

        private long IsthereSubset(long[] souvenirs, long lastidx, long sum1, long sum2, long sum3, Dictionary<string, long> allsubsets)
        {
            if (sum1 == 0 && sum2 == 0 && sum3 == 0)
                return 1;
            if (lastidx < 0)
                return 0;
            
            string subset = sum1.ToString() + "," + sum2.ToString() + "," + sum3.ToString() + "," + lastidx.ToString();
            if (!allsubsets.ContainsKey(subset))
            {
                long firstsubset = 0;
                if (sum1 - souvenirs[lastidx] >= 0)
                    firstsubset = IsthereSubset(souvenirs, lastidx - 1, sum1 - souvenirs[lastidx], sum2, sum3, allsubsets);

                long secondsubset = 0;
                if (firstsubset==0 && sum2 - souvenirs[lastidx] >= 0)
                    secondsubset = IsthereSubset(souvenirs, lastidx - 1, sum1, sum2 - souvenirs[lastidx], sum3, allsubsets);

                long thirdsubset = 0;
                if (firstsubset==0 && secondsubset==0 && sum3 - souvenirs[lastidx] >= 0)
                    thirdsubset = IsthereSubset(souvenirs, lastidx - 1, sum1, sum2, sum3 - souvenirs[lastidx], allsubsets);

                allsubsets.Add(subset,(firstsubset==1 || secondsubset==1 || thirdsubset==1)? 1:0);
            }

            return allsubsets[subset];
        }
    }
}
