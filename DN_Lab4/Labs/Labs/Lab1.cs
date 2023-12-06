using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs
{
    public static class Lab1
    {
        public static string Run(string pathInpFile = "input.txt")
        {
            List<Tuple<int, int>> inputData = File.ReadLines(pathInpFile).
                Select(tpl =>
                {
                    int spaceIndex = tpl.IndexOf(' ');
                    return new Tuple<int, int>(Convert.ToInt32(tpl.Substring(0, spaceIndex)), Convert.ToInt32(tpl.Substring(spaceIndex + 1)));
                }
                ).ToList();

            return GetAgreedFriendsResultRow(inputData).Trim();
        }

        /// <summary>
        /// Отримати список друзів, яких зможе загітувати Толік
        /// </summary>
        /// <returns>Результуючий рядок, в якому на першому рядку кількість друзів, а в другому рядку номери друзів Толіка в тому порядку, в якому потрібно їх агітувати</returns>
        private static string GetAgreedFriendsResultRow(List<Tuple<int, int>> inputData)
        {
            int reputation = inputData[0].Item2;
            Dictionary<int, Tuple<int, int>> friendsInfo = new Dictionary<int, Tuple<int, int>>();
            for (int i = 1; i < inputData.Count; i++)
            {
                friendsInfo.Add(i, inputData[i]);
            }
            var friendsToGrowReputation = friendsInfo.Where(x => x.Value.Item2 >= 0).OrderBy(x => x.Value.Item1).ToDictionary(key => key.Key, value => value.Value);
            var friendsToLoseReputation = friendsInfo.Where(x => x.Value.Item2 < 0).ToDictionary(key => key.Key, value => value.Value);
            bool addFriends = true;
            string resultRow = string.Empty;
            int countOfFriends = 0;
            while (addFriends)
            {
                addFriends = false;
                int friendNum = friendsToGrowReputation.Where(friend => friend.Value.Item1 <= reputation).FirstOrDefault().Key;
                if (friendNum != 0)
                {
                    addFriends = true;
                    countOfFriends++;
                    resultRow += ' ' + Convert.ToString(friendNum);
                    reputation += friendsToGrowReputation[friendNum].Item2;
                    friendsToGrowReputation.Remove(friendNum);
                }
                else
                {
                    friendNum = friendsToLoseReputation.Where(friend => friend.Value.Item1 <= reputation).OrderByDescending(friend => friend.Value.Item1).ThenByDescending(friend => friend.Value.Item2).FirstOrDefault().Key;
                    if (friendNum != 0)
                    {
                        addFriends = true;
                        countOfFriends++;
                        resultRow += ' ' + Convert.ToString(friendNum);
                        reputation += friendsToLoseReputation[friendNum].Item2;
                        friendsToLoseReputation.Remove(friendNum);
                    }
                }
            }

            return Convert.ToString(countOfFriends) + '\n' + resultRow.Trim();
        }
    }
}
