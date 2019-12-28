using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A11
{
    public class Q4SetWithRangeSums : Processor
    {
        public Q4SetWithRangeSums(string testDataName) : base(testDataName)
        {
            CommandDict =
                        new Dictionary<char, Func<string, string>>()
                        {
                            ['+'] = Add,
                            ['-'] = Del,
                            ['?'] = Find,
                            ['s'] = Sum
                        };
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        public readonly Dictionary<char, Func<string, string>> CommandDict;

        public const long M = 1_000_000_001;

        public long X = 0;

        protected List<long> Data;

        public List<Node> SplayTree;

        public int root;

        public string[] Solve(string[] lines)
        {
            SplayTree = new List<Node>();
            root = -1;
            X = 0;
            Data = new List<long>();
            List<string> result = new List<string>();
            foreach (var line in lines)
            {
                char cmd = line[0];
                string args = line.Substring(1).Trim();
                var output = CommandDict[cmd](args);
                if (null != output)
                    result.Add(output);
            }
            return result.ToArray();
        }

        private long Convert(long i)
            => i = (i + X) % M;

        private string Add(string arg)
        {
            long i = Convert(long.Parse(arg));
            var res = BSTinsert(i, SplayTree);
            if(SplayTree.Count!=1)
                Splay(res, SplayTree);
            return null;
        }

        private string Del(string arg)
        {
            long i = Convert(long.Parse(arg));
            var pos = BSTfind(i, root, SplayTree);
            Splay((int)Next((int)pos,SplayTree), SplayTree);
            Splay((int)pos, SplayTree);
            var left = SplayTree[(int)pos].leftchild;
            var right = SplayTree[(int)pos].rightchild;
            SplayTree[right].leftchild = left;
            SplayTree[left].Parent = right;
            root = right;
            SplayTree[right].Parent = -1;
            return null;
        }

        private string Find(string arg)
        {
            long i = Convert(int.Parse(arg));
            long idx = BSTfind(i, root, SplayTree);
            if (idx==-1 || SplayTree[(int)idx].Key != i)
                return "Not found";
            if(SplayTree.Count!=1)
                Splay((int)idx, SplayTree);
            return "Found";
        }

        private string Sum(string arg)
        {
            long sum = 0;
            var toks = arg.Split();
            long l = Convert(long.Parse(toks[0]));
            long r = Convert(long.Parse(toks[1]));
            var firstidx = BSTfind(l, root, SplayTree);
            if (SplayTree[(int)firstidx].Key == l)
                sum += SplayTree[(int)firstidx].Key;
            var first = Split(l, root, SplayTree);
            var second = Split(r, first.Item2, SplayTree);
            sum = SplayTree[second.Item1].Sum + SplayTree[second.Item1].Key;
            Merge(second.Item1, second.Item2, SplayTree);
            Merge(first.Item1, first.Item2, SplayTree);
            //var node = BSTfind(l, root, SplayTree);
            //long sum = 0;
            //while (SplayTree[(int)node].Key <= r)
            //{
            //    if(SplayTree[(int)node].leftchild!=-1)
            //    sum += SplayTree[SplayTree[(int)node].leftchild].Sum + SplayTree[SplayTree[(int)node].leftchild].Key+SplayTree[(int)node].Key;
            //    node = Next((int)node, SplayTree);
                
            //}

            X = sum;

            return sum.ToString();
        }

        public void Merge(int firstroot,int secondroot,List<Node> tree)
        {
            var node = BSTfind(long.MaxValue, firstroot, tree);
            Splay((int)node, tree);
            tree[(int)node].rightchild = secondroot;
            tree[secondroot].Parent = (int)node;
            root = firstroot;
            SetSumofNode(root, tree);
        }
        public Tuple<int,int> Split(long key,int root,List<Node> tree)
        {
            var res = BSTfind(key, root, tree);
            Splay((int)res, tree);
            if (tree[(int)res].Key > key)
                return CutLeft((int)res,tree);
            if (tree[(int)res].Key < key)
                return CutRight((int)res,tree);
            else
            {
                var right = tree[(int)res].rightchild;
                if(right==-1)

                tree[right].Parent = -1;
                tree[(int)res].rightchild = -1;
                SetSumofNode((int)res, tree);
                return new Tuple<int, int>((int)res, right);
            }
                

        }

        private Tuple<int, int> CutRight(int res, List<Node> tree)
        {
            var right = tree[res].rightchild;
            tree[res].rightchild = -1;
            tree[right].Parent = -1;
            return new Tuple<int, int>(res, right);
        }

        private Tuple<int, int> CutLeft(int pos,List<Node> tree)
        {
            var left = tree[pos].leftchild;
            tree[pos].leftchild = -1;
            tree[left].Parent = -1;
            return new Tuple<int, int>(left, pos);
        }

        public int BSTinsert(long key,List<Node> tree)
        {
            if (root == -1)
            {
                tree.Add(new Node(key));
                tree[0].Key = key;
                root = 0;
                return 0;
            }
            var pos = BSTfind(key, root, tree);
            tree.Add(new Node(key));
            tree[tree.Count - 1].Parent = (int)pos;
            if (tree[(int)pos].Key > key)
                tree[(int)pos].leftchild = tree.Count - 1;
            else
                tree[(int)pos].rightchild = tree.Count - 1;
            SetSumofNode((int)pos, tree);
            return tree.Count - 1;
        }

        private void SetSumofNode(int pos, List<Node> tree)
        {
            tree[pos].Sum = 0;
            var left = tree[pos].leftchild;
            var right = tree[pos].rightchild;
            if (left != -1)
                tree[pos].Sum += (tree[left].Sum == 0) ? tree[left].Key : tree[left].Key + tree[left].Sum;
            
            if (right != -1)
                tree[pos].Sum += (tree[right].Sum == 0) ? tree[right].Key : tree[right].Key + tree[right].Sum;
        }

        public long BSTfind(long key,int root,List<Node> tree)
        {
            if (root==-1 || tree[root].Key == key)
                return root;
           else if (tree[root].Key < key)
                if (tree[root].rightchild != -1)
                    return BSTfind(key, tree[root].rightchild, tree);
                else
                    return root;
           else if (tree[root].Key > key)
                if (tree[root].leftchild != -1)
                    return BSTfind(key, tree[root].leftchild, tree);
                else
                    return root;
            return root;

        }

        public long Next(int node, List<Node> tree)
        {
            if (tree[node].rightchild != -1)
                return Left(tree[node].rightchild, tree);
            else
                return Right(node, tree);
        }

        private long Right(int node, List<Node> tree)
        {
            if (tree[tree[node].Parent].leftchild == node)
                return tree[node].Parent;
            else
                return Right(tree[node].Parent, tree);
        }

        private long Left(int rightchild, List<Node> tree)
        {
            if (tree[rightchild].leftchild == -1)
                return rightchild;
            else
                return Left(tree[rightchild].leftchild, tree);
        }

        public void Splay(int node, List<Node> nodes)
        {
            var parent = nodes[node].Parent;
            if (parent == -1)
                return;
            if (nodes[parent].Parent == -1)
                Zig(node, nodes);
            else if ((node == nodes[parent].rightchild && nodes[nodes[parent].Parent].leftchild==parent ) ||
                (node == nodes[parent].leftchild && nodes[nodes[parent].Parent].rightchild == parent))
                ZigZag(node,nodes);
            else
                ZigZig(node,nodes);
            if (nodes[node].Parent != -1)
                Splay(node, nodes);
            else
                root = node;
        }

        private void ZigZig(long node, List<Node> nodes)
        {
            var parent = nodes[(int)node].Parent;
            var parentof = nodes[parent].Parent;
            if (nodes[parent].leftchild == node)
            {
                if (nodes[parentof].leftchild == -1)
                    nodes[(int)node].Parent = -1;
                else
                    nodes[(int)node].Parent = nodes[parentof].Parent;
                nodes[parent].leftchild = nodes[(int)node].rightchild;
                nodes[parentof].leftchild = nodes[parent].rightchild;
                nodes[(int)node].rightchild = parent;
                nodes[parent].Parent = (int)node;
                nodes[parent].rightchild = parentof;
                nodes[parentof].Parent = parent;
            }
            else
            {
                if (nodes[parentof].leftchild == -1)
                    nodes[(int)node].Parent = -1;
                else
                    nodes[(int)node].Parent = nodes[parentof].Parent;
                nodes[parent].rightchild = nodes[(int)node].leftchild;
                nodes[parentof].rightchild = nodes[parent].leftchild;
                nodes[(int)node].leftchild = parent;
                nodes[parent].Parent =(int) node;
                nodes[parent].leftchild = parentof;
                nodes[parentof].Parent = parent;
            }
            SetSumofNode(parentof, nodes);
            SetSumofNode(parent, nodes);
            SetSumofNode((int)node, nodes);
        }

        private void ZigZag(int node, List<Node> nodes)
        {
            var parent = nodes[node].Parent;
            int parentof = 0;
            if (node == nodes[parent].rightchild)
            {
                parentof = nodes[parent].Parent;
                if (nodes[parentof].Parent == -1)
                    nodes[node].Parent = -1;
                else
                    nodes[node].Parent = nodes[parentof].Parent;
                nodes[parentof].leftchild = nodes[node].rightchild;
                nodes[parent].rightchild = nodes[node].leftchild;
                nodes[node].rightchild = parentof;
                nodes[node].leftchild = parent;
                nodes[parent].Parent = nodes[parentof].Parent = node;

            }
            else
            {
                parentof = nodes[parent].Parent;
                if (nodes[parentof].Parent == -1)
                    nodes[node].Parent = -1;
                else
                    nodes[node].Parent = nodes[parentof].Parent;
                nodes[parentof].rightchild = nodes[node].leftchild;
                nodes[parent].leftchild = nodes[node].rightchild;
                nodes[node].leftchild = parentof;
                nodes[node].rightchild = parent;
                nodes[parent].Parent = nodes[parentof].Parent = node;
            }
            SetSumofNode(parent, nodes);
            SetSumofNode(parentof, nodes);
            SetSumofNode(node, nodes);
        }

        private void Zig(int node, List<Node> nodes)
        {
            var parent = nodes[node].Parent;
            if (node == nodes[parent].rightchild)
            {
                nodes[node].Parent = -1;
                nodes[parent].rightchild = nodes[node].leftchild;
                nodes[node].leftchild = parent;
                nodes[parent].Parent = node;
            }
            else
            {
                nodes[node].Parent = -1;
                nodes[parent].leftchild = nodes[node].rightchild;
                nodes[node].rightchild = parent;
                nodes[parent].Parent = node;
            }
            SetSumofNode(parent, nodes);
            SetSumofNode(node, nodes);
        }
    }
}
