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
            var sw = System.Diagnostics.Stopwatch.StartNew();

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

            // =============================================
            // 1. GREEN POINTS ON SEGMENTS
            // =============================================
            Console.WriteLine("ETAP 1: Generowanie zielonych punktów na segmentach...");

            var green = new HashSet<(long x, long y)>();
            var red = new HashSet<(long x, long y)>();

            foreach (var p in points)
                red.Add((p.X, p.Y));

            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                var a = points[i];
                var b = points[j];

                if (a.X == b.X)
                {
                    long x = a.X;
                    long y1 = Math.Min(a.Y, b.Y);
                    long y2 = Math.Max(a.Y, b.Y);

                    for (long y = y1; y <= y2; y++)
                        green.Add((x, y));
                }
                else // horizontal
                {
                    long y = a.Y;
                    long x1 = Math.Min(a.X, b.X);
                    long x2 = Math.Max(a.X, b.X);

                    for (long x = x1; x <= x2; x++)
                        green.Add((x, y));
                }
            }
            Console.WriteLine("ETAP 1 zakończony.\n");

            // =============================================
            // 2. FILL INTERIOR USING FAST SCANLINE
            // =============================================
            Console.WriteLine("ETAP 2: Wypełnianie wnętrza pętli (scanline fill)...");

            long minX = points.Min(p => p.X);
            long maxX = points.Max(p => p.X);
            long minY = points.Min(p => p.Y);
            long maxY = points.Max(p => p.Y);

            int lastPercent = -1;

            for (long y = minY; y <= maxY; y++)
            {
                // znajdź wszystkie zielone punkty w tym wierszu
                var xs = green.Where(p => p.y == y).Select(p => p.x).OrderBy(x => x).ToList();
                if (xs.Count < 2) continue;

                for (int k = 0; k < xs.Count - 1; k += 2) // pary start-stop
                {
                    long startX = xs[k];
                    long endX = xs[k + 1];

                    for (long x = startX; x <= endX; x++)
                        green.Add((x, y));
                }

                // postęp
                int percent = (int)((y - minY) * 100 / (maxY - minY + 1));
                if (percent != lastPercent)
                {
                    Console.Write($"\r  progress: {percent}%   ");
                    lastPercent = percent;
                }
            }
            Console.WriteLine("\nETAP 2 zakończony.\n");

            // =============================================
            // 3. CHECK RECTANGLES
            // =============================================
            Console.WriteLine("ETAP 3: Sprawdzanie prostokątów...");

            long totalRects = (long)points.Count * (points.Count - 1) / 2;
            long rectDone = 0;
            lastPercent = -1;

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    rectDone++;
                    int percent = (int)(rectDone * 100 / totalRects);
                    if (percent != lastPercent)
                    {
                        Console.Write($"\r  progress: {percent}%   ");
                        lastPercent = percent;
                    }

                    var a = points[i];
                    var b = points[j];

                    long left = Math.Min(a.X, b.X);
                    long right = Math.Max(a.X, b.X);
                    long bottom = Math.Min(a.Y, b.Y);
                    long top = Math.Max(a.Y, b.Y);

                    bool ok = true;

                    for (long xCheck = left; xCheck <= right && ok; xCheck++)
                    {
                        for (long yCheck = bottom; yCheck <= top && ok; yCheck++)
                        {
                            if (!green.Contains((xCheck, yCheck)) && !red.Contains((xCheck, yCheck)))
                            {
                                ok = false;
                                break;
                            }
                        }
                    }

                    if (ok)
                    {
                        long area = (right - left + 1) * (top - bottom + 1);
                        if (area > maxArea)
                            maxArea = area;
                    }
                }
            }
            Console.WriteLine("\nETAP 3 zakończony.\n");

            Console.WriteLine($"Max Area Part2: {maxArea}");
            sw.Stop();
            Console.WriteLine($"Czas wykonania: {sw.Elapsed}");
        }


    }
}
