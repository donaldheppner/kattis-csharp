using System;
using System.IO;
using System.Linq;

namespace Kattis
{
    internal class Program
    {
        static int Main(string[] args)
        {
            return Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public static int Solve(Stream @in, Stream @out)
        {
            var reader = new StreamReader(@in);
            var writer = new StreamWriter(@out);

            var answer = reader.ReadLine()?.Split(' ').Last() ?? string.Empty;


            writer.WriteLine(answer);
            writer.Flush();

            return 0;
        }
    }
}
