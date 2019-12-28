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

            return PreOrder(rootnode, tree);


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
                idx++;
                if (tree[node].rightchild != -1)
                    nodes.Push(tree[node].rightchild);
                
                if (tree[node].leftchild != -1)
                    nodes.Push(tree[node].leftchild);
                else
                {
                    inorderresult[inidx++] = tree[node].Key;
                    if (tree[node].rightchild != -1)
                        continue;
                    postresult[postidx++] = tree[node].Key;
                    long thisnode = 0;
                    if (nodes.Count == 0)
                    {
                        tree[root].rightchild = -2;
                        thisnode = tree[root].rightchild;
                    }
                    else
                        thisnode = nodes.Peek();
                    while (thisnode != tree[node].rightchild)
                    {
                        bool left = node == tree[tree[node].Parent].leftchild;
                        node = tree[node].Parent;
                        if (!left || tree[node].rightchild==-1 || tree[node].rightchild==-2)
                            postresult[postidx++] = tree[node].Key;
                       
                        if(inidx!=tree.Length && left)
                            inorderresult[inidx++]=tree[node].Key;

                    }
                }
               
               
            }
            return new long[3][] { inorderresult.ToArray(), preresult, postresult.ToArray() };
        }


    }
}