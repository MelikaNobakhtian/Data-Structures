using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A6
{
    public class Q5LCSOfThree : Processor
    {
        public Q5LCSOfThree(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2, long[] seq3)
        {

            long[,,] matchnumber = new long[seq1.Length + 1, seq2.Length + 1, seq3.Length + 1];
            bool[] sequence1 = new bool[seq1.Length];
            bool[] sequence2 = new bool[seq2.Length];
            bool[] sequence3 = new bool[seq3.Length];
            for (int i = 1; i <= seq1.Length; i++)
            {

                for (int j = 1; j <= seq2.Length; j++)
                {
                    for (int k = 1; k <= seq3.Length; k++)
                    {
                        long countplus = 0;
                        if (seq1[i - 1] == seq2[j - 1] && seq2[j - 1] == seq3[k - 1] && !sequence1[i - 1] && !sequence2[j - 1] && !sequence3[k - 1])
                        {
                            countplus = 1;
                            sequence3[k - 1] = true;
                            sequence2[j - 1] = true;
                            sequence1[i - 1] = true;
                        }

                        var a = Math.Max(Math.Max(matchnumber[i, j - 1, k], matchnumber[i - 1, j, k]), matchnumber[i - 1, j - 1, k]);
                        var b = Math.Max(Math.Max(matchnumber[i, j - 1, k], matchnumber[i, j, k - 1]), matchnumber[i, j - 1, k]);
                        var c = Math.Max(Math.Max(matchnumber[i, j, k - 1], matchnumber[i - 1, j, k]), matchnumber[i - 1, j, k - 1]);

                        matchnumber[i, j, k] = Math.Max(Math.Max(a, b), c) + countplus;
                    }
                }

            }

            return matchnumber[seq1.Length, seq2.Length, seq3.Length];
        }


    }
}
