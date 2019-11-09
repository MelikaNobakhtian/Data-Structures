using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A6
{
    public class Q4LCSOfTwo : Processor
    {
        public Q4LCSOfTwo(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2)
        {
            long[,] matchnumber = new long[seq1.Length + 1, seq2.Length + 1];
            for(int i = 1; i <= seq1.Length; i++)
            {
                
                for(int j = 1; j <= seq2.Length; j++)
                {
                    long countplus = 0;
                    if(seq1[i-1]==seq2[j-1]  )
                    {
                        countplus = 1;
                    }
                    matchnumber[i, j] = Math.Max(Math.Max(matchnumber[i, j - 1], matchnumber[i - 1, j]), matchnumber[i - 1, j - 1]) + countplus;
                }
            }

            return matchnumber[seq1.Length, seq2.Length];
        }
    }
}
