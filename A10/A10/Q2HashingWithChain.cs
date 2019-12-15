using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class Q2HashingWithChain : Processor
    {
        public Q2HashingWithChain(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, string[], string[]>)Solve);

        public int  buckets;
        public List<string>[] hash;
        public string[] Solve(long bucketCount, string[] commands)
        {
            hash = new List<string>[bucketCount];
            buckets = (int)bucketCount;
            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];

                switch (cmdType)
                {
                    case "add":
                        Add(arg);
                        break;
                    case "del":
                        Delete(arg);
                        break;
                    case "find":
                        result.Add(Find(arg));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str, int start, int count,
            long p = BigPrimeNumber, long x = ChosenX)
        {
            long hash = 0;
           for(int i = str.Length-1;i>=start;i--)
            {
                
                hash = (hash * x + str[i]) % p;
                //hash %= count;
            }
            return hash%count;
        }

        public void Add(string str)
        {
            long hashfunc = PolyHash(str, 0, buckets);
            if (hash[hashfunc] != null)
            {
                foreach (var str1 in hash[hashfunc])
                    if (str1 == str)
                        return;
                hash[hashfunc].Add(str);
            }
            else
                hash[hashfunc] = new List<string>() { str };
            
        }

        public string Find(string str)
        {
            long hashfunc = PolyHash(str, 0, buckets);
            if (hash[hashfunc]!=null  && hash[hashfunc].Contains(str))
                return "yes";
            else
                return "no";
        }

        public void Delete(string str)
        {
            long hashfunc = PolyHash(str, 0, buckets);
            if(hash[hashfunc]!=null  && hash[hashfunc].Contains(str))
                hash[hashfunc].Remove(str);
        }

        public string Check(int i)
        {
            if (hash[i] == null || hash[i].Count==0)
                return "-";
            hash[i].Reverse();
            return String.Join(' ', hash[i].Select(s => s));
        }
    }
}
