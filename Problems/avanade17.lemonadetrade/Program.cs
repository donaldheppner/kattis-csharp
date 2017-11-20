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

        public class Trader
        {
            public string Offering;
            public string Wanting;
            public double Ratio;

            public Trader(string offering, string wanting, double ratio)
            {
                Offering = offering;
                Wanting = wanting;
                Ratio = ratio;
            }
        }


        private static double GetBlue(List<Trader> traders, string product, double amount)
        {
            Dictionary<string, double> forks = new Dictionary<string, double>();
            forks.Add(product, Math.Log(amount));
            double maxBlue = double.MinValue;
            bool hasBlue = false;

            foreach (var trader in traders)
            {
                double a;
                if (forks.TryGetValue(trader.Wanting, out a))
                {
                    double newAmount = a + Math.Log(trader.Ratio);

                    double oldAmount;
                    if (forks.TryGetValue(trader.Offering, out oldAmount))
                    {
                        forks[trader.Offering] = Math.Max(oldAmount, newAmount);
                    }
                    else
                    {
                        forks.Add(trader.Offering, newAmount);
                    }

                    if (trader.Offering == "blue")
                    {
                        hasBlue = true;
                        maxBlue = Math.Max(newAmount, maxBlue);
                        if (maxBlue >= 10) return 10;
                    }
                }
            }

            return hasBlue ? Math.Pow(Math.E, maxBlue) : 0.0;
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var numberKids = long.Parse(reader.ReadLine());
            if (numberKids == 0)
            {
                writer.WriteLine("{0:F15}", 0.0);
            }
            else
            {
                var traders = new List<Trader>();
                for (long i = 0; i < numberKids; i++)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        var vals = line.Split(' ').ToArray();
                        traders.Add(new Trader(vals[0], vals[1], double.Parse(vals[2])));
                    }
                }

                var result = GetBlue(traders, "pink", 1.0);

                writer.WriteLine("{0:F15}", result > 10 ? 10 : result);
            }
            writer.Flush();
        }
    }
}
