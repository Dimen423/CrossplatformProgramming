using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs
{
    public class Lab3Manager
    {
        private readonly List<List<int>> _tree;
        private readonly List<string> _restrictions;

        public Lab3Manager(List<Tuple<int, int>> connections, List<string> restrictions)
        {
            int n = restrictions.Count;
            _tree = new List<List<int>>();
            for (int i = 0; i <= n; i++)
            {
                _tree.Add(new List<int>());
            }

            foreach (var (u, v) in connections)
            {
                _tree[u].Add(v);
                _tree[v].Add(u);
            }

            _restrictions = restrictions;
        }

        public string Run()
        {
            var memo = new Dictionary<(int, char), int>();
            return Dfs(1, -1, 'X', memo).ToString();
        }

        private int Dfs(int node, int parent, char parentColor, Dictionary<(int, char), int> memo)
        {
            if (memo.ContainsKey((node, parentColor)))
            {
                return memo[(node, parentColor)];
            }

            int indigo = 0;
            if (_restrictions[node - 1].Contains('I') && parentColor != 'I')
            {
                indigo = 1;
                foreach (var child in _tree[node])
                {
                    if (child != parent)
                    {
                        indigo += Dfs(child, node, 'I', memo);
                    }
                }
            }

            int nonIndigo = 0;
            foreach (var child in _tree[node])
            {
                if (child != parent)
                {
                    nonIndigo += Dfs(child, node, 'X', memo);
                }
            }

            memo[(node, parentColor)] = Math.Max(indigo, nonIndigo);
            return memo[(node, parentColor)];
        }
    }
}
