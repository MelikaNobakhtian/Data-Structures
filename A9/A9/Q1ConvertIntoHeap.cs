using System;
using System.Collections.Generic;
using TestCommon;

namespace A9
{
    public class Q1ConvertIntoHeap : Processor
    {
        public Q1ConvertIntoHeap(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);



        public Tuple<long, long>[] Solve(long[] array)
        {
            List<Tuple<long, long>> swaps = new List<Tuple<long, long>>();
            int last = array.Length / 2;
            for (int i = last; i >= 0; i--)
                SiftDown(i, array, swaps);


            return swaps.ToArray();
        }

        public void SiftDown(int i, long[] heap, List<Tuple<long, long>> swaps)
        {
            int minindex = i;
            int leftchild;
            int rightchild;
            if (2 * i + 1 < heap.Length)
            {
                leftchild = 2 * i + 1;
                if (heap[leftchild] < heap[minindex])
                    minindex = leftchild;
            }
            if (2 * i + 2 < heap.Length)
            {
                rightchild = 2 * i + 2;
                if (heap[minindex] > heap[rightchild])
                    minindex = rightchild;
            }
            if (i != minindex)
            {
                (heap[i], heap[minindex]) = (heap[minindex], heap[i]);
                swaps.Add(new Tuple<long, long>(i, minindex));
                SiftDown(minindex, heap, swaps);
            }

        }
    }
}
