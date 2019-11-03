using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A5
{
    public class Q5OrganizingLottery : Processor
    {
        public Q5OrganizingLottery(string testDataName) : base(testDataName)
        { }
        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);

        public virtual long[] Solve(long[] points, long[] startSegments, long[] endSegment)
        {
            var index = new long[points.Length];
            for (int i = 0; i < points.Length; i++)
                index[i] = i;
            QuickSort(index, points, 0, points.Length - 1);
            var allmembers = new long[2, points.Length + startSegments.Length + endSegment.Length];
            for (int i = 0; i < startSegments.Length; i++)
            {
                allmembers[0, i] = startSegments[i];
                allmembers[1, i] = 0;
            }
            for (int i = startSegments.Length, j = 0; i < points.Length + startSegments.Length; i++, j++)
            {
                allmembers[0, i] = points[j];
                allmembers[1, i] = 1;
            }
            for (int i = points.Length + startSegments.Length, j = 0; i < points.Length + endSegment.Length + startSegments.Length; i++, j++)
            {
                allmembers[0, i] = endSegment[j];
                allmembers[1, i] = 2;
            }
            QuickSort(allmembers, 0, points.Length + endSegment.Length + startSegments.Length - 1);
            long[] participation = new long[points.Length];
            long common = 0;
            int pidx = 0;
            for (int i = 0; i < points.Length + endSegment.Length + startSegments.Length; i++)
            {
                if (allmembers[1, i] == 2)
                    common--;
                if (allmembers[1, i] == 0)
                    common++;
                if (allmembers[1, i] == 1)
                {
                    int idx = (int)index[pidx];
                    participation[idx] = common;

                    pidx++;
                }
            }

            return participation;

        }

        private void QuickSort(long[] idx, long[] a, int s, int e)
        {
            if (e > s)
            {
                var pivot = a[s];
                int j = s;
                for (int i = s + 1; i <= e; i++)
                {
                    if (a[i] < pivot)
                    {
                        j += 1;
                        (a[i], a[j]) = (a[j], a[i]);
                        (idx[i], idx[j]) = (idx[j], idx[i]);
                    }
                }
                (a[s], a[j]) = (a[j], a[s]);
                (idx[s], idx[j]) = (idx[j], idx[s]);

                QuickSort(idx, a, s, j - 1);
                QuickSort(idx, a, j + 1, e);
            }
        }

        private void QuickSort(long[,] a, int s, int e)
        {
            if (e > s)
            {
                var pivot = a[0, s];
                int j = s;
                for (int i = s + 1; i <= e; i++)
                {
                    if (a[0, i] < pivot ||( a[0,i]==pivot && a[1,i]<a[1,s]))
                    {
                        j += 1;
                        (a[0, i], a[0, j]) = (a[0, j], a[0, i]);
                        (a[1, i], a[1, j]) = (a[1, j], a[1, i]);
                    }

                    
                }
                (a[0, s], a[0, j]) = (a[0, j], a[0, s]);
                (a[1, s], a[1, j]) = (a[1, j], a[1, s]);

                QuickSort(a, s, j - 1);
                QuickSort(a, j + 1, e);
            }
        }
    }
}
