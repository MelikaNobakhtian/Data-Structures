using System;
using System.Collections.Generic;
using TestCommon;

namespace E1a
{
    public class Q2UnitFractions : Processor
    {
        public Q2UnitFractions(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long>)Solve);


        public virtual long Solve(long nr, long dr)
        {
           
            while (true)
            {
                if (nr == 1)
                    return dr;
                if (nr < dr && dr % nr == 0)
                    return dr / nr;
                if (nr > dr && nr % dr == 0)
                    return 1;
                var ourdr = bigestdr(nr,dr);
                if (dr % nr == 0)
                    dr = dr / nr;
                if (dr == ourdr)
                    return ourdr;
                nr = (nr * ourdr) - (dr);
                dr = dr * ourdr;
            }
        }

        public static long bigestdr(long nr ,long dr)
        {
            float kasr = (float)nr / (float)dr;
            long i = 1;
            while (true)
            {
                float less = (float)1 / (float)i;
                if (less <= kasr)
                    return i;
                i++;
            }
        }
    }
}
