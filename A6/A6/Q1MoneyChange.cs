using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q1MoneyChange: Processor
    {
        private static readonly int[] COINS = new int[] {1, 3, 4};

        public Q1MoneyChange(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>) Solve);

        public long Solve(long n)
        {
            long[] coins = new long[] { 1, 3, 4 };
            long[] MinNumCoins = new long[n+1];
            MinNumCoins[0] = 0;
            long NumCoins;
            for(int i = 1; i <= n; i++)
            {
                MinNumCoins[i] = long.MaxValue;
                for(int j = 0; j < coins.Length;j++)
                {
                    if (i >= coins[j])
                    {
                        NumCoins = MinNumCoins[i - coins[j]] + 1;
                        if (NumCoins < MinNumCoins[i])
                        {
                            MinNumCoins[i] = NumCoins;
                        }
                    }

                }
            }

            return MinNumCoins[n];

        }
    }
}
