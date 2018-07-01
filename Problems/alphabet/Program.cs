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

        public static int GetLongestSequence(int index, string letters, string sequence)
        {
            sequence += letters[index];

            var chains = new List<int> { sequence.Length };
            for (int i = index + 1; i < letters.Length; i++)
            {
                if (letters[i] > letters[index])
                {
                    chains.Add(GetLongestSequence(i, letters, sequence));
                }
            }

            return chains.Max();
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var letters = reader.ReadLine();

            var lengths = new List<int>();
            char minLetter = 'z';
            for(int i = 0; i < letters.Length; i++)
            {
                if (letters[i] < minLetter)
                {
                    minLetter = letters[i];
                    lengths.Add(GetLongestSequence(i, letters, string.Empty));
                }
            }
            writer.WriteLine(26 - lengths.Max());
            writer.Flush();
        }
    }
}
