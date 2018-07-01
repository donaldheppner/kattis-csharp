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

            var numberOfCases = int.Parse(reader.ReadLine());
            for (int i = 0; i < numberOfCases; i++)
            {
                var numbers = reader.ReadLine().Split(' ').Select(s => double.Parse(s)).ToArray();
                var outerRing = numbers[0];
                var ballDiameter = numbers[1];
                var minimumDistance = numbers[2];

                double fatestRadius = (outerRing - ballDiameter) / 2.0;
                // double ballDiameterRadians = Math.Tan((ballDiameter + minimumDistance) / fatestRadius);
                double ballDiameterRadians = Math.Tan(ballDiameter / fatestRadius);
                double diameterDistance = fatestRadius * ballDiameterRadians;

                double fatestCircumference = (outerRing - ballDiameter) * Math.PI;//3.1415926535897932384626433832795m;

                // decimal answer = (long)Math.Floor(fatestCircumference / (diameterDistance));
                decimal answer = (long)Math.Floor(fatestCircumference / (diameterDistance + minimumDistance));
                writer.WriteLine(answer);
            }
            writer.Flush();
        }
    }
}
