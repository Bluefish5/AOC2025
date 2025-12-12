using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day3
    {
        public static void SolvePart1()
        {
            long sumOfJolts = 0;
            int maximumJolts = 0;
            string path = "files\\data_2025_3.txt";

            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length - 1; i++)
                {
                    for (int j = i + 1; j < line.Length; j++)
                    {
                        var number = (line[i] - '0') * 10 + (line[j] - '0');
                        if (number > maximumJolts)
                        {
                            maximumJolts = number;
                        }
                    }


                }
                sumOfJolts += maximumJolts;
                maximumJolts = 0;
            }
            Console.WriteLine("Sum of maximum jolts: " + sumOfJolts);
        }
        public static void SolvePart2()
        {
            long sum = 0;
            string path = "files\\data_2025_3.txt";

            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                int[] indexesOfBatterys = new int[12];
                int start = 0;

                for (int i = 0; i < 12; i++)
                {
                    int bestIndex = start;
                    int end = line.Length - (12 - i);

                    for (int j = start; j <= end; j++)
                    {
                        if (line[j] > line[bestIndex])
                        {
                            bestIndex = j;
                        }
                    }

                    indexesOfBatterys[i] = bestIndex;
                    start = bestIndex + 1;
                }

                long numberToAdd = 0;

                for (int i = 0; i < 12; i++)
                {
                    numberToAdd = numberToAdd * 10 + (line[indexesOfBatterys[i]] - '0');
                }

                sum += numberToAdd;
            }

            Console.WriteLine("Sum of all batteries: " + sum);
        }
    }
}
