using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day4
    {
        public static void SolvePart1()
        {
            long sumRollsOfPaper = 0;
            string path = "files\\data_2025_4.txt";

            var lines = File.ReadAllLines(path);

            int paperAround = 0;

            var matrix = new List<(int dx, int dy)>
            {
                (-1, -1),
                (-1,  0),
                (-1,  1),
                ( 0, -1),
                ( 0,  1),
                ( 1, -1),
                ( 1,  0),
                ( 1,  1)
            };

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    paperAround = 0;
                    if (lines[i][j] == '@')
                    {
                        foreach (var (dx, dy) in matrix)
                        {
                            int newX = i + dx;
                            int newY = j + dy;
                            if (newX >= 0 && newX < lines.Length && newY >= 0 && newY < lines[i].Length)
                            {
                                if (lines[newX][newY] == '@')
                                {
                                    paperAround++;
                                }
                            }
                        }
                        if (paperAround < 4) sumRollsOfPaper++;

                    }
                }
            }
            System.Console.WriteLine("Sum of all  rolls of paper that can be accessed by a forklift: " + sumRollsOfPaper);
        }
        public static void SolvePart2()
        {
            long totalPapers = 0;
            string path = "files\\data_2025_4.txt";

            var lines = File.ReadAllLines(path)
                       .Select(x => x.ToCharArray())
                       .ToArray();

            int paperAround = 0;
            bool paperRunOut = false;

            var matrix = new List<(int dx, int dy)>
            {
                (-1, -1),
                (-1,  0),
                (-1,  1),
                ( 0, -1),
                ( 0,  1),
                ( 1, -1),
                ( 1,  0),
                ( 1,  1)
            };

            while (!paperRunOut)
            {
                var toRemove = new List<(int x, int y)>();

                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        paperAround = 0;
                        if (lines[i][j] == '@')
                        {
                            int newX = 0;
                            int newY = 0;
                            foreach (var (dx, dy) in matrix)
                            {
                                newX = i + dx;
                                newY = j + dy;


                                if (newX >= 0 && newX < lines.Length && newY >= 0 && newY < lines[i].Length)
                                {
                                    if (lines[newX][newY] == '@') paperAround++;

                                }
                            }
                            if (paperAround < 4) toRemove.Add((i, j));


                        }
                    }
                }
                if (toRemove.Count == 0) paperRunOut = true;


                foreach (var (x, y) in toRemove)
                {
                    lines[x][y] = '.';
                }

                totalPapers += toRemove.Count;
            }

            System.Console.WriteLine("Sum of all  rolls of paper that can be accessed by a forklift: " + totalPapers);
        }
    }
    
}
