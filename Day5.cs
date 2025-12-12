using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day5
    {
        public static void SolvePart1()
        {
            int sumOfFresshNumbers = 0;
            string path = "files\\data_2025_5.txt";
            var lines = File.ReadAllLines(path);

            List<(long, long)> ranges = new List<(long, long)>();
            List<long> numbersToCheck = new List<long>();

            bool isRangeSection = true;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isRangeSection = false;
                }
                else
                {
                    if (isRangeSection)
                    {

                        var parts = line.Split('-');
                        long start = long.Parse(parts[0]);
                        long end = long.Parse(parts[1]);
                        ranges.Add((start, end));
                    }
                    if (!isRangeSection)
                    {
                        numbersToCheck.Add(long.Parse(line));
                    }
                }

            }
            foreach (var number in numbersToCheck)
            {
                foreach (var (start, end) in ranges)
                {
                    if (number >= start && number <= end)
                    {
                        sumOfFresshNumbers++;
                        break;
                    }
                }

            }
            System.Console.WriteLine("Sum of all fressh numbers: " + sumOfFresshNumbers);
        }
        public static void SolvePart2()
        {
            string path = "files\\data_2025_5.txt";
            var lines = File.ReadAllLines(path);

            List<(long start, long end)> ranges = new List<(long, long)>();


            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) break;

                var parts = line.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);
                ranges.Add((start, end));
            }
            ranges = ranges.OrderBy(r => r.start).ToList();

            long total = 0;
            long currentStart = ranges[0].start;
            long currentEnd = ranges[0].end;

            foreach (var (start, end) in ranges)
            {
                if (start <= currentEnd + 1) currentEnd = Math.Max(currentEnd, end);
                else
                {
                    total += currentEnd - currentStart + 1;
                    currentStart = start;
                    currentEnd = end;
                }
            }
            total += currentEnd - currentStart + 1;

            Console.WriteLine("Total fresh IDs: " + total);
        }
    }
}
