using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    public static class Tests
    {
        public static void testListDictLoopTimes(int listSize, int testsToAverage)
        {
            Console.WriteLine("Testing List Loop Times");

            List<testComponent> componentsList = new List<testComponent>();
            Dictionary<vector2, testComponent> componentsDict = new Dictionary<vector2, testComponent>();

            Console.WriteLine("Fill Test 0 - For Loop and List");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            for(int i = 0; i < testsToAverage; i++)
            {
                watch.Stop();
                componentsList.Clear();
                watch.Start();
                for (int j = 0; j < listSize; j++)
                {
                    componentsList.Add(new testComponent(new vector2(j, j)));
                }
            }

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Fill: Test 0 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Fill: Test 1 - For Loop and Dict");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                watch.Stop();
                componentsDict.Clear();
                watch.Start();
                for (int j = 0; j < listSize; j++)
                {
                    vector2 pos = new vector2(j, j);
                    componentsDict.Add(pos, new testComponent(pos));
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Fill: Test 1 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Iteration: Test 2 - For Loop and List");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                for (int j = 0; j < componentsList.Count; j++)
                {
                    componentsList[j].update();
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Iteration: Test 2 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Iteration: Test 3 - For Loop and Dict");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                for (int j = 0; j < componentsDict.Count; j++)
                {
                    componentsDict[new vector2(j, j)].update();
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Iteration: Test 3 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Iteration: Test 4 - Foreach Loop and List");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                foreach (iComponent v in componentsList)
                {
                    v.update();
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Iteration: Test 4 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Iteration: Test 5 - Foreach Loop and Dict");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                foreach (KeyValuePair<vector2, testComponent> kvp in componentsDict)
                {
                    kvp.Value.update();
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Iteration: Test 5 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Find: Test 6 - List");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                for(int j = 0; j < componentsList.Count; j++)
                {
                    iComponent working = componentsList.Single(s => (s.pos.x == j));
                    working.update();
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Find: Test 6 - Took " + (elapsedMs / testsToAverage) + " ms");

            Console.WriteLine("Find: Test 7 - Dict");

            watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < testsToAverage; i++)
            {
                for (int j = 0; j < componentsDict.Count; j++)
                {
                    testComponent working;

                    if(componentsDict.TryGetValue(new vector2(j,j), out working))
                    {
                        working.update();
                    }
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Find: Test 7 - Took " + (elapsedMs / (testsToAverage) + " ms"));
            Console.ReadLine();
        }

        private class testComponent : iComponent
        {
            public vector2 pos;

            public testComponent(vector2 pos)
            {
                this.pos = pos;
            }

            public void start()
            {

            }

            public void stop()
            {

            }

            public void update()
            {

            }
        }
    }
}
