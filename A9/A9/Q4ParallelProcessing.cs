using System;
using System.Collections.Generic;
using TestCommon;

namespace A9
{
    public class Q4ParallelProcessing : Processor
    {
        public Q4ParallelProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
            Tuple<long, long>[] jobs = new Tuple<long, long>[jobDuration.Length];
            long[] threadstime = new long[threadCount];
            long[] threads = new long[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                threadstime[i] = jobDuration[i];
                jobs[i] = new Tuple<long, long>(i, 0);
                threads[i] = i;
            }
            QuickSort(threads, threadstime, 0, (int)threadCount - 1);
            for (int i = (int)threadCount; i < jobDuration.Length; i++)
            {
                jobs[i] = new Tuple<long, long>(threads[0], threadstime[0]);
                ChangePriority(0, threadstime[0] + jobDuration[i], threadstime, threads);
            }

            return jobs;


        }

        public void ChangePriority(int i, long p, long[] heap, long[] idx)
        {
            var oldvalue = heap[i];
            heap[i] = p;
            if (p > oldvalue)
                SiftDown(i, heap, idx);
            else
                SiftUp(i, heap, idx);
        }

        public void SiftUp(int i, long[] heap, long[] idx)
        {
            while (i > 0 && heap[(i - 1) / 2] > heap[i])
            {
                (heap[i], heap[(i - 1) / 2]) = (heap[(i - 1) / 2], heap[i]);
                (idx[i], idx[(i - 1) / 2]) = (idx[(i - 1) / 2], idx[i]);
            }
        }

        public void SiftDown(int i, long[] heap, long[] idx)
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
                (idx[i], idx[minindex]) = (idx[minindex], idx[i]);
                SiftDown(minindex, heap, idx);
            }

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
    }
}
