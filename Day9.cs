using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    
    public static class Day9
    {
        public static void SolvePart1()
        {
            string path = "files\\data_2025_9.txt";
            var lines = File.ReadAllLines(path);

            var points = new List<Point>();

            long maxArea = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                points.Add(new Point(x, y));
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

            var redPoints = new List<Point>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                redPoints.Add(new Point(x, y));
            }

            System.Console.WriteLine("Red Points count: " + redPoints.Count);

            var maxAreaDict = new Dictionary<(Point A, Point B), long>();

            for (int i = 0; i < redPoints.Count; i++)
            {
                for (int j = i + 1; j < redPoints.Count; j++)
                {
                    long dx = Math.Abs((long)redPoints[i].X - redPoints[j].X) + 1;
                    long dy = Math.Abs((long)redPoints[i].Y - redPoints[j].Y) + 1;

                    long area = dx * dy;
                    maxAreaDict[(redPoints[i], redPoints[j])] = area;

                }
            }
            maxAreaDict = maxAreaDict.OrderByDescending(c => c.Value).ToDictionary();

            (redPoints, maxAreaDict) = CompressTiles(redPoints, maxAreaDict);

            var edges = GetEdges(redPoints);

            var maxAreaResoult = FindLargestSquareInShape(redPoints,maxAreaDict, edges); 
            Console.WriteLine("Part 2: " + maxAreaResoult);


        }

        public static (List<Point> compressTiles,Dictionary<(Point, Point), long>) CompressTiles(List<Point> tiles,Dictionary<(Point, Point), long> squares)
        {
            var uniqueX = tiles.Select(t => t.X).Distinct().OrderBy(x => x);
            var uniqueY = tiles.Select(t => t.Y).Distinct().OrderBy(y => y);

            var xMap = uniqueX.Select((v, i) => new { v, i }).ToDictionary(c => c.v, c => c.i);
            var yMap = uniqueY.Select((v, i) => new { v, i }).ToDictionary(c => c.v, c => c.i);

            var newTiles = tiles.Select(t => new Point(xMap[t.X], yMap[t.Y])).ToList();

            var newSquares = squares.ToDictionary(c => (new Point(xMap[c.Key.Item1.X], yMap[c.Key.Item1.Y]),new Point(xMap[c.Key.Item2.X], yMap[c.Key.Item2.Y])),c => c.Value);

            return (newTiles, newSquares);
        }

        static public HashSet<Point> GetEdges(List<Point> redPoints)
        {
            System.Console.WriteLine($"Getting Redtiles:{redPoints.Count}");

            HashSet<Point> edges = new HashSet<Point>();
            for (int i = 0; i < redPoints.Count; i++)
            {
                var t1 = redPoints[i];
                var t2 = redPoints[(i + 1) % redPoints.Count];
                if (t1.X == t2.X)
                {
                    var minY = Math.Min(t1.Y, t2.Y);
                    var maxY = Math.Max(t1.Y, t2.Y);

                    for (int y = minY; y <= maxY; y++)
                    {
                        edges.Add(new Point(t1.X, y));
                    }
                }
                else if (t1.Y == t2.Y)
                {
                    var minX = Math.Min(t1.X, t2.X);
                    var maxX = Math.Max(t1.X, t2.X);

                    for (int x = minX; x <= maxX; x++)
                    {
                        edges.Add(new Point(x, t1.Y));
                    }
                }
            }
            return edges;
        }

        static bool TestSquare(List<Point> square, HashSet<Point> edges)
        {
            for (int i = 0; i < square.Count; i++)
            {
                var t1 = square[i];
                var t2 = square[(i + 1) % square.Count];

                var minY = Math.Min(t1.Y, t2.Y);
                var maxY = Math.Max(t1.Y, t2.Y);
                var minX = Math.Min(t1.X, t2.X);
                var maxX = Math.Max(t1.X, t2.X);

                for (int x = minX + 1; x < maxX; x++)
                {
                    if (edges.Contains(new Point(x, minY + 1)) || edges.Contains(new Point(x, maxY - 1)))
                        return false;
                }
                for (int y = minY + 1; y < maxY; y++)
                {
                    if (edges.Contains(new Point(minX + 1, y)) || edges.Contains(new Point(maxX - 1, y)))
                        return false;
                }
            }
            return true;
        }
        static long FindLargestSquareInShape(
            List<Point> tiles,
            Dictionary<(Point, Point), long> squares,
            HashSet<Point> edges)
        {

            foreach (var square in squares)
            {
                var p1 = square.Key.Item1;
                var p2 = square.Key.Item2;

                var testPoint1 = new Point(p1.X, p2.Y);
                var testPoint2 = new Point(p2.X, p1.Y);

                if (!TestSquare(new List<Point> { p1, p2 }, edges))
                    continue;

                return square.Value;
            }

            return 0L;
        }







    }
}
