using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q3RabinKarp : Processor
    {
        public Q3RabinKarp(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long[]>)Solve);

        public long[] Solve(string pattern, string text)
        {
            List<long> results = new List<long>();
            var pHash = PolyHash(pattern, 0, (int)BigPrimeNumber);
            long[] Hash = new long[text.Length - pattern.Length + 1];
            Hash = PreComputeHashes(text, pattern.Length);
            for (int i = 0; i <= text.Length - pattern.Length; i++)
                if (pHash != Hash[i])
                    continue;
                else if (pattern == text.Substring(i, pattern.Length))
                    results.Add(i);
            return results.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str, int start, int count,
            long p = BigPrimeNumber, long x = ChosenX)
        {
            long hash = 0;
            for (int i = str.Length - 1; i >= start; i--)
            {
                hash = (hash * x + str[i]) % p;
            }
            return hash % count;
        }
        public static long[] PreComputeHashes(
            string T, 
            int P, 
            long p=BigPrimeNumber, 
            long x=ChosenX)
        {
            long[] Hash = new long[T.Length - P + 1];
            var s = T.Substring(T.Length - P , P);
            Hash[T.Length - P] = PolyHash(s, 0, (int)p);
            long y = 1;
            for (int i = 0; i < P; i++)
                y = (y * x) % p;
            y %= p;
            for (int i = T.Length - P - 1; i >= 0; i--)
            {
                var tmp = ((x * Hash[i + 1]) + (T[i] - y * T[i + P]));
                if (tmp < 0) tmp += p * 100;
                Hash[i] = ((tmp % p)+p) % p;
            }
               
            return Hash;
        }
    }
}
