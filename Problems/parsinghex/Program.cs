using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

            var line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                Match match = Regex.Match(line, @"(?:0[xX][0-9a-fA-F]{0,8})");
                while (match.Success)
                {
                    string hex = match.Value;
                    var integer = UInt32.Parse(hex.Substring(2), NumberStyles.AllowHexSpecifier);

                    writer.WriteLine("{0} {1}", hex, integer);
                    match = match.NextMatch();
                }

                line = reader.ReadLine();
            }

            writer.Flush();
        }
    }
}
