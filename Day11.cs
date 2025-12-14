using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day11
    {
        public static void SolvePart1()
        {
            string path = "files\\data_2025_11.txt";
            var lines = File.ReadAllLines(path);

            Dictionary<string,List<string>> dictionary = new Dictionary<string,List<string>>();

            int numberOfPaths = 0;

            foreach (var line in lines)
            {
                string[] parts = line.Split(':', 2);

                string key = parts[0].Trim();

                List<string> values = new List<string>();
                if (parts.Length > 1)
                {
                    values.AddRange(parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }

                dictionary.Add(key, values);
            }

            Queue<string> queueWord = new Queue<string>();

            queueWord.Enqueue("you");

            while (queueWord.Count > 0)
            {
                string currentWord = queueWord.Dequeue();
                if (currentWord == "out")
                {
                    numberOfPaths++;
                    continue;
                }
                if (dictionary.ContainsKey(currentWord))
                {
                    foreach (var nextWord in dictionary[currentWord])
                    {
                        queueWord.Enqueue(nextWord);
                    }
                }
            }
            Console.WriteLine($"Number of different paths lead to out: {numberOfPaths}");
        }

        public static void SolvePart2()
        {
            string path = "files\\data_2025_11.txt";
            var lines = File.ReadAllLines(path);

            // Tworzymy graf
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                string[] parts = line.Split(':', 2);
                string key = parts[0].Trim();
                List<string> values = new List<string>();
                if (parts.Length > 1)
                {
                    values.AddRange(parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }
                dictionary[key] = values;
            }

            // Memoizacja: klucz = (currentNode, visitedDac, visitedFft)
            Dictionary<(string, bool, bool), long> memo = new Dictionary<(string, bool, bool), long>();

            long DFS(string currentNode, bool visitedDac, bool visitedFft)
            {
                if (currentNode == "dac") visitedDac = true;
                if (currentNode == "fft") visitedFft = true;

                if (currentNode == "out")
                {
                    return (visitedDac && visitedFft) ? 1L : 0L;
                }

                var key = (currentNode, visitedDac, visitedFft);
                if (memo.ContainsKey(key)) return memo[key];

                long total = 0;
                if (dictionary.ContainsKey(currentNode))
                {
                    foreach (var next in dictionary[currentNode])
                    {
                        total += DFS(next, visitedDac, visitedFft);
                    }
                }

                memo[key] = total;
                return total;
            }

            long numberOfPaths = DFS("svr", false, false);
            Console.WriteLine($"Number of different paths lead to out: {numberOfPaths}");
        }


    }
}
