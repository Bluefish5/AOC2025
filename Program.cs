using System;
using System.Diagnostics.Metrics;


class Program
{

    static void Main(string[] args)
    {

        var NAME_PROGRAM_TO_RUN = "2025_2_p1";

        var runner = new ProgramRunner();

        switch (NAME_PROGRAM_TO_RUN)
        {
            case "2025_1_p1":
                runner.program_2025_1_p1();
                break;

            case "2025_1_p2":
                runner.program_2025_1_p2();
                break;

            case "2025_2_p1":
                runner.program_2025_2_p1();
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
    public void program_2025_2_p1()
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

                    
                    if (isTwice)sum += i;
                }
            }
        }

        Console.WriteLine("Added up all invalid indexes: " + sum);
    }
    public void program_2025_2_p2()
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

                for (long numberToCheck = start; numberToCheck <= end; numberToCheck++)
                {
                    string stringNumber = numberToCheck.ToString();
                    int len = stringNumber.Length;

                    bool isPattern = false;

                    for (int step = 1; step <= len / 2; step++)
                    {
                        if (len % step != 0)continue;

                        bool matches = true;
                        int cursor = step;

                        while (cursor < len)
                        {
                            for (int j = 0; j < step; j++)
                            {
                                if (stringNumber[j] != stringNumber[cursor + j])
                                {
                                    matches = false;
                                    break;
                                }
                            }

                            if (!matches)break;
                            cursor += step;
                        }

                        if (matches)
                        {
                            isPattern = true;
                            break;
                        }
                    }

                    if (isPattern)
                        sum += numberToCheck;
                }
            }
        }
        Console.WriteLine("Added up all invalid indexes: " + sum);
    }




}