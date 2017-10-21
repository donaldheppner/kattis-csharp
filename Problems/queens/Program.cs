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

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            int numberOfQueens = int.Parse(reader.ReadLine());
            var xs = new HashSet<int>();
            var ys = new HashSet<int>();
            var nwses = new HashSet<int>(); // NW-SE diagonals
            var nesws = new HashSet<int>(); // NE-SW diagonals    

            string answer = "CORRECT";
            for (int i = 0; i < numberOfQueens; i++)
            {
                var queen = reader.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                if (!xs.Add(queen[0]) || !ys.Add(queen[1]) || !nwses.Add(queen[0] + queen[1]) || !nesws.Add(queen[0] - queen[1]))
                {
                    answer = "INCORRECT";
                    break;
                }
            }


            writer.WriteLine(answer);
            writer.Flush();
        }
    }
}
