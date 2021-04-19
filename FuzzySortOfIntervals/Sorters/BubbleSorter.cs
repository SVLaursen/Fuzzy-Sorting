using System;
using System.Collections.Generic;
using FuzzySortOfIntervals.Data;

namespace FuzzySortOfIntervals.Sorters
{
    public class BubbleSorter : Sorter
    {
        //O(N^2)
        public void Sort(List<Interval> intervals)
        {
            var N = intervals.Count - 1;

            for (var i = N; i > 0; i--)
            {
                var swapped = false;
                for (var j = 0; j < i; j++)
                {
                    if (intervals[j].Start <= intervals[j + 1].Start) continue;
                    Swap(ref intervals, j, j + 1);
                    swapped = true;
                }

                if (!swapped) break;
            }
        }
    }
}