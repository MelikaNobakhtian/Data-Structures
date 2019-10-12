using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A3
{
    public class Q1MergeSort : Processor
    {
        public Q1MergeSort(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)Solve);

        public long[] Solve(long n, long[] a)
        {
            int high = (int)n - 1;
            return MergeSort(a, 0, high);
        }

        private long[] MergeSort(long[] a, int low, int high)
        {
            if (high - low + 1 == 1)
                return new long[] { a[low] };
            int mid = (low + high) / 2;
            var b = MergeSort(a, low, mid);
            var c = MergeSort(a, mid + 1, high);
            var result = Merge(b, c);
            return result;
        }

        private long[] Merge(long[] b, long[] c)
        {
            long[] d = new long[b.Length + c.Length];
            int i = 0, j = 0, k = 0;
            while (j < b.Length && k < c.Length)
            {
                var b1 = b[j];
                var c1 = c[k];
                if (b1 <= c1)
                {

                    d[i] = b1;
                    j++;
                    i++;
                }
                else
                {
                    d[i] = c1;
                    k++;
                    i++;
                }
            }

            int n = d.Length;
            if (j < b.Length)
            {
                for (int h = j; h < b.Length; h++, i++)
                    d[i] = b[h];
            }
            else if (k < c.Length)
            {
                for (int h = k; h < c.Length; h++, i++)
                    d[i] = c[h];
            }


            return d;

        }


    }
}
