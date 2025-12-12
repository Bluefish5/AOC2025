using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day7
    {
        public static void SolvePart1()
        {
            string path = "files\\data_2025_7.txt";
            var lines = File.ReadAllLines(path)
                            .Select(line => new StringBuilder(line))
                            .ToList();

            int totalBeamSplits = 0;

            int lengthOfBeam = lines[0].Length;
            int lenghtOfLines = lines.Count;

            var beamIndexes = new HashSet<int>();

            for (int i = 0; i < lenghtOfLines; i++)
            {

                if (i == 0)
                {
                    for (int j = 0; j < lengthOfBeam; j++)
                    {
                        if (lines[i][j] == 'S')
                        {
                            beamIndexes.Add(j);
                        }
                    }
                }
                else
                {
                    var nextBeams = new HashSet<int>();

                    for (int j = 0; j < lengthOfBeam; j++)
                    {
                        if (beamIndexes.Contains(j))
                        {
                            if (lines[i][j] == '^')
                            {
                                if (j - 1 >= 0)
                                {
                                    nextBeams.Add(j - 1);
                                    lines[i][j - 1] = '|';
                                }
                                if (j + 1 < lengthOfBeam)
                                {
                                    nextBeams.Add(j + 1);
                                    lines[i][j + 1] = '|';
                                }

                                totalBeamSplits++;

                            }
                            else
                            {
                                lines[i][j] = '|';
                                nextBeams.Add(j);
                            }
                        }
                    }
                    beamIndexes = nextBeams;
                }
            }
            System.Console.WriteLine("Total beam splits: " + totalBeamSplits);
        }

        public static void SolvePart1Visual() 
        {
            string path = "files\\data_2025_7.txt";
            var lines = File.ReadAllLines(path)
                            .Select(line => new StringBuilder(line))
                            .ToList();

            int totalBeamSplits = 0;

            int lengthOfBeam = lines[0].Length;
            int lenghtOfLines = lines.Count;

            var beamIndexes = new HashSet<int>();

            int step = 0;
            PrintState(lines, step);

            for (int i = 0; i < lenghtOfLines; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < lengthOfBeam; j++)
                        if (lines[i][j] == 'S')
                            beamIndexes.Add(j);
                }
                else
                {
                    var nextBeams = new HashSet<int>();

                    for (int j = 0; j < lengthOfBeam; j++)
                    {
                        if (beamIndexes.Contains(j))
                        {
                            if (lines[i][j] == '^')
                            {
                                if (j - 1 >= 0)
                                {
                                    nextBeams.Add(j - 1);
                                    lines[i][j - 1] = '|';
                                }
                                if (j + 1 < lengthOfBeam)
                                {
                                    nextBeams.Add(j + 1);
                                    lines[i][j + 1] = '|';
                                }

                                totalBeamSplits++;
                            }
                            else
                            {
                                lines[i][j] = '|';
                                nextBeams.Add(j);
                            }
                        }
                    }

                    beamIndexes = nextBeams;
                }

                PrintState(lines, ++step);
            }

            Console.WriteLine("Total beam splits: " + totalBeamSplits);
        }
        public static void PrintState(List<StringBuilder> board, int step)
        {
            Console.Clear();
            Console.WriteLine($"STEP: {step}");
            foreach (var line in board)
                Console.WriteLine(line.ToString());
            Thread.Sleep(20);
        }

        public static void SolvePart2() 
        {
            string path = "files\\data_2025_7.txt";
            var lines = File.ReadAllLines(path)
                            .Select(line => new StringBuilder(line))
                            .ToList();

            int lengthOfBeam = lines[0].Length;
            int lengthOfLines = lines.Count;

            Dictionary<int, long> beamPositions = new Dictionary<int, long>();

            for (int j = 0; j < lengthOfBeam; j++)
            {
                if (lines[0][j] == 'S')
                {
                    beamPositions[j] = 1;
                }
            }

            for (int i = 1; i < lengthOfLines; i++)
            {
                var nextBeamPositions = new Dictionary<int, long>();

                foreach (var pair in beamPositions)
                {
                    int index = pair.Key;
                    long value = pair.Value;

                    if (lines[i][index] == '^')
                    {
                        if (index - 1 >= 0)
                            nextBeamPositions[index - 1] = nextBeamPositions.GetValueOrDefault(index - 1, 0) + value;
                        if (index + 1 < lengthOfBeam)
                            nextBeamPositions[index + 1] = nextBeamPositions.GetValueOrDefault(index + 1, 0) + value;
                    }
                    else
                    {
                        nextBeamPositions[index] = nextBeamPositions.GetValueOrDefault(index, 0) + value;
                    }
                }

                beamPositions = nextBeamPositions;
            }

            long totalTimelines = beamPositions.Values.Sum();
            Console.WriteLine("Total timelines: " + totalTimelines);
        }
    }
}
