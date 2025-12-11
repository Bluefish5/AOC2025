using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2025
{
    class Program
    {

        static void Main(string[] args)
        {

            var NAME_PROGRAM_TO_RUN = "2025_8_p1";

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

                case "2025_6_p1":
                    runner.program_2025_6_p1();
                    break;

                case "2025_6_p2":
                    runner.program_2025_6_p2();
                    break;

                case "2025_7_p1":
                    runner.program_2025_7_p1();
                    break;

                case "2025_7_p1_visual":
                    runner.program_2025_7_p1_visual();
                    break;

                case "2025_7_p2":
                    runner.program_2025_7_p2();
                    break;

                case "2025_8_p1":
                    Day8.Execute();
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
        public void program_2025_6_p1()
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
        public void program_2025_6_p2()
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
        public void program_2025_7_p1()
        {
            string path = "files\\data_2025_7.txt";
            var lines = File.ReadAllLines(path)
                            .Select(line => new StringBuilder(line))
                            .ToList();

            int totalBeamSplits = 0;

            int lengthOfBeam = lines[0].Length;
            int lenghtOfLines = lines.Count;

            var beamIndexes = new HashSet<int>();

            for (int i = 0; i < lenghtOfLines; i++)
            {

                if (i == 0)
                {
                    for (int j = 0; j < lengthOfBeam; j++)
                    {
                        if (lines[i][j] == 'S')
                        {
                            beamIndexes.Add(j);
                        }
                    }
                }
                else
                {
                    var nextBeams = new HashSet<int>();

                    for (int j = 0; j < lengthOfBeam; j++)
                    {
                        if (beamIndexes.Contains(j))
                        {
                            if (lines[i][j] == '^')
                            {
                                if (j - 1 >= 0)
                                {
                                    nextBeams.Add(j - 1);
                                    lines[i][j - 1] = '|';
                                }
                                if (j + 1 < lengthOfBeam)
                                {
                                    nextBeams.Add(j + 1);
                                    lines[i][j + 1] = '|';
                                }

                                totalBeamSplits++;

                            }
                            else
                            {
                                lines[i][j] = '|';
                                nextBeams.Add(j);
                            }
                        }
                    }
                    beamIndexes = nextBeams;
                }
            }
            System.Console.WriteLine("Total beam splits: " + totalBeamSplits);
        }

        public void program_2025_7_p1_visual()
        {
            string path = "files\\data_2025_7.txt";
            var lines = File.ReadAllLines(path)
                            .Select(line => new StringBuilder(line))
                            .ToList();

            int totalBeamSplits = 0;

            int lengthOfBeam = lines[0].Length;
            int lenghtOfLines = lines.Count;

            var beamIndexes = new HashSet<int>();

            int step = 0;
            PrintState(lines, step);

            for (int i = 0; i < lenghtOfLines; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < lengthOfBeam; j++)
                        if (lines[i][j] == 'S')
                            beamIndexes.Add(j);
                }
                else
                {
                    var nextBeams = new HashSet<int>();

                    for (int j = 0; j < lengthOfBeam; j++)
                    {
                        if (beamIndexes.Contains(j))
                        {
                            if (lines[i][j] == '^')
                            {
                                if (j - 1 >= 0)
                                {
                                    nextBeams.Add(j - 1);
                                    lines[i][j - 1] = '|';
                                }
                                if (j + 1 < lengthOfBeam)
                                {
                                    nextBeams.Add(j + 1);
                                    lines[i][j + 1] = '|';
                                }

                                totalBeamSplits++;
                            }
                            else
                            {
                                lines[i][j] = '|';
                                nextBeams.Add(j);
                            }
                        }
                    }

                    beamIndexes = nextBeams;
                }

                PrintState(lines, ++step);
            }

            Console.WriteLine("Total beam splits: " + totalBeamSplits);
        }
        void PrintState(List<StringBuilder> board, int step)
        {
            Console.Clear();
            Console.WriteLine($"STEP: {step}");
            foreach (var line in board)
                Console.WriteLine(line.ToString());
            Thread.Sleep(20);
        }

        public void program_2025_7_p2()
        {
            string path = "files\\data_2025_7.txt";
            var lines = File.ReadAllLines(path)
                            .Select(line => new StringBuilder(line))
                            .ToList();

            int lengthOfBeam = lines[0].Length;
            int lengthOfLines = lines.Count;

            Dictionary<int, long> beamPositions = new Dictionary<int, long>();

            for (int j = 0; j < lengthOfBeam; j++)
            {
                if (lines[0][j] == 'S')
                {
                    beamPositions[j] = 1;
                }
            }

            for (int i = 1; i < lengthOfLines; i++)
            {
                var nextBeamPositions = new Dictionary<int, long>();

                foreach (var pair in beamPositions)
                {
                    int index = pair.Key;
                    long value = pair.Value;

                    if (lines[i][index] == '^')
                    {
                        if (index - 1 >= 0)
                            nextBeamPositions[index - 1] = nextBeamPositions.GetValueOrDefault(index - 1, 0) + value;
                        if (index + 1 < lengthOfBeam)
                            nextBeamPositions[index + 1] = nextBeamPositions.GetValueOrDefault(index + 1, 0) + value;
                    }
                    else
                    {
                        nextBeamPositions[index] = nextBeamPositions.GetValueOrDefault(index, 0) + value;
                    }
                }

                beamPositions = nextBeamPositions;
            }

            long totalTimelines = beamPositions.Values.Sum();
            Console.WriteLine("Total timelines: " + totalTimelines);
        }
        public class Point3D
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }
        public class Connection
        {
            public Point3D startPoint { get; set; }
            public Point3D endPoint { get; set; }

        }
        public void program_2025_8_p1()
        {
            List<Point3D> points = new List<Point3D>();
            List<List<Connection>> circuits = new List<List<Connection>>();

            string path = "files\\data_2025_8.txt";
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                points.Add(new Point3D
                {
                    X = double.Parse(parts[0]),
                    Y = double.Parse(parts[1]),
                    Z = double.Parse(parts[2])
                });
            }

            for (int l = 0; l < 1000; l++)
            {
                // --- znajdź najbliższą parę punktów ---
                double minDistance = double.MaxValue;
                Connection selectedConnection = null;

                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        double dx = points[i].X - points[j].X;
                        double dy = points[i].Y - points[j].Y;
                        double dz = points[i].Z - points[j].Z;
                        double distance = Math.Sqrt(dx * dx + dy * dy + dz * dz);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            selectedConnection = new Connection
                            {
                                startPoint = points[i],
                                endPoint = points[j]
                            };
                        }
                    }
                }

                if (selectedConnection == null) break;

                // --- dodaj połączenie do istniejącego circuitu lub stwórz nowy ---
                List<Connection> targetCircuit = null;
                foreach (var circuit in circuits)
                {
                    if (circuit.Any(c => SharesPoint(c, selectedConnection)))
                    {
                        targetCircuit = circuit;
                        break;
                    }
                }

                if (targetCircuit == null)
                {
                    circuits.Add(new List<Connection> { selectedConnection });
                }
                else
                {
                    targetCircuit.Add(selectedConnection);
                }

                // --- scal obwody powiązane ze sobą ---
                bool merged = true;
                while (merged)
                {
                    merged = false;
                    for (int i = 0; i < circuits.Count; i++)
                    {
                        for (int j = i + 1; j < circuits.Count; j++)
                        {
                            if (circuits[i].Any(a => circuits[j].Any(b => SharesPoint(a, b))))
                            {
                                circuits[i].AddRange(circuits[j]);
                                circuits.RemoveAt(j);
                                merged = true;
                                break;
                            }
                        }
                        if (merged) break;
                    }
                }
            }

            // --- policz rozmiary obwodów ---
            List<int> sizes = circuits.Select(c => c.Count + 1).ToList(); // +1 bo połączenia = punkty-1
            sizes.Sort();
            sizes.Reverse();

            long result = (long)sizes[0] * sizes[1] * sizes[2];
            Console.WriteLine("Value of multiply together three largest circuits: " + result);
        }

        private bool SharesPoint(Connection a, Connection b)
        {
            return PointsEqual(a.startPoint, b.startPoint) ||
                   PointsEqual(a.startPoint, b.endPoint) ||
                   PointsEqual(a.endPoint, b.startPoint) ||
                   PointsEqual(a.endPoint, b.endPoint);
        }

        private bool PointsEqual(Point3D a, Point3D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }


        public void program_2025_8_p2()
        {

        }
    }
}

