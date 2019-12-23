using System;
using System.Linq;
using TestCommon;

namespace A11
{
    public class Node
    {
        public long Key { get; set; }
        public long Sum { get; set; }
        public int Parent { get; set; }
        public int leftchild { get; set; }
        public int rightchild { get; set; }
        public Node(long value)
        {
            Key = value;
            Parent = -1;
            leftchild = -1;
            rightchild = -1;
            Sum = 0;
        }
    }
    public class Q2IsItBST : Processor
    {
        public Q2IsItBST(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);

        public bool Solve(long[][] nodes)
        {
            Node[] tree = new Node[nodes.Length];
            long rootnode = 0;
            for (int i = 0; i < tree.Length; i++)
            {
                if (tree[i] == null)
                    tree[i] = new Node(nodes[i][0]);
                else
                    tree[i].Key = nodes[i][0];
                tree[i].leftchild = (int)nodes[i][1];
                if (nodes[i][1] != -1)
                {
                    if (tree[nodes[i][1]] == null)
                        tree[nodes[i][1]] = new Node(0);
                    tree[nodes[i][1]].Parent = i;
                }
                tree[i].rightchild = (int)nodes[i][2];
                if (nodes[i][2] != -1)
                {
                    if (tree[nodes[i][2]] == null)
                        tree[nodes[i][2]] = new Node(0);
                    tree[nodes[i][2]].Parent = i;
                }

            }
            for (int i = 0; i < tree.Length; i++)
            {
                if (tree[i].Parent == -1)
                {
                    rootnode = i;
                    break;
                }
            }
            var child = rootnode;
            var rightchild = rootnode;
            while (true)
            {
                if (tree[child].leftchild == -1)
                    break;
                else
                    child = tree[child].leftchild;
            }
            while (true)
            {
                if (tree[rightchild].rightchild == -1)
                    break;
                else
                    rightchild = tree[rightchild].rightchild;
            }

            while (child != rightchild)
            {
                var result = Next(child, tree);
                if (tree[child].rightchild != -1)
                {
                    if (tree[child].Key >= tree[result].Key)
                        return false;
                    child = result;
                    continue;
                }
                else
                {
                    if (tree[child].Key >= tree[result].Key)
                        return false;
                    child = result;
                }
            }

            return true;

        }

        public long Next(long node, Node[] tree)
        {
            if (tree[node].rightchild != -1)
                return Left(tree[node].rightchild, tree);
            else
                return Right(node, tree);
        }

        private long Right(long node, Node[] tree)
        {
            if (tree[tree[node].Parent].leftchild == node)
                return tree[node].Parent;
            else
                return Right(tree[node].Parent, tree);
        }

        private long Left(long rightchild, Node[] tree)
        {
            if (tree[rightchild].leftchild == -1)
                return rightchild;
            else
                return Left(tree[rightchild].leftchild, tree);
        }
    }
}
