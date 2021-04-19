using System;
using System.Collections.Generic;

namespace FuzzySortOfIntervals.Data
{
    [Serializable]
    public class Interval
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Number { get; set; }
        
        public Interval(int start, int end, int number)
        {
            Start = start;
            End = end;
            Number = number;
        }
    }
}