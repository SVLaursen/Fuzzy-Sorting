using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static System.Int32;
using FuzzySortOfIntervals.Data;
using FuzzySortOfIntervals.Sorters;

namespace FuzzySortOfIntervals
{
    public static class Runtime
    {
        private static List<Interval> _items = new List<Interval>();
        
        public static void ChooseAlgorithm()
        {
            var active = true;

            while (active)
            {
                Console.WriteLine("Choose an algorithm or create some test data");
                Console.WriteLine("Enter one of the following options by writing in the command console");
                Console.WriteLine(">>> 1: Create Test Data");
                Console.WriteLine(">>> 2: Fuzzy Interval Sorting");
                Console.WriteLine(">>> 3: Quick Interval Sorting");
                Console.WriteLine(">>> 4: Bubble Interval Sorting");
                Console.WriteLine(">>> 5: Clear Memory (recommended between each use)");
                Console.WriteLine(">>> 6: Run All Three Algorithms!");
                Console.WriteLine(">>> 0: Exit Application");
            
                var input = Console.ReadLine();

                if (TryParse(input, out var number))
                {
                    if (number == 0)
                    {
                        Console.WriteLine("Exiting....");
                        active = false;
                        return;
                    }

                    switch (number)
                    {
                        case 1:
                            GenerateTestData();
                            break;
                        case 2:
                            PerformFuzzySort();
                            break;
                        case 3:
                            PerformQuickSort();
                            break;
                        case 4:
                            PerformBubbleSort();
                            break;
                        case 5:
                            ClearMemory();
                            break;
                        case 6:
                            RunAllThree();
                            break;
                        default:
                            Console.WriteLine("Invalid input, try again");
                            break;
                    }
                } else Console.WriteLine("Invalid input, try again");
            }
        }

