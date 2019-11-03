using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A5
{
    public class Q6ClosestPoints : Processor
    {
        public Q6ClosestPoints(string testDataName) : base(testDataName)
        { }
        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], double>)Solve);

        public virtual double Solve(long n, long[] xPoints, long[] yPoints)
        {
            QuickSort(yPoints, xPoints, 0, xPoints.Length - 1);
            var distance = FindFirstDistance(xPoints, yPoints, 0, xPoints.Length - 1);
            var result = Math.Round(distance, 4);
            return result;
        }

        private void QuickSort(long[] y, long[] x, int s, int e)
        {
            if (e > s)
            {
                var pivot = x[s];
                int j = s;
                for (int i = s + 1; i <= e; i++)
                {
                    if (x[i] <= pivot)
                    {
                        j += 1;
                        (x[i], x[j]) = (x[j], x[i]);
                        (y[i], y[j]) = (y[j], y[i]);
                    }
                }
                (x[s], x[j]) = (x[j], x[s]);
                (y[s], y[j]) = (y[j], y[s]);

                QuickSort(y, x, s, j - 1);
                QuickSort(y, x, j + 1, e);
            }
        }



        private double FindFirstDistance(long[] x, long[] y, int low, int high)
        {
            if (high == low + 1)
                return Math.Sqrt(Math.Pow(x[high] - x[low], 2) + Math.Pow(y[high] - y[low], 2));
            var mid = (high + low) / 2;
            var first = FindFirstDistance(x, y, low, mid);
            var second = FindFirstDistance(x, y, mid, high);
            double distance = Math.Min(first, second);
            List<long> firstdisx = new List<long>();
            List<long> firstdisy = new List<long>();

            for (int i = low; i <= mid; i++)
            {
                if (Math.Abs(x[mid] - x[i]) < distance)
                {
                    firstdisx.Add(x[i]);
                    firstdisy.Add(y[i]);
                }
            }

            for (int i = mid + 1; i < high; i++)
            {
                if (Math.Abs(x[i] - x[mid]) < distance)
                {
                    firstdisx.Add(x[i]);
                    firstdisy.Add(y[i]);
                }
            }

            var xarray = firstdisx.ToArray();
            var yarray = firstdisy.ToArray();
            int firstdisxCount = firstdisx.Count;
            QuickSort(xarray, yarray, 0, firstdisxCount - 1);
            for (int i = 0; i < firstdisxCount; i++)
                for (int j = i + 1; j < i + 8; j++)
                {
                    if (j >= firstdisxCount)
                        break;
                    var nowdis = Math.Sqrt(Math.Pow(xarray[i] - xarray[j], 2) + Math.Pow(yarray[i] - yarray[j], 2));
                    if (distance > nowdis)
                    {
                        distance = nowdis;
                    }
                }
            return distance;

        }
    }
}