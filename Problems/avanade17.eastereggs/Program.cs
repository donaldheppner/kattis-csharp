using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kattis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public struct Point
        {
            public Point(int[] xy)
            {
                X = xy[0];
                Y = xy[1];
            }

            public int X;
            public int Y;
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var lineOne = reader.ReadLine()?.Split(' ').Select(x=>int.Parse(x)).ToArray();
            int numberEggs = lineOne[0];
            int numberBlue = lineOne[1];
            int numberRed = lineOne[2];

            List<Point> bluePoints = new List<Point>();
            for (int i = 0; i < numberBlue; i++)
            {
                bluePoints.Add(new Point(reader.ReadLine()?.Split(' ').Select(x => int.Parse(x)).ToArray()));
            }

            List<Point> redPoints = new List<Point>();
            for (int i = 0; i < numberRed; i++)
            {
                redPoints.Add(new Point(reader.ReadLine()?.Split(' ').Select(x => int.Parse(x)).ToArray()));
            }

            List<double> distances = new List<double>();
            foreach (var bp in bluePoints)
            {
                foreach (var rp in redPoints)
                {
                    var x = Math.Abs(bp.X - rp.X);
                    var y = Math.Abs(bp.Y - rp.Y);

                    distances.Add(Math.Sqrt((x * x) + (y * y)));
                }
            }

            distances.Sort();

            var result = distances.Count - numberEggs;

            writer.WriteLine("{0:F15}", distances[result]);
            writer.Flush();
        }
    }
}
