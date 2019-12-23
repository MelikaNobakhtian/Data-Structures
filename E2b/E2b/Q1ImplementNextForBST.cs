using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace E2b
{
    public class Q1ImplementNextForBST : Processor
    {
        public Q1ImplementNextForBST(string testDataName) : base(testDataName) 
        {
            //this.ExcludeTestCaseRangeInclusive(1, 10);
        }
        public override string Process(string inStr)
        {
            long n, node;
            var lines = inStr.Split(TestTools.NewLineChars, StringSplitOptions.RemoveEmptyEntries);
            TestTools.ParseTwoNumbers(lines[0], out n, out node);
            var bst = lines[1].Split(TestTools.IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();

            return Solve(n, node, bst).ToString();
        }

        public long Solve(long n, long node, long[] BST)
        {
            return Next(node, n, BST);           
        }

        public long RightChild(long idx)
        {
            return 2 * idx + 2;
        }
        public long LeftChild(long idx)
        {
            return 2 * idx + 1;
        }
        public long Next(long node,long n,long[] tree)
        {
            var right = RightChild(node);
            if (right < n && tree[right] != -1)
            {
                return LeftDes(right, n, tree);
            }
            else
                return RightAnc(node, n, tree);
        }

        private long RightAnc(long node, long n, long[] tree)
        {
            if (node == 0)
                return -1;
            var parent = Parent(node);
            if (tree[parent] > tree[node])
                return parent;
            else
                return RightAnc(parent, n, tree);
           
        }

        public long Parent(long idx)
        {
            return (idx - 1) / 2;
        }

        private long LeftDes(long node, long n, long[] tree)
        {

            var left = LeftChild(node);

            if (left < n && tree[left] == -1)
                return node;
            else if (left < n)
                return LeftDes(left, n, tree);
            else
                return -1;
        }
    }
}