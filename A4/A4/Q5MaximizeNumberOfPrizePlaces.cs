using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A4
{
    public class Q5MaximizeNumberOfPrizePlaces : Processor
    {
        public Q5MaximizeNumberOfPrizePlaces(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)Solve);


        public virtual long[] Solve(long n)
        {
            var result = new List<long>();
            int i = 1;
            while (n - i >= 0)
            {
                result.Add(i);
                n = n - i;
                i++;
            }
            if (n - i < 0)
            {
                result[result.Count - 1] += n;
            }

            return result.ToArray();
        }
    }
}

