using System.Collections.Generic;

namespace Spreetail.Interfaces
{
    public interface IDataBook
    {
        Dictionary<string, HashSet<string>> DataMemberList { get; set; }
        /// <summary>
        /// Adds a member to a collection for a given key. 
        /// </summary>
        /// <returns>boolean for success or failure of addition</returns>
        bool AddMember(string key, string value);

        /// <summary>
        /// Returns all the keys in the dictionary. Order is not guaranteed.
        /// </summary>
        IEnumerable<string> ListKeys();

        /// <summary>
        /// Returns the collection of strings for the given key. Return order is not guaranteed.
        /// </summary>
        /// <returns>
        /// a tuple with boolean values for if entries are present and the current list for the given key (keyExists, (bool)membersExists, success, currentValueList)
        /// </returns>
        (bool keyExists, bool? memberExists, bool success, IEnumerable<string> results) ListMembers(string key);

        /// <summary>
        /// Removes a member from a key. If the last member is removed from the key, the key is removed from the dictionary. If the key or member does not exist, displays an error.
        /// </summary>
        /// <returns>tuple with boolean values for keyExists, memberExists, and a successful retrieval (keyExists, memberExists, success, null)/returns>
        (bool keyExists, bool? memberExists, bool success, IEnumerable<string> results) RemoveMember(string key, string value);

        /// <summary>
        /// Removes all members for a key and removes the key from the dictionary. 
        /// </summary>
        /// <returns>tuple with boolean values for keyExists and successful retrieval. (keyExists, null, success ,null). Returns an error if the key does not exist.</returns>
        (bool keyExists, bool? memberExists, bool success, IEnumerable<string> results) RemoveAllMembers(string key);

        /// <summary>
        /// Removes all keys and all members from the dictionary.
        /// </summary>
        /// <returns>boolean indicating success or failure</returns>
        bool ClearEntries();

        /// <summary>
        /// Returns whether a key exists or not.
        /// </summary>
        bool KeyExists(string key);

        /// <summary>
        /// Returns whether a member exists within a key. Returns false if the key does not exist.
        /// </summary>
        bool MemberExists(string key, string value);

        /// <summary>
        /// Returns all the members in the dictionary. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        IEnumerable<string> ListAllMembers();

        /// <summary>
        /// Returns all keys in the dictionary and all of their members. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        Dictionary<string, HashSet<string>> ListAllItems();
    }
}
