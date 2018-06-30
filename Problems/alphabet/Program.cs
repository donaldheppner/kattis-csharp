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

        static int leftAndRightInOrder(int index, string letters)
        {
            char middle = letters[index];
            char leftLetter = middle;
            char rightLetter = middle;

            int leftCount = 0;
            for(int i = index - 1; i >= 0; i--)
            {
                if (letters[i] < leftLetter)
                {
                    leftLetter = letters[i];
                    leftCount++;
                }
            }

            int rightCount = 0;
            for (int i = index + 1; i < letters.Length; i++)
            {
                if (letters[i] > rightLetter)
                {
                    rightLetter = letters[i];
                    rightLetter++;
                }
            }

            return 26 - leftCount - rightCount - 1;
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var letters = reader.ReadLine();

            int min = 26;
            for (int i = 0; i < letters.Length; i++)
            {
                int a = leftAndRightInOrder(i, letters);
                if (a < min) min = a;
            }

            writer.WriteLine(min);
            writer.Flush();
        }
    }
}
