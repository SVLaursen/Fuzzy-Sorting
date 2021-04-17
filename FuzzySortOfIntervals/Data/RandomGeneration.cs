using System;
using System.Collections.Generic;

namespace FuzzySortOfIntervals.Data
{
    public static class RandomGeneration
    {
        private static readonly Random _random = new Random();
        
        public static List<Interval> GenerateRandomIntervals(int amount, int maxRange)
        {
            var result = new List<Interval>();

            for (var i = 0; i < amount; i++)
            {
                var start = GetRandomInteger(0, maxRange);
                var end = GetRandomInteger(start, maxRange + 1);
                var entry = new Interval(start, end, i);
                result.Add(entry);
            }

            return result;
        }

        public static void GenerateAndSaveToJSON(int amount, int maxRange)
        {
            var data = GenerateRandomIntervals(amount, maxRange);
            DataHandler.ExportDataObjectToJSON(data, "generation_data");
        }

        private static int GetRandomInteger(int start, int end)
        {
            return _random.Next(start, end);
        }
    }
}