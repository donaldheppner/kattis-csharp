using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Kattis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public class Community
        {
            public HashSet<int> Members { get; } = new HashSet<int>();
            public int MinSize { get; private set; } = 0;
            public int MaxSize => Members.Count;

            public override bool Equals(object obj)
            {
                return obj is Community && (obj as Community).Members.SetEquals(Members);
            }

            public override int GetHashCode()
            {
                return Members.GetHashCode();
            }

            public bool Contains(int member)
            {
                return Members.Contains(member);
            }

            public void AddMember(int member, Dictionary<int, int> allDependencies, Dictionary<int, int> remainingDependencies)
            {
                do
                {
                    Members.Add(member);
                    remainingDependencies.Remove(member);

                    if (MinSize == 0)
                    {
                        // have we found our cycle?
                        if (Contains(allDependencies[member]))
                        {
                            MinSize++;

                            // calculate distance from target to member
                            int target = allDependencies[member]; // member in the collection we are dependent on
                            while (target != member)
                            {
                                target = allDependencies[target];
                                MinSize++;
                            }

                            break;
                        }

                        member = allDependencies[member];
                    }
                    else break; // just adding a single member
                } while (remainingDependencies.Count > 0);
            }
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var reader = new StreamReader(stdin);
            var writer = new StreamWriter(stdout);

            var line1 = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var totalParticipants = line1[0];
            var numberOfPlaces = line1[1];
            var dependencies = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var allDependencies = Enumerable.Range(1, totalParticipants).ToDictionary(x => x, x => dependencies[x - 1]);
            var remainingDependencies = new Dictionary<int, int>(allDependencies);

            List<Community> communities = new List<Community>();
            while (remainingDependencies.Count != 0)
            {
                // find a community that contains the referenced member
                bool hasCommunity = false;
                var elderly = remainingDependencies.First();
                foreach (var community in communities)
                {
                    if (community.Contains(elderly.Value))
                    {
                        community.AddMember(elderly.Key, allDependencies, remainingDependencies);
                        hasCommunity = true;
                        break;
                    }
                }

                if (!hasCommunity)
                {
                    var community = new Community();
                    community.AddMember(elderly.Key, allDependencies, remainingDependencies);
                    communities.Add(community);
                }
            }

            var allCommunities = new List<Community>(communities);
            var weights = allCommunities.Select(c => c.MinSize).ToArray();
            var values = allCommunities.Select(c => c.MaxSize).ToArray();

            var result = Math.Min(numberOfPlaces, KnapSack(numberOfPlaces, weights, values, weights.Length));

            writer.WriteLine(result);
            writer.Flush();
        }

        public static int KnapSack(int capacity, int[] weight, int[] value, int itemsCount)
        {
            int[,] K = new int[itemsCount + 1, capacity + 1];

            for (int i = 0; i <= itemsCount; ++i)
            {
                for (int w = 0; w <= capacity; ++w)
                {
                    if (i == 0 || w == 0)
                        K[i, w] = 0;
                    else if (weight[i - 1] <= w)
                        K[i, w] = Math.Max(value[i - 1] + K[i - 1, w - weight[i - 1]], K[i - 1, w]);
                    else
                        K[i, w] = K[i - 1, w];
                }
            }

            return K[itemsCount, capacity];
        }
    }
}
