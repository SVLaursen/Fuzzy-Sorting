using System;
using System.Collections.Generic;

namespace FuzzySortOfIntervals.Data
{
    [System.Serializable]
    public struct AlgorithmResults
    {
        public AlgorithmResults(string name, long memoryBefore, long memoryAfter, long elapsedMilliseconds,
            Interval[] dataBefore, Interval[] dataAfter)
        {
            this.name = name;
            this.memoryAfter = memoryAfter;
            this.memoryBefore = memoryBefore;
            this.elapsedMilliseconds = elapsedMilliseconds;
            this.dataBefore = dataBefore;
            this.dataAfter = dataAfter;
        }
        
        public string name { get; set; }
        
        public long memoryBefore { get; set; }
        public long memoryAfter { get; set; }
        public long elapsedMilliseconds { get; set; }

        public Interval[] dataBefore { get; set; }
        public Interval[] dataAfter { get; set; }
    }
}