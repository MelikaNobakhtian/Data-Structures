using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A5
{
    public class Q3ImprovingQuickSort : Processor
    {
        public Q3ImprovingQuickSort(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)Solve);

        public virtual long[] Solve(long n, long[] a)
        {
            QuickSort(a, 0, a.Length - 1);
            return a;
        }

        private static int[] Partition(long[] a, int low, int high)
        {
            long x = a[low];
            int j = low;
            int k = j;
            for (int i = j + 1; i <= high; i++)
            {
                if (a[i] < x)
                {
                    j++;
                    k++;
                    (a[i], a[j]) = (a[j], a[i]);
                    if (j != k)
                    {
                        (a[i], a[k]) = (a[k], a[i]);
                    }
                }

                else if (a[i] == x)
                {
                    k++;
                    (a[i], a[k]) = (a[k], a[i]);
                }
            }
            (a[low], a[j]) = (a[j], a[low]);
            return new int[] { j, k };
        }

        public static void QuickSort(long[] a, int low, int high)
        {
            if (low >= high)
                return;
            var points = Partition(a, low, high);

            QuickSort(a, low, points[0] - 1);
            QuickSort(a, points[1] + 1, high);

        }

    }
}
