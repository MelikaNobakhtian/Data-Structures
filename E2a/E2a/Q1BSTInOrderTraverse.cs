using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace E2a
{
    public class Q1BSTInOrderTraverse : Processor
    {
        public Q1BSTInOrderTraverse(string testDataName) : base(testDataName) { }
        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)Solve);
        List<long> result;
        public long[] Solve(long n, long[] BST)
        {
            result = new List<long>();
            Dfs(n, BST, 0);
            return result.ToArray();
        }

        public void Dfs(long n,long[] Bst,int idx)
        {
            if (idx>= n || Bst[idx] == -1)
            {              
                return;
            }
            Dfs(n, Bst, 2 * idx + 1);
            result.Add(Bst[idx]);
            Dfs(n, Bst, 2 * idx + 2);

        }
    }
}