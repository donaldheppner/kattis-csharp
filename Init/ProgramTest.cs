using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

[TestFixture]
// ReSharper disable once CheckNamespace
public class ProgramTest
{
    public static IEnumerable<TestCaseData> TestDataFiles
    {
        get
        {
            var projectDirectory = Path.Combine(Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent?.FullName ?? string.Empty, "testdata");
            var inFiles = new DirectoryInfo(projectDirectory).GetFiles("*.in", SearchOption.AllDirectories);

            return inFiles.Select(i => i.FullName).Select(fn =>
                new TestCaseData(File.ReadAllText(fn))
                    .Returns(File.ReadAllText(fn.Substring(0, fn.Length - 2) + "ans")));
        }
    }

    [Test, TestCaseSource(typeof(ProgramTest), nameof(TestDataFiles))]
    public string Test(string input)
    {
        using (var inputStream = new MemoryStream())
        {
            using (var inputWriter = new StreamWriter(inputStream))
            {
                inputWriter.Write(input);
                inputWriter.Flush();

                inputStream.Position = 0;

                using (var outputStream = new MemoryStream())
                {
                    Program.Solve(inputStream, outputStream);
                    outputStream.Position = 0;

                    using (var result = new StreamReader(outputStream))
                    {
                        return result.ReadToEnd().Replace(Environment.NewLine, "\n");
                    }
                }
            }
        }
    }
}