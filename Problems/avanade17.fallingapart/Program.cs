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

            var numberOfPieces = int.Parse(reader.ReadLine());
            var pieceValues = reader.ReadLine()?.Split().Select(x => int.Parse(x)).ToList();
            pieceValues.Sort();
            pieceValues.Reverse();

            int alice = 0, bob = 0;
            for (int i = 0; i < pieceValues.Count; i++)
            {
                if (i % 2 == 0) alice += pieceValues[i];
                else bob += pieceValues[i];
            }
            
            writer.WriteLine("{0} {1}", alice, bob);
            writer.Flush();
        }
    }
}
