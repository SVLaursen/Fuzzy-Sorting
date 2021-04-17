using System;
using System.Collections.Generic;

namespace FuzzySortOfIntervals.Data
{
    [Serializable]
    public class Interval
    {
        private int _start;
        private int _end;
        private int _number;

        public int Start => _start;
        public int End => _end;
        public int Number => _number;
        
        public Interval() {}

        public Interval(int start, int end, int number)
        {
            _start = start;
            _end = end;
            _number = number;
        }

        public int GetStart() => _start;

        public int GetEnd() => _end;

        public int GetNumber() => _number;

        public void SetStart(int value) => _start = value;

        public void SetEnd(int value) => _end = value;
    }
}