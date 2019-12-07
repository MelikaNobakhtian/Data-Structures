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
            SimplePriorityQueue<int, long> Distance = new SimplePriorityQueue<int, long>();
            SimplePriorityQueue<int, long> foods = new SimplePriorityQueue<int, long>();
            int n = distance.Length;
            for (int i = 0; i < n; i++)
            {
                Distance.Enqueue(i, initialDistance - distance[i]);
            }
            long path = 0;
            int friendhouse = 0;
            for (int i = 0; i < n; i++)
            {
                friendhouse = Distance.Dequeue();
                path = distance[friendhouse];
                while (initialEnergy < initialDistance - path)
                {
                    if (foods.Count == 0)
                        return -1;
                    var idx = foods.Dequeue();
                    initialEnergy += food[idx];
                    result++;
                }

                foods.Enqueue(friendhouse, food[friendhouse] * -1);
            }

            while (initialEnergy < initialDistance)
            {
                if (foods.Count == 0)
                    return -1;
                var idx = foods.Dequeue();
                initialEnergy += food[idx];
                result++;
            }

            return result;
        }
    }
}
