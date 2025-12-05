using System;
using System.Diagnostics.Metrics;


class Program
{

    static void Main(string[] args)
    {

        var NAME_PROGRAM_TO_RUN = "2025_5_p2";

        System.Console.WriteLine("Running program: " + NAME_PROGRAM_TO_RUN);

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

            case "2025_2_p2":
                runner.program_2025_2_p2();
                break;

            case "2025_3_p1":
                runner.program_2025_3_p1();
                break;

            case "2025_3_p2":
                runner.program_2025_3_p2();
                break;

            case "2025_4_p1":
                runner.program_2025_4_p1();
                break;

            case "2025_4_p2":
                runner.program_2025_4_p2();
                break;

            case "2025_5_p1":
                runner.program_2025_5_p1();
                break;

            case "2025_5_p2":
                runner.program_2025_5_p2();
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

            arrowValue += (number * turn);

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


                    if (isTwice) sum += i;
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
    public void program_2025_3_p1()
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

    public void program_2025_3_p2()
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
    public void program_2025_4_p1()
    {
        long sumRollsOfPaper = 0;
        string path = "files\\data_2025_4.txt";

        var lines = File.ReadAllLines(path);

        int paperAround = 0;

        var matrix = new List<(int dx, int dy)>
        {
            (-1, -1),
            (-1,  0),
            (-1,  1),
            ( 0, -1),
            ( 0,  1),
            ( 1, -1),
            ( 1,  0),
            ( 1,  1)
        };

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                paperAround = 0;
                if (lines[i][j] == '@')
                {
                    foreach (var (dx, dy) in matrix)
                    {
                        int newX = i + dx;
                        int newY = j + dy;
                        if (newX >= 0 && newX < lines.Length && newY >= 0 && newY < lines[i].Length)
                        {
                            if (lines[newX][newY] == '@')
                            {
                                paperAround++;
                            }
                        }
                    }
                    if (paperAround < 4) sumRollsOfPaper++;

                }
            }
        }
        System.Console.WriteLine("Sum of all  rolls of paper that can be accessed by a forklift: " + sumRollsOfPaper);
    }

    public void program_2025_4_p2()
    {
        long totalPapers = 0;
        string path = "files\\data_2025_4.txt";

        var lines = File.ReadAllLines(path)
                   .Select(x => x.ToCharArray())
                   .ToArray();

        int paperAround = 0;
        bool paperRunOut = false;

        var matrix = new List<(int dx, int dy)>
        {
            (-1, -1),
            (-1,  0),
            (-1,  1),
            ( 0, -1),
            ( 0,  1),
            ( 1, -1),
            ( 1,  0),
            ( 1,  1)
        };

        while (!paperRunOut)
        {
            var toRemove = new List<(int x, int y)>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    paperAround = 0;
                    if (lines[i][j] == '@')
                    {
                        int newX = 0;
                        int newY = 0;
                        foreach (var (dx, dy) in matrix)
                        {
                            newX = i + dx;
                            newY = j + dy;


                            if (newX >= 0 && newX < lines.Length && newY >= 0 && newY < lines[i].Length)
                            {
                                if (lines[newX][newY] == '@') paperAround++;

                            }
                        }
                        if (paperAround < 4) toRemove.Add((i, j));


                    }
                }
            }
            if (toRemove.Count == 0) paperRunOut = true;


            foreach (var (x, y) in toRemove)
            {
                lines[x][y] = '.';
            }

            totalPapers += toRemove.Count;
        }

        System.Console.WriteLine("Sum of all  rolls of paper that can be accessed by a forklift: " + totalPapers);
    }
    public void program_2025_5_p1()
    {
        int sumOfFresshNumbers = 0;
        string path = "files\\data_2025_5.txt";
        var lines = File.ReadAllLines(path);

        List<(long, long)> ranges = new List<(long, long)>();
        List<long> numbersToCheck = new List<long>();

        bool isRangeSection = true;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRangeSection = false;
            }
            else
            {
                if (isRangeSection)
                {

                    var parts = line.Split('-');
                    long start = long.Parse(parts[0]);
                    long end = long.Parse(parts[1]);
                    ranges.Add((start, end));
                }
                if (!isRangeSection)
                {
                    numbersToCheck.Add(long.Parse(line));
                }
            }

        }
        foreach (var number in numbersToCheck)
        {
            foreach (var (start, end) in ranges)
            {
                if (number >= start && number <= end)
                {
                    sumOfFresshNumbers++;
                    break;
                }
            }

        }
        System.Console.WriteLine("Sum of all fressh numbers: " + sumOfFresshNumbers);

    }
    public void program_2025_5_p2()
    {
        string path = "files\\data_2025_5.txt";
        var lines = File.ReadAllLines(path);

        List<(long start, long end)> ranges = new List<(long, long)>();
        

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) break;

            var parts = line.Split('-');
            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);
            ranges.Add((start, end));
        }
        ranges = ranges.OrderBy(r => r.start).ToList();

        long total = 0;
        long currentStart = ranges[0].start;
        long currentEnd = ranges[0].end;

        foreach (var (start, end) in ranges)
        {
            if (start <= currentEnd + 1) currentEnd = Math.Max(currentEnd, end);
            else
            {
                total += currentEnd - currentStart + 1;
                currentStart = start;
                currentEnd = end;
            }
        }
        total += currentEnd - currentStart + 1;

        Console.WriteLine("Total fresh IDs: " + total);
    }

}



