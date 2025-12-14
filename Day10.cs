using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public class Machine
    {
        public int State { get; private set; }
        public List<List<int>> Buttons { get; private set; }

        public Machine()
        {
            Buttons = new List<List<int>>();
        }

        public Machine(string line) : this()
        {
            ParseState(line);
            ParseButtons(line);
        }

        
        private void ParseState(string line)
        {
            int start = line.IndexOf('[');
            int end = line.IndexOf(']');

            string stateString = line.Substring(start + 1, end - start - 1);

            string binaryString = new string(stateString.Select(c => c == '#' ? '1' : '0').ToArray());

            State = Convert.ToInt32(binaryString, 2);

        }

        private void ParseButtons(string line)
        {
            int index = 0;

            while ((index = line.IndexOf('(', index)) != -1)
            {
                int end = line.IndexOf(')', index);

                string content = line.Substring(index + 1, end - index - 1);

                List<int> button = new List<int>();

                if (!string.IsNullOrWhiteSpace(content))
                {
                    string[] numbers = content.Split(',');

                    foreach (string number in numbers)
                    {
                        button.Add(int.Parse(number));
                    }
                }

                Buttons.Add(button);
                index = end + 1;
            }
        }
        //public int CalculateButtonPresses()
        //{

        //}
            

    }
    public static class Day10
    {
        public static void SolvePart1()
        {
            string path = "files\\data_2025_10.txt";
            var lines = File.ReadAllLines(path);
            long totalPresses = 0;


            foreach (var line in lines)
            {
                Machine machine = new Machine(line);
                //totalPresses += machine.CalculateButtonPresses();
            }

            Console.WriteLine($"Fewest button presses required to configure the indicator lights: {totalPresses}");
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
