using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Z3;

namespace AOC2025
{
    public class Machine
    {
        public string initialState { get; private set; }

        public string goalState { get; private set; }

        public List<List<int>> Buttons { get; private set; }

        public List<int> goalIndicators { get; private set; }

        public Machine()
        {
            initialState = "";
            goalState = "";
            goalIndicators = new List<int>();
            Buttons = new List<List<int>>();
        }

        public Machine(string line) : this()
        {
            ParseState(line);
            ParseButtons(line);
            ParseIndicators(line);
        }
        private void ParseIndicators(string line)
        {
            int start = line.IndexOf('{');
            if (start == -1) return;

            int end = line.IndexOf('}', start);
            string content = line.Substring(start + 1, end - start - 1);

            if (string.IsNullOrWhiteSpace(content)) return;

            foreach (var n in content.Split(','))
            {
                goalIndicators.Add(int.Parse(n.Trim()));
            }
        }

        private void ParseState(string line)
        {
            int start = line.IndexOf('[');
            int end = line.IndexOf(']');

            string stateString = line.Substring(start + 1, end - start - 1);

            goalState = new string(stateString.Select(c => c == '#' ? '1' : '0').ToArray());
            initialState = new string('0', goalState.Length);

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
        public int CalculateButtonPresses()
        {            
            Queue<(string state,int count)> stateValues = new Queue<(string, int)>();

            stateValues.Enqueue((initialState,0));

            while (stateValues.Count > 0) 
            { 
                (string currentState,int currentCount) = stateValues.Dequeue();
                if (currentState.Equals(goalState)) return currentCount;

                for (int i = 0; i < Buttons.Count; i++)
                {
                    string newState = currentState;
                    foreach (var index in Buttons[i])
                    {
                        char[] stateArray = newState.ToCharArray();
                        stateArray[index] = stateArray[index] == '1' ? '0' : '1';
                        newState = new string(stateArray);
                    }
                    stateValues.Enqueue((newState, currentCount + 1));
                }
            }
            return -1;
        }
        public int CalculateIndicatorPressesZ3()
        {
            using (Context ctx = new Context())
            {
                int nButtons = Buttons.Count;
                int nIndicators = goalIndicators.Count;

                
                IntExpr[] x = new IntExpr[nButtons];
                for (int i = 0; i < nButtons; i++)
                {
                    x[i] = (IntExpr)ctx.MkIntConst($"x{i}");
                }

                Solver solver = ctx.MkSolver();

                
                foreach (var xi in x) solver.Assert(ctx.MkGe(xi, ctx.MkInt(0)));
                

                
                for (int ind = 0; ind < nIndicators; ind++)
                {
                    ArithExpr sum = ctx.MkInt(0);
                    for (int btn = 0; btn < nButtons; btn++)
                    {
                        if (Buttons[btn].Contains(ind))
                            sum = ctx.MkAdd(sum, x[btn]);
                    }
                    solver.Assert(ctx.MkEq(sum, ctx.MkInt(goalIndicators[ind])));
                }

                
                Optimize opt = ctx.MkOptimize();
                foreach (var c in solver.Assertions) opt.Assert(c);
                ArithExpr totalPresses = ctx.MkAdd(x);
                opt.MkMinimize(totalPresses);

                if (opt.Check() == Status.SATISFIABLE)
                {
                    Model model = opt.Model;
                    int total = 0;
                    for (int i = 0; i < nButtons; i++)
                    {
                        total += ((IntNum)model.Evaluate(x[i])).Int;
                    }
                    return total;
                }
                else
                {
                    return -1; 
                }
            }
        }


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
                totalPresses += machine.CalculateButtonPresses();
            }

            Console.WriteLine($"Fewest button presses required to configure the indicator lights: {totalPresses}");
        }
        public static void SolvePart2()
        {

            string path = "files\\data_2025_10.txt";
            var lines = File.ReadAllLines(path);
            long totalPresses = 0;

            foreach (var line in lines)
            {
                Machine machine = new Machine(line);
                totalPresses += machine.CalculateIndicatorPressesZ3();
            }

            Console.WriteLine($"Fewest button presses required to configure the indicator: {totalPresses}");
        }
            
    }

}
