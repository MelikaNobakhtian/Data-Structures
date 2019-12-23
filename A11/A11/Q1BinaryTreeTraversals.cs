using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;
using System.Linq;

namespace A11
{

    public class Q1BinaryTreeTraversals : Processor
    {
        public Q1BinaryTreeTraversals(string testDataName) : base(testDataName) { }
        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);

        public long[][] Solve(long[][] nodes)
        {
            Node[] tree = new Node[nodes.Length];
            long rootnode = 0;
            for (int i = 0; i < tree.Length; i++)
            {
                if (tree[i] == null)
                    tree[i] = new Node(nodes[i][0]);
                else
                    tree[i].Key = nodes[i][0];
                //tree[i].leftchild = nodes[i][1];
                if (nodes[i][1] != -1)
                {
                    if (tree[nodes[i][1]] == null)
                        tree[nodes[i][1]] = new Node(0);
                    tree[nodes[i][1]].Parent = i;
                }
               // tree[i].rightchild = nodes[i][2];
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

            return PreOrder(rootnode, tree);


        }

        public long[] InOrder(long root, Node[] tree)
        {
            Stack<long> nodes = new Stack<long>();
            long[] results = new long[tree.Length];
            var child = root;
            int idx = 0;
            while (nodes.Count != 0 || child != -1)
            {
                while (true)
                {
                    if (tree[child].leftchild == -1)
                        break;
                    else
                        child = tree[child].leftchild;
                }

                var present = nodes.Pop();
                results[idx] = present;
                child = tree[present].rightchild;
                idx++;
            }

            return results;

        }

        public long[][] PreOrder(long root, Node[] tree)
        {
            long[] preresult = new long[tree.Length];
            long[] postresult = new long[tree.Length];
            long[] inorderresult = new long[tree.Length];
            long idx = 0;
            long postidx = 0;
            long inidx = 0;
            long child = root;
            Stack<long> nodes = new Stack<long>();
            nodes.Push(child);
            while (nodes.Count != 0)
            {
                var node = nodes.Pop();
                preresult[idx] = tree[node].Key;
               
                if (tree[node].rightchild != -1)
                    nodes.Push(tree[node].rightchild);
                
                if (tree[node].leftchild != -1)
                    nodes.Push(tree[node].leftchild);
                else
                {
                    inorderresult[inidx++] = tree[node].Key;
                    long thisnode = 0;
                    if (nodes.Count == 0)
                    {
                        thisnode = tree[root].rightchild;
                    }
                    else
                        thisnode = nodes.Peek();
                    while (thisnode != tree[node].rightchild)
                    {
                        postresult[postidx++] = tree[node].Key;
                        node = tree[node].Parent;
                        if(inidx!=tree.Length)
                            inorderresult[inidx++]=tree[node].Key;

                    }
                }
               
                idx++;
            }
            return new long[3][] { inorderresult.ToArray(), preresult, postresult.ToArray() };
        }


    }
}