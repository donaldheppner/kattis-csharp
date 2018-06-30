using System;
using System.Diagnostics;
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

        private static int GetRankStars(int rank)
        {
            if (rank > 20) return 2;
            if (rank > 15) return 3;
            if (rank > 10) return 4;
            return 5;
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var sequence = reader.ReadLine();

            int run = 0;
            int rank = 25;
            int stars = 0;

            foreach (char c in sequence)
            {
                if (c == 'W')
                {
                    run++;
                    int newStars = run > 2 && rank > 5 ? 2 : 1;

                    int nextRankStars = stars + newStars - GetRankStars(rank);
                    if (nextRankStars > 0)
                    {
                        rank--; // move up a rank
                        stars = nextRankStars;  // add 1-2 stars at that rank
                    }
                    else
                    {
                        stars += newStars;
                    }
                }
                else
                {
                    run = 0;
                    if (rank < 20)
                    {
                        if (stars > 0) stars--;
                        else
                        {
                            rank++;
                            stars = GetRankStars(rank) - 1;
                        }
                    }
                    else if(rank == 20)
                    {
                        stars = Math.Max(0, stars - 1);
                    }
                }

                if (rank == 0) break;
            }

            writer.WriteLine(rank == 0 ? "Legend" : rank.ToString());
            writer.Flush();
        }
    }
}
