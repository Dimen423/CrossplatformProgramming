using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs
{
    public class Lab1Manager
    {
        private readonly int _initialReputation;
        private readonly List<Tuple<int, int>> _inputData;

        public Lab1Manager(int initialReputation, List<Tuple<int, int>> inputData)
        {
            _initialReputation = initialReputation;
            _inputData = new List<Tuple<int, int>>(inputData);
        }

        public string Run()
        {
            if (_inputData == null || !_inputData.Any())
            {
                throw new Exception("Input data is empty or null.");
            }

            string resultRow = GetAgreedFriendsResultRow();
            return resultRow.Trim();
        }

        private string GetAgreedFriendsResultRow()
        {
            int reputation = _initialReputation;
            Dictionary<int, Tuple<int, int>> friendsInfo = new Dictionary<int, Tuple<int, int>>();
            for (int i = 0; i < _inputData.Count; i++)
            {
                friendsInfo.Add(i + 1, _inputData[i]);
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
