using System;
using TestCommon;

namespace E1b
{
    public class Q4HungryFrog : Processor
    {
        public Q4HungryFrog(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);


        public virtual long Solve(long n, long p, long[][] numbers)
        {
            long[] firstarray = new long[n];
            long[] secondarray = new long[n];
            firstarray[0] = numbers[0][0];
            secondarray[0] = numbers[1][0];
            long first = 0;
            long second = 0;
            for (int i = 1; i < n; i++)
            {
                first = firstarray[i - 1] + numbers[0][i];
                second = secondarray[i - 1] + numbers[0][i] - p;
                if (first >= second)
                    firstarray[i] = first;
                else
                    firstarray[i] = second;
                first = secondarray[i - 1] + numbers[1][i];
                second = firstarray[i - 1] + numbers[1][i] - p;
                if (first >= second)
                    secondarray[i] = first;
                else
                    secondarray[i] = second;

            }

            return (Math.Max(firstarray[n - 1], secondarray[n - 1]));

        }
    }
}
