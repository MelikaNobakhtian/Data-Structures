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

            int i = 0;
            int j = 0;
            long result = 0;
            if (numbers[0][0] > numbers[1][0])
            {
                result = numbers[0][0];
                i = 0;
                j++;
            }
            else
            {
                result = numbers[1][0];
                i = 1;
                j++;
            }

            var firstchoice = whichway(n, p, numbers, i, j + 1) + result + numbers[i][j];
            var secondchoice = whichway(n, p, numbers, 1 - i, j + 1) + numbers[1 - i][j] - p + result;
            if (firstchoice > secondchoice)
            {
                return firstchoice;
            }
            else
            {
                return secondchoice;
            }

        }

        public static long whichway(long n, long p, long[][] numbers, int k, int h)
        {
            int i = k;
            int j = h;
            long result = 0;
            if (j == n - 1)
                if (numbers[i][j] > numbers[1 - i][j])
                    return numbers[i][j];
                else
                    return numbers[1 - i][j] - p;


            var firstchoice = whichway(n, p, numbers, i, j + 1) + result + numbers[i][j];
            var secondchoice = whichway(n, p, numbers, 1 - i, j + 1) + numbers[1 - i][j] - p + result;
            if (firstchoice > secondchoice)
            {
                return firstchoice;
            }
            else
            {
                return secondchoice;
            }


        }
    }
}
