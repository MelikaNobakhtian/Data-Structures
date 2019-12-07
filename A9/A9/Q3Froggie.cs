using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q3Froggie : Processor
    {
        public Q3Froggie(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[], long[], long>)Solve);

        public long Solve(long initialDistance, long initialEnergy, long[] distance, long[] food)
        {
            long result = 0;
            Tuple<long, long>[] Distance = new Tuple<long, long>[distance.Length];
            SimplePriorityQueue<long, long> foods = new SimplePriorityQueue<long, long>();
            int n = distance.Length;
            for (int i = 0; i < n; i++)
            {
                Distance[i]=(new Tuple<long, long>(initialDistance-distance[i],food[i]));
            }
            QuickSort(Distance, 0, n - 1);
            long path = 0;
           
            for (int i = 0; i < n; i++)
            {
                path = Distance[i].Item1;
                while (initialEnergy <  path)
                {
                    if (foods.Count == 0)
                        return -1;
                    initialEnergy += foods.Dequeue();
                    result++;
                }

                foods.Enqueue(Distance[i].Item2, Distance[i].Item2* -1);
            }

            while (initialEnergy < initialDistance)
            {
                if (foods.Count == 0)
                    return -1;
                initialEnergy += foods.Dequeue();
                result++;
            }

            return result;
        }

        private void QuickSort( Tuple<long,long>[] a, int s, int e)
        {
            if (e > s)
            {
                var pivot = a[s].Item1;
                int j = s;
                for (int i = s + 1; i <= e; i++)
                {
                    if (a[i].Item1 < pivot)
                    {
                        j += 1;
                        (a[i], a[j]) = (a[j], a[i]);
                    }
                }
                (a[s], a[j]) = (a[j], a[s]);
               

                QuickSort( a, s, j - 1);
                QuickSort( a, j + 1, e);
            }
        }
    }
}
