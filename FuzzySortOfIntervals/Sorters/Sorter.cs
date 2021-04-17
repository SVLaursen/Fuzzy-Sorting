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
            var intersection = new Interval(pivot.GetStart(), pivot.GetEnd(), 9999);

            for (var i = start; i <= end - 1; i++)
            {
                var current = intervals[i];
                
                if (current.GetEnd().CompareTo(intersection.GetStart()) > 0 ||
                    current.GetStart().CompareTo(intersection.GetEnd()) < 0) continue;
                
                if(current.GetStart().CompareTo((intersection.GetStart())) > 0)
                    intersection.SetStart(current.GetStart());
                if(current.GetEnd().CompareTo(intersection.GetEnd()) < 0)
                    intersection.SetEnd(current.GetEnd());
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