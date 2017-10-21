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

        private static bool IsPalindrome(string str, int start, int end)
        {
            for (int i = 0; i <= (end - start) / 2; i++)
            {
                if (str[start + i] != str[end - i]) return false;
            }
            return true;
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                HashSet<string> result = new HashSet<string>();
                for (int i = 0; i < line.Length - 1; i++)
                {
                    for (int j = i + 1; j < line.Length; j++)
                    {
                        if (IsPalindrome(line, i, j)) result.Add(line.Substring(i, j - i + 1));
                    }
                }

                // leave sorting to the end
                SortedSet<string> sortedSet = new SortedSet<string>(result);
                foreach (var palindrome in sortedSet)
                {
                    writer.WriteLine(palindrome);
                }

                writer.WriteLine();
                line = reader.ReadLine();
            }

            
            writer.Flush();
        }
    }
}
