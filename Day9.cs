using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    class Point2D
    {
        public long X { get; set; }
        public long Y { get; set; }
        public Point2D(long x, long y)
        {
            X = x;
            Y = y;
        }
    }
    public static class Day9
    {
        public static void SolvePart1()
        {
            string path = "files\\data_2025_9.txt";
            var lines = File.ReadAllLines(path);

            var points = new List<Point2D>();

            long maxArea = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue; 

                var parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                points.Add(new Point2D(x, y));
            }

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    long dx = Math.Abs((long)points[i].X - points[j].X) + 1;
                    long dy = Math.Abs((long)points[i].Y - points[j].Y) + 1;

                    long area = dx * dy;
                    if (area > maxArea) maxArea = area;
                }
            }

            Console.WriteLine($"Max Area: {maxArea}");
        }

        public static void SolvePart2()
        {
            string path = "files\\data_2025_9.txt";
            var lines = File.ReadAllLines(path);

            var points = new List<Point2D>();

            long maxArea = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                points.Add(new Point2D(x, y));
            }


        }
    }
}
