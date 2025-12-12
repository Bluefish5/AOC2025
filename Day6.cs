using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day6
    {
        public static void SolvePart1()
        {
            string path = "files\\data_2025_6.txt";
            var lines = File.ReadAllLines(path);

            List<List<long>> numbersInColumns = new List<List<long>>();
            List<string> operators = new List<string>();

            long totalSum = 0;
            long tmpSum = 0;

            for (int i = 0; i < 4; i++)
            {
                List<long> numbers = lines[i]
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToList();

                numbersInColumns.Add(numbers);
            }
            operators = lines[4]
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

            for (int i = 0; i < numbersInColumns[0].Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (operators[i] == "+")
                    {
                        tmpSum += numbersInColumns[j][i];
                    }
                    else if (operators[i] == "*")
                    {
                        if (j == 0) tmpSum = numbersInColumns[j][i];
                        else tmpSum *= numbersInColumns[j][i];
                    }
                }
                totalSum += tmpSum;
                tmpSum = 0;
            }
            Console.WriteLine("Total sum: " + totalSum);
        }
        public static void SolvePart2()
        {
            string path = "files\\data_2025_6.txt";
            var lines = File.ReadAllLines(path);

            List<string> operators = new List<string>();

            double totalSum = 0;

            string tmpStringNumber = "";

            int cursorOperator = 0;

            List<int> numbers = new List<int>();

            operators = lines[4]
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

            operators.Reverse();

            for (int i = lines[0].Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    tmpStringNumber += lines[j][i];
                }
                if (!tmpStringNumber.All(c => c == ' '))
                {
                    numbers.Add(int.Parse(tmpStringNumber.Trim()));
                }
                else
                {
                    if (operators[cursorOperator] == "+")
                    {
                        foreach (var number in numbers)
                        {
                            totalSum += number;
                        }
                    }
                    else if (operators[cursorOperator] == "*")
                    {
                        double tmpProduct = 1;
                        foreach (var number in numbers)
                        {
                            tmpProduct *= number;
                        }
                        totalSum += tmpProduct;
                    }
                    cursorOperator++;
                    numbers.Clear();
                }

                tmpStringNumber = "";
            }
            if (numbers.Count > 0)
            {
                if (operators[cursorOperator] == "+")
                {
                    foreach (var number in numbers)
                    {
                        totalSum += number;
                    }
                }
                else if (operators[cursorOperator] == "*")
                {
                    double tmpProduct = 1;
                    foreach (var number in numbers)
                    {
                        tmpProduct *= number;
                    }
                    totalSum += tmpProduct;
                }
            }
            Console.WriteLine("Total sum: " + totalSum);
        }
    }

}
