using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestCommon;

namespace E2a
{
    public class Q2ThreeChildrenMinHeap : Processor
    {
        public Q2ThreeChildrenMinHeap(string testDataName) : base(testDataName) { }
        public override string Process(string inStr)
        {
            long n;
            long changeIndex, changeValue;
            long[] heap;
            using (StringReader reader = new StringReader(inStr))
            {
                n = long.Parse(reader.ReadLine());

                string line = null;
                line = reader.ReadLine();

                TestTools.ParseTwoNumbers(line, out changeIndex, out changeValue);

                line = reader.ReadLine();
                heap = line.Split(TestTools.IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => long.Parse(x)).ToArray();
            }

            return string.Join("\n", Solve(n, changeIndex, changeValue, heap));

        }
        public long[] Solve(
            long n, 
            long changeIndex, 
            long changeValue, 
            long[] heap)
        {
            long value = 0;
            value = changeValue + heap[changeIndex];
            ChangePriority(changeIndex, value, heap,heap.Length);
            return heap;
        }

        public void ChangePriority(long idx,long key,long[] heap, long n)
        {
            var oldkey = heap[idx];
            heap[idx] = key;
            if (oldkey > key)
                SiftUp(idx,heap);
            if (oldkey < key)
                SiftDown(idx,heap,n);
        }

        private void SiftDown(long idx, long[] heap,long length)
        {
            var minidx = idx;
            var left = LeftChild(idx);
            if (left < length && heap[left] != -1 && heap[minidx] > heap[left])
                minidx = left;
            var mid = MidChild(idx);
            if (mid < length && heap[mid] != -1 && heap[minidx] > heap[mid])
                minidx = mid;
            var right = RightChild(idx);
            if (right < length && heap[right] != -1 && heap[minidx] > heap[right])
                minidx = right;
            if (idx != minidx)
            {
                (heap[idx], heap[minidx]) = (heap[minidx], heap[idx]);
                SiftDown(minidx, heap,length);
            }
        }

        private long RightChild(long idx)
        {
            return 3 * idx + 3;
        }

        private long MidChild(long idx)
        {
            return 3 * idx + 2;
        }

        private long LeftChild(long idx)
        {
            return 3 * idx + 1;
        }

        private void SiftUp(long idx, long[] heap)
        {
            while(idx>0 && heap[Parent(idx)]>heap[idx])
            {
                var parent = Parent(idx);
                (heap[parent], heap[idx]) = (heap[idx], heap[parent]);
                idx = parent;
            }
        }

        public long Parent(long idx)
        {
            return (idx - 1) / 3;
        }
    }
}
