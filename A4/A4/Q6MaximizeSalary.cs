using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A4
{
    public class Q6MaximizeSalary : Processor
    {
        public Q6MaximizeSalary(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], string>) Solve);


        public virtual string Solve(long n, long[] numbers)
        {
            
            string result = null;
            var listofnumbers = numbers.ToList();
            while (listofnumbers.Count>0)
            {
                var max = FindBestChoice(listofnumbers);
                result += max.ToString();
                listofnumbers.Remove(max);
                

            }

            return result;
        }

        private long FindBestChoice(List<long> listofnumbers)
        {
            var max = listofnumbers[0];
            for (int i = 1; i < listofnumbers.Count; i++)
                max = Maximize(listofnumbers[i], max);
            return max;
        }

        private long Maximize(long v, long max)
        {
            var first = v.ToString() + max.ToString();
            var second = max.ToString() + v.ToString();
            if (long.Parse(first) > long.Parse(second))
                return v;
            return max;
        }


       
    }
}

