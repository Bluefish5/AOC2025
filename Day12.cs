using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC2025
{
    public static class Day12
    {

        class MapToFill
        {
            public int Width { get; }
            public int Height { get; }
            public Dictionary<int, int> GridCounts { get; }

            public MapToFill(int w, int h, Dictionary<int, int> grids)
            {
                Width = w;
                Height = h;
                GridCounts = grids;
            }
        }

        class Shape
        {
            public List<(int r, int c)[]> Variants { get; } = new();
        }

        public static void SolvePart1()
        {
            string path = "files\\data_2025_12.txt";
            var lines = File.ReadAllLines(path);

            var grids = new Dictionary<int, char[,]>();
            var gridFilledCells = new Dictionary<int, int>();
            var shapes = new Dictionary<int, Shape>();
            var maps = new List<MapToFill>();

            ParseInput(lines, grids, gridFilledCells, shapes, maps);

            int totalPossibleAreas = 0;

            foreach (var map in maps)
            {
                int mapArea = map.Width * map.Height;
                int totalFigures = 0;
                int minCells = 0;

                foreach (var kvp in map.GridCounts)
                {
                    totalFigures += kvp.Value;
                    minCells += kvp.Value * gridFilledCells[kvp.Key];
                }

                if (totalFigures * 9 <= mapArea)
                {
                    totalPossibleAreas++;
                    continue;
                }

                if (minCells > mapArea)
                {
                    continue;
                }

                bool[,] board = new bool[map.Height, map.Width];
                var memo = new Dictionary<string, bool>();

                if (CanPack(board, map.GridCounts, shapes, memo))
                    totalPossibleAreas++;
            }

            Console.WriteLine($"Total possible areas: {totalPossibleAreas}");
        }

        static void ParseInput(
            string[] lines,
            Dictionary<int, char[,]> grids,
            Dictionary<int, int> gridFilledCells,
            Dictionary<int, Shape> shapes,
            List<MapToFill> maps)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                if (lines[i].EndsWith(":"))
                {
                    int gridId = int.Parse(lines[i].TrimEnd(':'));
                    List<string> gridLines = new();

                    i++;
                    while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
                    {
                        gridLines.Add(lines[i]);
                        i++;
                    }

                    char[,] grid = new char[3, 3];
                    int filled = 0;

                    for (int r = 0; r < 3; r++)
                        for (int c = 0; c < 3; c++)
                        {
                            grid[r, c] = gridLines[r][c];
                            if (grid[r, c] == '#') filled++;
                        }

                    grids[gridId] = grid;
                    gridFilledCells[gridId] = filled;
                    shapes[gridId] = BuildShape(grid);
                }
                else
                {
                    var parts = lines[i].Split(':', StringSplitOptions.RemoveEmptyEntries);

                    var values = parts[1]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList();

                    var sizes = parts[0]
                        .Split('x', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList();

                    int width = sizes[0];
                    int height = sizes[1];

                    Dictionary<int, int> gridCounts = new();
                    int index = 0;

                    foreach (var id in gridFilledCells.Keys.OrderBy(x => x))
                    {
                        gridCounts[id] = (index  < values.Count) ? values[index ] : 0;
                        index++;
                    }

                    maps.Add(new MapToFill(width, height, gridCounts));
                }
            }
        }

        static Shape BuildShape(char[,] grid)
        {
            Shape shape = new();
            char[,] current = grid;

            for (int i = 0; i < 4; i++)
            {
                var cells = new List<(int, int)>();

                for (int r = 0; r < 3; r++)
                    for (int c = 0; c < 3; c++)
                        if (current[r, c] == '#')
                            cells.Add((r, c));

                shape.Variants.Add(cells.ToArray());
                current = Rotate(current);
            }

            return shape;
        }

        static char[,] Rotate(char[,] g)
        {
            char[,] r = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    r[j, 2 - i] = g[i, j];
            return r;
        }

        

        static bool CanPack(
            bool[,] board,
            Dictionary<int, int> remaining,
            Dictionary<int, Shape> shapes,
            Dictionary<string, bool> memo)
        {
            string key = MakeKey(board, remaining);
            if (memo.TryGetValue(key, out bool cached))
                return cached;

            if (remaining.Values.All(v => v == 0))
                return memo[key] = true;

            var pos = FindFirstFree(board);
            if (pos.r == -1)
                return memo[key] = false;

            foreach (var kvp in remaining)
            {
                if (kvp.Value == 0) continue;

                foreach (var variant in shapes[kvp.Key].Variants)
                {
                    if (CanPlace(board, variant, pos.r, pos.c))
                    {
                        Place(board, variant, pos.r, pos.c, true);
                        remaining[kvp.Key]--;

                        if (CanPack(board, remaining, shapes, memo))
                            return memo[key] = true;

                        remaining[kvp.Key]++;
                        Place(board, variant, pos.r, pos.c, false);
                    }
                }
            }

            return memo[key] = false;
        }

        static (int r, int c) FindFirstFree(bool[,] board)
        {
            for (int r = 0; r < board.GetLength(0); r++)
                for (int c = 0; c < board.GetLength(1); c++)
                    if (!board[r, c])
                        return (r, c);
            return (-1, -1);
        }

        static bool CanPlace(bool[,] board, (int r, int c)[] shape, int r0, int c0)
        {
            foreach (var (dr, dc) in shape)
            {
                int r = r0 + dr;
                int c = c0 + dc;

                if (r < 0 || c < 0 ||
                    r >= board.GetLength(0) ||
                    c >= board.GetLength(1) ||
                    board[r, c])
                    return false;
            }
            return true;
        }

        static void Place(bool[,] board, (int r, int c)[] shape, int r0, int c0, bool value)
        {
            foreach (var (dr, dc) in shape)
                board[r0 + dr, c0 + dc] = value;
        }

        static string MakeKey(bool[,] board, Dictionary<int, int> remaining)
        {
            var sb = new StringBuilder();

            for (int r = 0; r < board.GetLength(0); r++)
                for (int c = 0; c < board.GetLength(1); c++)
                    sb.Append(board[r, c] ? '1' : '0');

            sb.Append('|');

            foreach (var kvp in remaining.OrderBy(x => x.Key))
                sb.Append($"{kvp.Key}:{kvp.Value},");

            return sb.ToString();
        }
    }
}
