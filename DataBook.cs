using Spreetail.Interfaces;
using System;
using System.Collections.Generic;

namespace Spreetail
{
    public class DataBook : IDataBook
    {
        public Dictionary<string, HashSet<string>> DataMemberList { get; set; }

        public DataBook()
        {
            DataMemberList = new Dictionary<string, HashSet<string>>();
        }


        public bool AddMember(string key, string value)
        {
            bool success;

            DataMemberList.TryGetValue(key, out HashSet<string> currentValueList);

            if (currentValueList == null)
            {
                success = DataMemberList.TryAdd(key, new HashSet<string> { value });
            }
            else if (currentValueList.Contains(value))
            {
                success = false;
            }
            else
            {
                success = currentValueList.Add(value);
            }

            return success;
        }

        public (bool, bool?, bool, IEnumerable<string>) RemoveMember(string key, string value)
        {
            bool success = false;

            bool keyExists = DataMemberList.TryGetValue(key, out HashSet<string> currentValueList);
            bool? memberExists = currentValueList?.Contains(value);

            if (keyExists && currentValueList.Count > 0)
            {
                success = currentValueList.Remove(value);

                if (currentValueList.Count == 0)
                {
                    DataMemberList.Remove(key);
                }
            }

            return (keyExists, memberExists, success, null);
        }

        public (bool, bool?, bool, IEnumerable<string>) ListMembers(string key)
        {
            bool keyExists = DataMemberList.TryGetValue(key, out HashSet<string> currentValueList);
            bool? membersExists = currentValueList?.Count > 0;
            bool success = true;

            return (keyExists, (bool)membersExists, success, currentValueList);
        }

        public IEnumerable<string> ListKeys()
        {
            return DataMemberList.Keys;
        }

        public (bool, bool?, bool, IEnumerable<string>) RemoveAllMembers(string key)
        {
            bool success = false;
            bool keyExists = DataMemberList.TryGetValue(key, out HashSet<string> currentValueList);

            if (keyExists)
            {
                currentValueList.Clear();
                DataMemberList.Remove(key);
                success = true;
            }

            return (keyExists, null, success, null);
        }

        public bool ClearEntries()
        {
            DataMemberList.Clear();

            return true;
        }

        public bool KeyExists(string key)
        {
            bool keyExists = DataMemberList.TryGetValue(key, out _);

            return keyExists;
        }

        public bool MemberExists(string key, string value)
        {
            bool keyExists = DataMemberList.TryGetValue(key, out HashSet<string> currentValueList);
            bool memberExists = keyExists ? (bool)currentValueList?.Contains(value) : false;

            return keyExists && memberExists;

        }

        public IEnumerable<string> ListAllMembers()
        {
            List<string> memberCollection = new List<String>();

            foreach (HashSet<string> entry in DataMemberList.Values)
            {
                memberCollection.AddRange(entry);
            }

            return memberCollection;
        }

        public Dictionary<string, HashSet<string>> ListAllItems()
        {
            return DataMemberList;
        }

        private IEnumerable<string> DoesKeyExist(string key)
        {
            DataMemberList.TryGetValue(key, out HashSet<string> currentValueList);

            return currentValueList;
        }
    }
}
