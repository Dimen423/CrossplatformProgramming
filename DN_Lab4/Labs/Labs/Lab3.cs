using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs
{
    public static class Lab3
    {
        static Dictionary<(int, char), int> memo = new Dictionary<(int, char), int>();
        static List<int>[] tree;
        static string[] restrictions;

        public static string Run(string pathInpFile = "input.txt")
        {
            string[] lines = File.ReadAllLines("input.txt");
            int n = int.Parse(lines[0]);
            tree = new List<int>[n + 1];
            restrictions = new string[n];

            for (int i = 0; i < tree.Length; i++)
            {
                tree[i] = new List<int>();
            }

            for (int i = 1; i < n; i++)
            {
                string[] parts = lines[i].Split(' ');
                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);

                tree[u].Add(v);
                tree[v].Add(u);
            }

            for (int i = 0; i < n; i++)
            {
                restrictions[i] = lines[n - 1 + i + 1];
            }

            int result = Dfs(1, -1, 'X');
            return result.ToString();
        }

        private static int Dfs(int node, int parent, char parentColor)
        {
            if (memo.ContainsKey((node, parentColor)))
            {
                return memo[(node, parentColor)];
            }

            int indigo = 0;
            if (restrictions[node - 1].Contains('I') && parentColor != 'I')
            {
                indigo = 1;
                foreach (var child in tree[node])
                {
                    if (child != parent)
                    {
                        indigo += Dfs(child, node, 'I');
                    }
                }
            }

            int nonIndigo = 0;
            foreach (var child in tree[node])
            {
                if (child != parent)
                {
                    nonIndigo += Dfs(child, node, 'X');
                }
            }

            memo[(node, parentColor)] = Math.Max(indigo, nonIndigo);
            return memo[(node, parentColor)];
        }
    }
}
