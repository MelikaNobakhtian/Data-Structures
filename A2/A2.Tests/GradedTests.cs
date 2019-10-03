using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2.Tests
{
    [DeploymentItem("TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod()]
        public void SolveTest_Q1NaiveMaxPairWise()
        {
            RunTest(new Q1NaiveMaxPairWise("TD1"));
        }

        [TestMethod(), Timeout(1500)]
        public void SolveTest_Q2FastMaxPairWise()
        {
            RunTest(new Q2FastMaxPairWise("TD2"));
        }

        [TestMethod()]
        public void SolveTest_StressTest()
        {
            var Q1 = new Q1NaiveMaxPairWise("TD2");
            var Q2 = new Q2FastMaxPairWise("TD2");
            Stopwatch time = new Stopwatch();
            time.Start();
            while (time.ElapsedMilliseconds<5000)
            {
                var array = RandomTests();
                Assert.AreEqual(Q1.Solve(array), Q2.Solve(array));
            }
            
           
          

        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A2", p.Process, p.TestDataName, p.Verifier);
        }

        public static long[] RandomTests()
        {
            Random random = new Random();
            int n = random.Next(4, 50);
            var longarray = new long[n];
            for(int i=0;i<n; i++)
                longarray[i] = random.Next(0, int.MaxValue);

            return longarray;
        }

    }
}