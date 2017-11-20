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

        static bool isEven(int x)
        {
            return x % 2 == 0;
        }
        static bool isOdd(int x)
        {
            return !isEven(x);
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var answer = reader.ReadLine()?.Split(' ').Select(x=>int.Parse(x)).ToArray();
            var height = answer[0];
            var width = answer[1];

            int result;
            if (isEven(width) && isOdd(height))
            {
                if (width == 2 && height > 1) result = 0;
                else if (height > width) result = 0;
                else result = 2;
            }
            else if (isOdd(width) && isOdd(height)) result = 1;
            else result = 0;


            writer.WriteLine(result);
            writer.Flush();
        }
    }
}
