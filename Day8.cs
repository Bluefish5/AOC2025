using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2025
{
    public class Point3D
    {
        public double X;
        public double Y;
        public double Z;
    }

    public class Connection
    {
        public int A; 
        public int B; 
        public double Distance;
    }
    class DSU
    {
        public int[] parent;
        public int[] size;

        public DSU(int n)
        {
            parent = new int[n];
            size = new int[n];

            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        public bool Union(int a, int b)
        {
            int pa = Find(a);
            int pb = Find(b);

            if (pa == pb) return false;

            if (size[pa] < size[pb])
            {
                (pa, pb) = (pb, pa);
            }

            parent[pb] = pa;
            size[pa] += size[pb];
            return true;
        }

        public int ComponentSize(int x)
        {
            return size[Find(x)];
        }
    }

    public static class Day8
    {
        
        public static void SolvePart1()
        {
            List<Point3D> points = new List<Point3D>();
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

            
            List<Connection> connections = new List<Connection>();

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    var p1 = points[i];
                    var p2 = points[j];

                    double dist = Math.Sqrt(
                        Math.Pow(p1.X - p2.X, 2) +
                        Math.Pow(p1.Y - p2.Y, 2) +
                        Math.Pow(p1.Z - p2.Z, 2));

                    connections.Add(new Connection
                    {
                        A = i,
                        B = j,
                        Distance = dist
                    });
                }
            }

            var shortest = connections.OrderBy(c => c.Distance)
                                      .Take(1000)
                                      .ToList();

            DSU dsu = new DSU(points.Count);

            foreach (var c in shortest)
            {
                dsu.Union(c.A, c.B);
            }

            Dictionary<int, int> componentSizes = new Dictionary<int, int>();

            for (int i = 0; i < points.Count; i++)
            {
                int root = dsu.Find(i);
                if (!componentSizes.ContainsKey(root))
                    componentSizes[root] = 0;

                componentSizes[root]++;
            }

            var largestThree = componentSizes.Values
                                             .OrderByDescending(x => x)
                                             .Take(3)
                                             .ToList();

            if (largestThree.Count < 3)
            {
                Console.WriteLine("ERROR: Less than 3 circuits detected!");
                return;
            }

            long result = (long)largestThree[0] * largestThree[1] * largestThree[2];

            Console.WriteLine("Largest circuits: " +
                              $"{largestThree[0]}, {largestThree[1]}, {largestThree[2]}");

            Console.WriteLine("Result = " + result);
        }

        public static void SolvePart2()
        {
            Console.WriteLine("Day 8 part 2 execution started.");

            string path = "files\\data_2025_8.txt";
            var lines = File.ReadAllLines(path);

            List<Point3D> points = new List<Point3D>();

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

            int n = points.Count;
            Console.WriteLine("Points count: " + n);

            List<Connection> connections = new List<Connection>();
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var p1 = points[i];
                    var p2 = points[j];
                    double dist = Math.Sqrt(
                        Math.Pow(p1.X - p2.X, 2) +
                        Math.Pow(p1.Y - p2.Y, 2) +
                        Math.Pow(p1.Z - p2.Z, 2));
                    connections.Add(new Connection { A = i, B = j, Distance = dist });
                }
            }


            connections = connections.OrderBy(c => c.Distance).ToList();

            DSU dsu = new DSU(n);
            int components = n;

            int lastA = -1, lastB = -1;
            bool found = false;

            foreach (var c in connections)
            {
                if (dsu.Union(c.A, c.B))
                {
                    components--;
                    if (components == 1)
                    {
                        lastA = c.A;
                        lastB = c.B;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Connection not Found!");
                return;
            }


            long x1 = (long)points[lastA].X;
            long x2 = (long)points[lastB].X;
            long productLong = x1 * x2;

            Console.WriteLine($"The last merge: point {lastA} ({points[lastA].X},{points[lastA].Y},{points[lastA].Z})");
            Console.WriteLine($"                point {lastB} ({points[lastB].X},{points[lastB].Y},{points[lastB].Z})");
            Console.WriteLine($"Product X: {x1} * {x2} = {productLong}");
        }
    }

    
}
