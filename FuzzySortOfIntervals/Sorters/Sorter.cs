using System;
using System.Collections.Generic;
using FuzzySortOfIntervals.Data;

namespace FuzzySortOfIntervals.Sorters
{
    public class Sorter
    {
        private readonly Random _random = new Random();
        
        protected Interval FindIntersectionWithRandomPivot(List<Interval> intervals, int start, int end)
        {
            if (intervals == null) return null;

            var size = end - start + 1;
            var randomIndex = _random.Next(size) + start;
            var pivot = intervals[randomIndex];

            Swap(ref intervals, randomIndex, end);
            var intersection = new Interval(pivot.Start, pivot.End, 9999);

            for (var i = start; i <= end - 1; i++)
            {
                var current = intervals[i];
                
                if (current.End.CompareTo(intersection.Start) > 0 ||
                    current.Start.CompareTo(intersection.End) < 0) continue;
                
                if(current.Start.CompareTo(intersection.Start) > 0)
                    intersection.Start = current.Start;
                if(current.End.CompareTo(intersection.End) < 0)
                    intersection.End = current.End;
            }

            return intersection;
        }
        
        protected void Swap(ref List<Interval> intervals, int a, int b)
        {
            var temp = intervals[b];
            intervals[b] = intervals[a];
            intervals[a] = temp;
        }
    }
}