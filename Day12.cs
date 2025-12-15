using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day12
    {
        class MapToFill
        {
            public int x { get; set; }
            public int y { get; set; }
            public List<int> numberOfGrids { get; set; }
            public MapToFill(int X,int Y, List<int>NumberOfGrids)
            {
                x = X;
                y = Y;
                numberOfGrids = NumberOfGrids;
            }
        }

        public static void SolvePart1()
        {
            string path = "files\\data_2025_12.txt";
            var lines = File.ReadAllLines(path);

            Dictionary<int, char[,]> grids = new Dictionary<int, char[,]>();

            List<string> gridLines = new List<string>();

            List<MapToFill> mapsToFill = new List<MapToFill>();

            int isNextGrid = 0;

            foreach (var line in lines)
            {
                
                if (line.EndsWith(":"))
                {
                    isNextGrid = 3;
                    continue;
                }
                else
                {
                    if (line.Equals("")) continue;

                    else if (isNextGrid > 0)
                    {
                        isNextGrid--;
                        gridLines.Add(line);
                    }
                    else if (isNextGrid == 0 && gridLines.Count > 0)
                    {
                        int gridId = int.Parse(line.Split(':')[0]);
                        int size = gridLines.Count;
                        char[,] grid = new char[size, size];
                        gridLines
                            .Select((row, r) => (row, r))
                            .ToList()
                            .ForEach(tuple =>
                            {
                                for (int c = 0; c < size; c++)
                                    grid[tuple.r, c] = tuple.row[c];
                            });
                        grids[gridId] = grid;
                        gridLines.Clear();
                    }
                    else
                    {
                        var parts = line.Split(":");
                        parts[0] = parts[0].Trim();
                        parts[1] = parts[1].Trim();
                        var coords = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        List<int> numbers = coords.Select(int.Parse).ToList();
                        int x = int.Parse(coords[0]);
                        int y = int.Parse(coords[1]);
                        MapToFill map = new MapToFill(x, y, numbers);
                    }

                }
            }

            Console.WriteLine($"Result Part 1: ");
        }
        public static void SolvePart2()
        {

            string path = "files\\data_2025_10.txt";
            var lines = File.ReadAllLines(path);
            long result = 0;

            Console.WriteLine($"Result Part 2: {result}");
        }

    }
}
