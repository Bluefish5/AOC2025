using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public static class Day1
    {
        public static void SolvePart1()
        {
            int arrowValue = 50;
            int turn = 1;
            int counter = 0;
            int number = 0;

            string path = "files\\data_2025_1.txt";
            var lines = File.ReadAllLines(path);


            foreach (var line in lines)
            {
                number = 0;

                turn = (line[0] == 'R') ? 1 : -1;

                for (int i = 1; i < line.Length; i++) number = number * 10 + (line[i] - '0');

                arrowValue += (number * turn);

                arrowValue %= 100;
                if (arrowValue < 0) arrowValue += 100;

                if (arrowValue == 0) counter++;
            }

            System.Console.WriteLine("How many times 0 accured: " + counter);
        }
        public static void SolvePart2()
        {
            int arrowValue = 50;
            int turn = 1;
            int counter = 0;
            int number = 0;

            string path = "files\\data_2025_1.txt";
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                number = 0;

                turn = (line[0] == 'R') ? 1 : -1;

                for (int i = 1; i < line.Length; i++) number = number * 10 + (line[i] - '0');

                for (int i = 0; i < number; i++)
                {
                    arrowValue += turn;
                    arrowValue %= 100;
                    if (arrowValue < 0) arrowValue += 100;
                    if (arrowValue == 0) counter++;
                }

            }
            System.Console.WriteLine("How many times 0 accured: " + counter);
        }
    }
}
