using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day2
    {
        public static void SolvePart1()
        {
            long sum = 0;
            string path = "files\\data_2025_2.txt";

            var lines = File.ReadAllLines(path);
            List<string[]> rows = new List<string[]>();

            foreach (string line in lines)
            {
                rows.Add(line.Split(','));
            }

            foreach (string[] row in rows)
            {
                foreach (string range in row)
                {
                    string[] values = range.Split('-');
                    long start = long.Parse(values[0]);
                    long end = long.Parse(values[1]);

                    for (long i = start; i <= end; i++)
                    {
                        string s = i.ToString();
                        int len = s.Length;

                        if (len % 2 != 0)
                            continue;

                        bool isTwice = true;

                        for (int j = 0; j < len / 2; j++)
                        {
                            if (s[j] != s[j + len / 2])
                            {
                                isTwice = false;
                                break;
                            }
                        }


                        if (isTwice) sum += i;
                    }
                }
            }

            Console.WriteLine("Added up all invalid indexes: " + sum);
        }
        public static void SolvePart2()
        {
            long sum = 0;
            string path = "files\\data_2025_2.txt";

            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                foreach (string range in line.Split(','))
                {
                    if (string.IsNullOrWhiteSpace(range)) continue;

                    string[] values = range.Split('-');
                    long start = long.Parse(values[0]);
                    long end = long.Parse(values[1]);

                    for (long numberToCheck = start; numberToCheck <= end; numberToCheck++)
                    {
                        string stringNumber = numberToCheck.ToString();
                        int len = stringNumber.Length;

                        bool isPattern = false;

                        for (int step = 1; step <= len / 2; step++)
                        {
                            if (len % step != 0) continue;
                            if (len / step < 2) continue;

                            bool patternFound = true;

                            for (int i = step; i < len; i++)
                            {
                                if (stringNumber[i] != stringNumber[i - step])
                                {
                                    patternFound = false;
                                    break;
                                }
                            }
                            if (patternFound)
                            {
                                isPattern = true;
                                break;
                            }
                        }
                        if (isPattern) sum += numberToCheck;
                    }
                }
            }

            Console.WriteLine("Added up all invalid indexes: " + sum);
        }
    }
}
