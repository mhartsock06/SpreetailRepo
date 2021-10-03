using System.Collections.Generic;

namespace Spreetail.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Displays the returned results
        /// </summary>
        void DisplayResults(IEnumerable<string> results);
        /// <summary>
        /// Displays the returned results based on the given command
        /// </summary>
        /// <param name="response">Tuple for keyExists, memberExists, success, and result set if there are any</param>
        /// <param name="command">ActionsConstants command</param>
        void DisplayResults((bool keyExists, bool? memberExists, bool success, IEnumerable<string> results) response, string command);
        /// <summary>
        /// Displays whether the command entered was successful or not
        /// </summary>
        /// <param name="success"> boolean</param>
        /// <param name="command">ActionsConstants command</param>
        void DisplayResults(bool success, string command);
        /// <summary>
        /// Displays the entire Multi-valued dictionary
        /// </summary>
        /// <param name="results">The dictionary to display</param>
        void DisplayResults(Dictionary<string, HashSet<string>> results);
        /// <summary>
        /// Converts the dictionary to JSON and saves to the specified path
        /// </summary>
        void Save(Dictionary<string, HashSet<string>> multiValueDictionary, string filePath);
    }
}
