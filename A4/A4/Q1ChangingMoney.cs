using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A4
{
    public class Q1ChangingMoney : Processor
    {
        public Q1ChangingMoney(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>)Solve);


        public virtual long Solve(long money)
        {

            var coin = new[] { 10, 5, 1 };
            long coincount = 0;
            int i = 0;
            while (money != 0)
            {
                coincount += money / coin[i];
                money = money % coin[i];
                i++;
            }
            return coincount;

        }
    }
}
