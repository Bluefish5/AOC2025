using System;
using System.Diagnostics.Metrics;


class Program
{

    static void Main(string[] args)
    {

        var NAME_PROGRAM_TO_RUN = "2025_1_p2";

        var runner = new ProgramRunner();

        switch (NAME_PROGRAM_TO_RUN)
        {
            case "2025_1_p1":
                runner.program_2025_1_p1();
                break;

            case "2025_1_p2":
                runner.program_2025_1_p2();
                break;

            default:
                Console.WriteLine("Set up var to execute: NAME_PROGRAM_TO_RUN.");
                break;
        }
    }
}
class ProgramRunner
{
    public void program_2025_1_p1()
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

            arrowValue +=  (number * turn);

            arrowValue %= 100;
            if (arrowValue < 0) arrowValue += 100;

            if (arrowValue == 0) counter++;
        }

        System.Console.WriteLine("How many times 0 accured: " + counter);

    }

    public void program_2025_1_p2()
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

            for(int i = 0; i < number; i++)
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