using System;
using System.Collections.Generic;
using FuzzySortOfIntervals.Data;

namespace FuzzySortOfIntervals.Sorters
{
    public class FuzzyIntervalSorter : Sorter
    {
        public void FuzzySort(List<Interval> intervals, int start, int end)
        {
            while (true)
            {
                if (start >= end) return;
                var intersection = FindIntersectionWithRandomPivot(intervals, start, end);
                var splitEndIndex = PartitionRight(intervals, intersection, start, end);
                var splitStartIndex = PartitionLeftMiddle(intervals, intersection, splitEndIndex, start, end);

                FuzzySort(intervals, start, splitStartIndex - 1);
                start = splitEndIndex + 1;
            }
        }

        private int PartitionRight(List<Interval> intervals, Interval intersection, int start, int end)
        {
            if (intervals == null || intersection == null || start > end || end < 0) return -1;
            var index = start - 1;

            for (var i = start; i <= end - 1; i++)
            {
                var current = intervals[i];
                if (current.GetStart().CompareTo(intersection.GetStart()) > 0) continue;
                index += 1;
                Swap(ref intervals, index, i);
            }

            Swap(ref intervals, index + 1, end);
            return index + 1;
        }

        private int PartitionLeftMiddle(List<Interval> intervals, Interval intersection, int right, int p, int end)
        {
            var index = p - 1;
            if (intervals == null || intersection == null || right == -1 || p > end || end > 0) return -1;

            for (var i = p; i <= right - 1; i++)
            {
                var current = intervals[i];
                if (current.GetEnd().CompareTo(intersection.GetEnd()) >= 0) continue;
                index += 1;
                Swap(ref intervals, index, i);
            }
            Swap(ref intervals, index + 1, right);
            return index + 1;
        }
    }
}