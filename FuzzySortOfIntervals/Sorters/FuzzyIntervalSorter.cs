using System;
using System.Collections.Generic;
using FuzzySortOfIntervals.Data;

namespace FuzzySortOfIntervals.Sorters
{
    public class FuzzyIntervalSorter : Sorter
    {
        public void FuzzySort(List<Interval> intervals, int start, int end)
        {
            if (start >= end) return;
            var intersection = FindIntersectionWithRandomPivot(intervals, start, end);
            var splitEndIndex = PartitionRight(intervals, intersection, start, end);
            var splitStartIndex = PartitionLeftMiddle(intervals, intersection, splitEndIndex, start, end);

            FuzzySort(intervals, start, splitStartIndex - 1);
            FuzzySort(intervals, splitEndIndex + 1, end);
        }

        private int PartitionRight(List<Interval> intervals, Interval intersection, int start, int end)
        {
            if (intervals == null || intersection == null || start > end || end < 0) return -1;
            var index = start - 1;

            for (var i = start; i <= end - 1; i++)
            {
                var current = intervals[i];
                if (current.Start.CompareTo(intersection.Start) > 0) continue;
                index += 1;
                Swap(ref intervals, index, i);
            }

            Swap(ref intervals, index + 1, end);
            return index + 1;
        }

        private int PartitionLeftMiddle(List<Interval> intervals, Interval intersection, int r, int p, int end)
        {
            var index = p - 1;
            if (intervals == null || intersection == null || r == -1 || p > end || end > 0) return -1;

            for (var i = p; i <= r - 1; i++)
            {
                var current = intervals[i];
                if (current.End.CompareTo(intersection.End) >= 0) continue;
                index += 1;
                Swap(ref intervals, index, i);
            }
            Swap(ref intervals, index + 1, r);
            return index + 1;
        }
    }
}