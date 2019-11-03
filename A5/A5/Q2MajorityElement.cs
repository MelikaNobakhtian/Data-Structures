using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A5
{
    public class Q2MajorityElement:Processor
    {

        public Q2MajorityElement(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);


        public virtual long Solve(long n, long[] a)
        {
            var result = MajorityElement(a, 0, a.Length - 1);
            return result.Item1 == long.MinValue ? 0 : 1;
        }

        public static Tuple<long, long> MajorityElement(long[] a, int low, int high)
        {
            if (high == low)
                return new Tuple<long, long>(a[low], 1);
            var mid = (low + high) / 2;
            var left = MajorityElement(a, low, mid);
            Tuple<long, long> right = new Tuple<long, long>(0, 0);
            int start;
            if ((high - low) % 2 == 0)
            {
                right = MajorityElement(a, mid, high);
                start = mid;
            }
            else
            {
                right = MajorityElement(a, mid + 1, high);
                start = mid + 1;
            }


            if (left.Item1 == long.MinValue && right.Item1 == long.MinValue)
                return new Tuple<long, long>(long.MinValue, 0);
            if (left.Item1 == right.Item1)
                return new Tuple<long, long>(right.Item1, right.Item2 + right.Item2);
            if (left.Item1 != long.MinValue && right.Item1 == long.MinValue)
            {
                var count = 0;
                for (int i = start; i <= high; i++)
                    if (a[i] == left.Item1)
                        count++;
                if (count + left.Item2 > (high - low + 1) / 2)
                    return new Tuple<long, long>(left.Item1, left.Item2 + count);
                else
                    return new Tuple<long, long>(long.MinValue, 0);
            }

            if (right.Item1 != long.MinValue && left.Item1 == long.MinValue)
            {
                var count = 0;
                for (int i = low; i <= mid; i++)
                    if (a[i] == right.Item1)
                        count++;
                if ((high - low + 1) / 2 < count + right.Item2)
                    return new Tuple<long, long>(right.Item1, right.Item2 + count);
                else
                    return new Tuple<long, long>(long.MinValue, 0);
            }

            if (left.Item1 != right.Item1)
            {
                int count = 0;
                int count1 = 0;
                for (int i = low; i <= mid; i++)
                    if (a[i] == right.Item1)
                        count1++;
                for (int i = start; i <= high; i++)
                    if (a[i] == left.Item1)
                        count++;
                if (count1 + right.Item2 > (high - low + 1) / 2)
                    return new Tuple<long, long>(right.Item1, count1 + right.Item2);
                else if (count + left.Item2 > (high - low + 1) / 2)
                    return new Tuple<long, long>(left.Item1, count + left.Item2);
                else
                    return new Tuple<long, long>(long.MinValue, 0);
            }
                
            return new Tuple<long, long>(long.MinValue, 0);
        }
    }
}
