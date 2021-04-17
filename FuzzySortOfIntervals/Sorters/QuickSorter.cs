using System.Collections.Generic;
using FuzzySortOfIntervals.Data;

namespace FuzzySortOfIntervals.Sorters
{
    //O(N log N) Partition Sort
    public class QuickSorter : Sorter
    {
        public void Sort(List<Interval> intervals, int start, int end)
        {
            if (start >= end) return;
            var intersection = FindIntersectionWithRandomPivot(intervals, start, end);
            var splitStartIndex = Partition(intervals, intersection, start, end);
                
            Sort(intervals, start,splitStartIndex - 1);
            Sort(intervals, splitStartIndex + 1, end);
        }

        private int Partition(List<Interval> intervals, Interval intersection, int p, int end)
        {
            var index = p - 1;
            if (intervals == null || intersection == null || p > end || end < 0) return -1;

            for (var i = p; i < end; i++)
            {
                var current = intervals[i];
                if (current.GetStart().CompareTo(intersection.GetStart()) >= 0) continue;
                index += 1;
                Swap(ref intervals, index, i);
            }

            Swap(ref intervals, index + 1, end);
            return index + 1;
        }
    }
}