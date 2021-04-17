using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FuzzySortOfIntervals.Data
{
    public static class DataHandler
    {
        public static void ExportDataObjectToJSON<T>(T data, string filename)
        {
            var jsonOptions = new JsonSerializerOptions {WriteIndented = true};
            var json = JsonSerializer.Serialize<T>(data, jsonOptions);
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.WriteAllText($"{filePath}/{filename}.json", json);
        }

        public static List<Interval> LoadIntervalDataFromFile(string filename)
        {
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            using var reader = new StreamReader($"{filePath}/{filename}.json");
            var json = reader.ReadToEnd();
            var items = JsonSerializer.Deserialize<List<Interval>>(json);
            reader.Close();
            return items;
        }
    }
}