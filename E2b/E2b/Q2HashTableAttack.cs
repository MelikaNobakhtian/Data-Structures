using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestCommon;

namespace E2b
{
    public class Q2HashTableAttack : Processor
    {
        public Q2HashTableAttack (string testDataName) : base(testDataName) 
        {
        }

        public override string Process(string inStr)
        {
            long bucketCount = long.Parse(inStr);
            return string.Join("\n", Solve(bucketCount));
        }

        public string[] Solve(long bucketCount)
        {
            long size = 9 * bucketCount / 10;
            List<string> result = new List<string>();
            Random rnd = new Random();
            var idx = rnd.Next(0, 25);
            var str = CapChars[idx].ToString();
            var hash = GetBucketNumber(str, bucketCount);
            for (int i = 0; i < 26 && result.Count < size; i++)
            {
                string str1 = CapChars[i].ToString();
                if (hash == GetBucketNumber(str1, bucketCount))
                    result.Add(str1);
            }
            for (int i = 0; i < 26 && result.Count < size; i++)
            {
                string str1 = CapChars[i].ToString();
                if (hash == GetBucketNumber(str1, bucketCount))
                    result.Add(str1);
            }
            for (int i = 0; i < 10 && result.Count < size; i++)
            {
                string str1 = Numbers[i].ToString();
                if (hash == GetBucketNumber(str1, bucketCount))
                    result.Add(str1);
            }
            if (result.Count == size)
                return result.ToArray();

            for(int i = 0; i < 26 && result.Count<size; i++)
            {
                for(int j = 0; j < 26 && result.Count<size; j++)
                {
                    string test = CapChars[i].ToString() + CapChars[j].ToString();
                    if (hash == GetBucketNumber(test, bucketCount))
                        result.Add(test);
                }
            }
            for (int i = 0; i < 26 && result.Count < size; i++)
            {
                for (int j = 0; j < 26 && result.Count < size; j++)
                {
                    string test = LowChars[i].ToString() + LowChars[j].ToString();
                    if (hash == GetBucketNumber(test, bucketCount))
                        result.Add(test);
                }
            }

            for (int i = 0; i < 10 && result.Count < size; i++)
            {
                for (int j = 0; j < 10 && result.Count < size; j++)
                {
                    string test = Numbers[i].ToString() + Numbers[j].ToString();
                    if (hash == GetBucketNumber(test, bucketCount))
                        result.Add(test);
                }
            }

            while (result.Count < size)
            {
                string a=new string(Enumerable.Repeat(CapChars, 3).Select(s => s[rnd.Next(s.Length)]).ToArray());
                if (hash == GetBucketNumber(a, bucketCount) && !result.Contains(a))
                    result.Add(a);
                if (result.Count < size)
                {
                    string b= new string(Enumerable.Repeat(LowChars, 3).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    if (hash == GetBucketNumber(b, bucketCount) && !result.Contains(b))
                        result.Add(b);
                }

                if (result.Count < size)
                {
                    string c = new string(Enumerable.Repeat(Numbers, 3).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    if (hash == GetBucketNumber(c, bucketCount) && !result.Contains(c))
                        result.Add(c);
                }

            }

            return result.ToArray();
            //for (int i = 0; i < 25 && result.Count < size; i++)
            //{
            //    string str1 =$"{CapChars[i]}{CapChars[i+1]}";
            //    if (hash == GetBucketNumber(str1, bucketCount))
            //        result.Add(str1);
            //}
            //for (int i = 0; i < 25 && result.Count < size; i++)
            //{
            //    string str1 = $"{LowChars[i]}{LowChars[i + 1]}";
            //    if (hash == GetBucketNumber(str1, bucketCount))
            //        result.Add(str1);
            //}

            //return result.ToArray();


        }

        #region Chars
        static char[] LowChars = Enumerable
            .Range(0, 26)
            .Select(n => (char)('a' + n))
            .ToArray();

        static char[] CapChars = Enumerable
            .Range(0, 26)
            .Select(n => (char)('A' + n))
            .ToArray();

        static char[] Numbers = Enumerable
            .Range(0, 10)
            .Select(n => (char)('0' + n))
            .ToArray();

        static char[] AllChars = 
            LowChars.Concat(CapChars).Concat(Numbers).ToArray();
        #endregion


        // پیاده‌سازی مورد استفاده دات‌نت برای پیدا کردن شماره باکت
        // https://referencesource.microsoft.com/#mscorlib/system/collections/generic/dictionary.cs,bcd13bb775d408f1
        public static long GetBucketNumber(string str, long bucketCount) =>
            (str.GetHashCode() & 0x7FFFFFFF) % bucketCount;
    }
}
