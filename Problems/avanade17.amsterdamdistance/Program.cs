using System;
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

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var lineOne = reader.ReadLine()?.Split(' ');
            int numberSegments = int.Parse(lineOne[0]);
            int numberRings = int.Parse(lineOne[1]);
            double radius = double.Parse(lineOne[2]);

            var lineTwo = reader.ReadLine()?.Split(' ');
            int ax = int.Parse(lineTwo[0]);
            int ay = int.Parse(lineTwo[1]);
            int bx = int.Parse(lineTwo[2]);
            int by = int.Parse(lineTwo[3]);


            if (by + ay == 0)
            {
                writer.WriteLine(0);
            }
            else
            {
                int xDeltaUnits = Math.Abs(ax - bx);
                int yDeltaUnits = Math.Abs(ay - by);

                // traverse curve on min y
                double minY = Math.Min(ay, by);
                double sliceDistance = Math.PI * radius * (minY / numberRings) * (1 / (double)numberSegments);
                double ringDistance = radius / numberRings;

                // ring traversal result
                double ringResult = (ringDistance * yDeltaUnits) + (sliceDistance * xDeltaUnits);
                // back to center traversal result
                double centerResult = (ringDistance * ay) + (ringDistance * by);

                writer.WriteLine(Math.Min(ringResult, centerResult));
            }
            writer.Flush();
        }
    }
}
