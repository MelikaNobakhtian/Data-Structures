using System;
using System.Collections.Generic;
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
            long sum = 0;
            long result = long.MinValue;
            long[] allsum = new long[n];
            for(int k = 0; k < n; k++)
            {
                allsum[k] = SumofArray(numbers, 0, k);
            }
            for(int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (i == 0)
                    {
                        if (result < allsum[j])
                            result = allsum[j];
                       
                    }
                    else
                    {
                        sum = allsum[j] - allsum[i - 1];
                        if (sum > result)
                            result = sum;
                    }
                   

                }

            }
               
            return result;
          
        }

        public static long SumofArray(long[]n,int low,int high)
        {
            long sum = 0;
            for (int i = low; i <= high; i++)
                sum += n[i];
            return sum;
        }
        
    }
}