        private static void ClearMemory()
        {
            Console.WriteLine("-> Clearing Memory...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("-> !!!! Cleared Unused Allocations !!!!");
        }

        private static void RunAllThree()
        {
            if (_items.Count <= 0)
            {
                Console.WriteLine("No test data found, please generate the data before running");
                return;
            }
            
            Console.WriteLine("-> RUNNING ALL SORTING ALGORITHMS!");
            var fuzzyThread = new Thread(PerformFuzzySortThreaded);
            var quickThread = new Thread(PerformQuickSortThreaded);
            var bubbleThread = new Thread(PerformBubbleSortThreaded);

            fuzzyThread.Start();
            quickThread.Start();
            bubbleThread.Start();

            fuzzyThread.Join();
            quickThread.Join();
            bubbleThread.Join();
            
            Console.WriteLine("-> !! SORTING COMPLETE !!");
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private static void GenerateTestData()
        {
            var active = true;
            while (active)
            {
                Console.WriteLine(">>> Please input the amount you want to generate (number)");
                Console.WriteLine(">>> Write 'cancel' to exit");
                var amountInput = Console.ReadLine();

                if (TryParse(amountInput, out var amountValue))
                {
                    Console.WriteLine(">>> Please input the max range of your intervals (number)");
                    Console.WriteLine(">>> Write 'cancel' to exit");
                    var rangeInput = Console.ReadLine();

                    if (TryParse(rangeInput, out var rangeValue))
                    {
                        _items = RandomGeneration.GenerateRandomIntervals(amountValue, rangeValue);
                        Console.WriteLine(">>> Data Generated!!");
                        Console.WriteLine("--------------------------------------------------------------------------------");
                        active = false;
                    }
                    else
                    {
                        if (rangeInput == "cancel")
                        {
                            active = false;
                            return;
                        }
                        Console.WriteLine("Invalid Input - please only use integer values");
                    }
                }
                else
                {
                    if (amountInput == "cancel")
                    {
                        active = false;
                        return;
                    }
                    Console.WriteLine("Invalid Input - please only use integer values");
                }
            }
        }

        private static void PerformFuzzySortThreaded() => PerformFuzzySort(true);

        private static void PerformQuickSortThreaded() => PerformQuickSort(true);

        private static void PerformBubbleSortThreaded() => PerformBubbleSort(true);
        
        private static void PerformFuzzySort(bool threaded = false)
        {
            Console.WriteLine(">>> Initializing Fuzzy Sort Algorithm <<<");
            var result = new AlgorithmResults();
            var items = _items.ToList();
            
            if (items.Count <= 0)
            {
                Console.WriteLine("No test data found, please generate the data before running");
                return;
            }
            
            Console.WriteLine("-> Test Data Loaded...");
            result.dataBefore = items.ToArray();
            
            var beforeMem = threaded ? GC.GetAllocatedBytesForCurrentThread() : GC.GetTotalMemory(false);
            var fuzzy = new FuzzyIntervalSorter();
            
            Console.WriteLine("-> Quick Sorter created...");
            
            var timer = Stopwatch.StartNew();
            Console.WriteLine("-> Running Sorting Algorithm...");
            
            fuzzy.FuzzySort(items, 0, items.Count - 1);
            timer.Stop();
            
            var afterMem = threaded ? GC.GetAllocatedBytesForCurrentThread() : GC.GetTotalMemory(false);

            result.memoryBefore = beforeMem;
            result.memoryAfter = afterMem;
            result.dataAfter = items.ToArray();
            result.name = $"FuzzySortWith{items.Count}Entries";
            result.elapsedMilliseconds = timer.ElapsedMilliseconds;
            
            DataHandler.ExportDataObjectToJSON(result, result.name);

            Console.WriteLine("!! Algorithm Complete !!");
            Console.WriteLine("-> Algorithm sorted {0} objects", items.Count);
            Console.WriteLine("-> The procedure took {0}ms", timer.ElapsedMilliseconds);
            Console.WriteLine("-> Results have been saved to your desktop with the filename: {0}", result.name);
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private static void PerformQuickSort(bool threaded = false)
        {
            Console.WriteLine(">>> Initializing Quick Sort Algorithm <<<");
            var result = new AlgorithmResults();
            var items = _items.ToList();

            if (items.Count <= 0)
            {
                Console.WriteLine("No test data found, please generate the data before running");
                return;
            }

            Console.WriteLine("-> Test Data Loaded...");
            result.dataBefore = items.ToArray();
            
            var beforeMem = threaded ? GC.GetAllocatedBytesForCurrentThread() : GC.GetTotalMemory(false);
            var quick = new QuickSorter();

            Console.WriteLine("-> Quick Sorter created...");

            var timer = Stopwatch.StartNew();
            
            Console.WriteLine("-> Running Sorting Algorithm...");

            quick.Sort(items, 0, items.Count - 1);
            timer.Stop();
            
            var afterMem = threaded ? GC.GetAllocatedBytesForCurrentThread() : GC.GetTotalMemory(false);

            result.memoryBefore = beforeMem;
            result.memoryAfter = afterMem;
            result.dataAfter = items.ToArray();
            result.name = $"QuickSortWith{items.Count}Entries";
            result.elapsedMilliseconds = timer.ElapsedMilliseconds;
            
            DataHandler.ExportDataObjectToJSON(result, result.name);

            Console.WriteLine("!! Algorithm Complete !!");
            Console.WriteLine("-> Algorithm sorted {0} objects", items.Count);
            Console.WriteLine("-> The procedure took {0}ms", timer.ElapsedMilliseconds);
            Console.WriteLine("-> Results have been saved to your desktop with the filename: {0}", result.name);
            Console.WriteLine("--------------------------------------------------------------------------------");
        }
        
        private static void PerformBubbleSort(bool threaded = false)
        {
            Console.WriteLine(">>> Initializing Bubble Sort Algorithm <<<");
            var result = new AlgorithmResults();
            var items = _items.ToList();
            
            if (items.Count <= 0)
            {
                Console.WriteLine("No test data found, please generate the data before running");
                return;
            }

            Console.WriteLine("-> Test Data Loaded...");
            result.dataBefore = items.ToArray();
            
            var beforeMem = threaded ? GC.GetAllocatedBytesForCurrentThread() : GC.GetTotalMemory(false);
            var bubble = new BubbleSorter();
            
            Console.WriteLine("-> BubbleSorter created...");
            
            var timer = Stopwatch.StartNew();
            
            Console.WriteLine("-> Running Sorting Algorithm...");
            
            bubble.Sort(items);
            timer.Stop();
            
            var afterMem = threaded ? GC.GetAllocatedBytesForCurrentThread() : GC.GetTotalMemory(false);

            result.memoryBefore = beforeMem;
            result.memoryAfter = afterMem;
            result.dataAfter = items.ToArray();
            result.name = $"BubbleSortWith{items.Count}Entries";
            result.elapsedMilliseconds = timer.ElapsedMilliseconds;
            
            DataHandler.ExportDataObjectToJSON(result, result.name);

            Console.WriteLine("!! Algorithm Complete !!");
            Console.WriteLine("-> Algorithm sorted {0} objects", items.Count);
            Console.WriteLine("-> The procedure took {0}ms", timer.ElapsedMilliseconds);
            Console.WriteLine("-> Results have been saved to your desktop with the filename: {0}", result.name);
            Console.WriteLine("--------------------------------------------------------------------------------");
        }
    }
}