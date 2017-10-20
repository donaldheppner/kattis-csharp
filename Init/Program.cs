using System;
using System.IO;
using System.Linq;

// ReSharper disable once CheckNamespace
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

        var answer = reader.ReadLine()?.Split(' ').Last() ?? string.Empty;


        writer.WriteLine(answer);
        writer.Flush();
    }
}
