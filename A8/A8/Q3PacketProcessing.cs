using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A8
{
    public class Q3PacketProcessing : Processor
    {
        public Q3PacketProcessing(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long[]>)Solve);

        public long[] Solve(long bufferSize, 
            long[] arrivalTimes, 
            long[] processingTimes)
        {
            if (processingTimes.Length == 0)
                return new long[] { };
            if (processingTimes.Length == 1)
                return new long[] { arrivalTimes[0] };
            Queue<long> box = new Queue<long>();
            long[] results = new long[arrivalTimes.Length];
            box.Enqueue(0);
            long time = arrivalTimes[0];

            for (int i = 1; i < arrivalTimes.Length; i++)
            {
                while (box.Count != 0 && time + processingTimes[box.Peek()] <= arrivalTimes[i])
                {
                    var boxx = box.Dequeue();

                    if (time < arrivalTimes[boxx])
                        time = arrivalTimes[boxx];
                    results[boxx] = time;
                    time += processingTimes[boxx];

                }
                if (box.Count + 1 <= bufferSize)
                {
                    box.Enqueue(i);
                }
                else
                    results[i] = -1;
            }
            while (box.Count != 0)
            {
                var boxx = box.Dequeue();
                
                if (time < arrivalTimes[boxx])
                    time = arrivalTimes[boxx];
                results[boxx] = time;
                time += processingTimes[boxx];
            }

            return results;

            //Queue<long> box = new Queue<long>();
            //int i = 1;
            //int arrivalidx = 0;
            //long size = 0;

            //long arrival = arrivalTimes[0];
            //while (arrivalidx < arrivalTimes.Length)
            //{
            //    while (true)
            //    {
            //        if (arrival > arrivalTimes[i])
            //        {
            //            results[i] = -1;
            //            i++;
            //        }
            //       else if (size != bufferSize && arrival == arrivalTimes[i])
            //        {
            //            box.Enqueue(processingTimes[i]);
            //            i++;
            //            size++;
            //        }
            //        else if (arrival != arrivalTimes[i])
            //        {
            //            if (arrivalidx == 0)
            //                arrival = arrivalTimes[0];
            //            FindArrivals(results, box, arrival, arrivalidx);
            //            arrival = results[i-1];
            //            arrivalidx = i;
            //            break;
            //        }
            //    }
            //}

            //return results;


        }

        //private void FindArrivals(long[] results, Queue<long> box,long arrivaltime,int index)
        //{
        //    while (box.Count != 0)
        //    {
        //        results[index] = arrivaltime;
        //        var presentbox = box.Dequeue();
        //        arrivaltime += presentbox;
        //        index++;
        //    }
        //}

       
    }
}