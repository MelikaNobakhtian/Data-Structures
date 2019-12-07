using System;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q2MergingTables : Processor
    {
        long[] parent;
        long[] tableSizes;
        long[] rank;

        public Q2MergingTables(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);


        public long[] Solve(long[] tableSizes, long[] targetTables, long[] sourceTables)
        {
            long[] results = new long[targetTables.Length];
            parent = new long[tableSizes.Length];
            rank = new long[tableSizes.Length];
            this.tableSizes = tableSizes;
            long maxsize = 0;
            for(int i = 0; i < tableSizes.Length; i++)
            {
                parent[i] = i ;
                if (this.tableSizes[i] > maxsize)
                    maxsize = this.tableSizes[i];
            }
            for(int i = 0; i < targetTables.Length; i++)
            {
                if (targetTables[i] == sourceTables[i])
                {
                    results[i] = maxsize;
                    continue;
                }
                  
                var idx=Union(sourceTables[i], targetTables[i]);
                if (this.tableSizes[idx] > maxsize)
                {
                    results[i] =this.tableSizes[idx];
                    maxsize = this.tableSizes[idx];
                }
                else
                    results[i] = maxsize;

            }

            return results;
        }

     
        public long Find(long i)
        {
            if (i != parent[i])
                parent[i] = Find(parent[i]);
            return parent[i];
        }

        public long Union(long i,long j)
        {
            long presentparent = 0;
            var i_id = Find(i-1);
            var j_id = Find(j-1);
            if (i_id == j_id)
                return i_id;
            if (rank[i_id] > rank[j_id])
            {
                presentparent = i_id;
                parent[j_id] = i_id;
                tableSizes[i_id] = tableSizes[i_id] + tableSizes[j_id];
                tableSizes[j_id] = 0;
            }
            else
            {
                presentparent = j_id;
                parent[i_id] = j_id;
                tableSizes[j_id] = tableSizes[i_id] + tableSizes[j_id];
                tableSizes[i_id] = 0;
            }
                
            if (rank[i_id] == rank[j_id])
                rank[j_id] = rank[i_id] + 1;

            return presentparent;
        }

    }
}
