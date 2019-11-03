using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A5
{
    public class Q1BinarySearch : Processor
    {
        public Q1BinarySearch(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long [], long[]>)Solve);


        public virtual long[] Solve(long[] a, long[] b)
        {
            var result = new long[b.Length];
            for (int i = 0; i < b.Length; i++)
                result[i] = BinarySearch(b[i], a, 0, a.Length - 1);
            return result;
        }

        public static long BinarySearch(long n,long[] a,int low,int high)
        {
            if (high < low)
                return -1;
            int mid = (high + low) / 2;
            if (a[mid] == n)
                return mid;
            return (a[mid] > n) ? BinarySearch(n, a, low, mid-1 ) : BinarySearch(n, a, mid+1 , high);
        }
    }
}
