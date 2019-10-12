using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2FastMaxPairWise : Processor
    {
        public Q2FastMaxPairWise(string testDataName) : base(testDataName) { }
        public override string Process(string inStr) =>
            Solve(inStr.Split(new char[] { '\n', '\r', ' ' },
                StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => long.Parse(s))
                    .ToArray()).ToString();

        public virtual long Solve(long[] numbers)
        {
            //write your code here
            long firstmax = 0;
            for (int i = 1; i < numbers.Length; i++)
                if (numbers[i] > numbers[firstmax])
                    firstmax = i;
            long secondmax = 0;
            if (firstmax == 0)
                secondmax++;
            for (int i = 1; i < numbers.Length; i++)
                if ((firstmax != i) && (numbers[secondmax] <= numbers[i]))
                    secondmax = i;

            return numbers[firstmax] * numbers[secondmax];
        }
    }
}
