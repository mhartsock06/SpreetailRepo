using Spreetail.Constants;
using Spreetail.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Spreetail
{
    public class Logger : ILogger
    {
        public void DisplayResults(IEnumerable<string> results)
        {
            if (results == null || !results.Any())
            {
                Console.WriteLine("(empty set)");
            }
            else
            {
                int index = 0;
                results.ToList().ForEach(item =>
                {
                    index++;
                    Console.WriteLine($"{index}) {item}");
                });
            }
        }

        public void DisplayResults((bool keyExists, bool? memberExists, bool success, IEnumerable<string> results) response, string command)
        {
            switch (command)
            {
                case ActionsConstants.Remove:
                    if (!response.keyExists)
                    {
                        Console.WriteLine("Error, key does not exist");
                    }
                    else if ((bool)!response.memberExists)
                    {
                        Console.WriteLine("Error, member does not exist");
                    }
                    else if (response.success)
                    {
                        Console.WriteLine("Removed");
                    }
                    break;

                case ActionsConstants.RemoveAll:
                    if (!response.keyExists)
                    {
                        Console.WriteLine("Error, key does not exist");
                    }
                    else if (response.success)
                    {
                        Console.WriteLine("Removed");
                    }
                    break;
                case ActionsConstants.Members:
                    if (!response.keyExists)
                    {
                        Console.WriteLine("Error, key does not exist");
                    }
                    else if (response.success && (bool)response.memberExists)
                    {
                        int index = 0;
                        response.results.ToList().ForEach(item =>
                        {
                            index++;
                            Console.WriteLine($"{index}) {item}");
                        });
                    }
                    break;
                default:
                    break;
            }
        }

        public void DisplayResults(bool success, string command)
        {
            switch (command)
            {
                case ActionsConstants.Add:
                    Console.WriteLine(success ? "Added" : "Error, member already exists for key");
                    break;
                case ActionsConstants.Clear:
                    Console.WriteLine(success ? "Cleared" : "Error, unable to clear");
                    break;
                case ActionsConstants.KeyExists:
                    Console.WriteLine(success);
                    break;
                case ActionsConstants.MemberExists:
                    Console.WriteLine(success);
                    break;
                default:
                    break;
            }
        }

        public void DisplayResults(Dictionary<string, HashSet<string>> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("(empty set)");
            }
            else
            {
                int index = 0;
                results.ToList().ForEach(entry =>
                {
                    entry.Value.ToList().ForEach(value =>
                    {
                        index++;
                        Console.WriteLine($"{index}) {entry.Key}: {value}");
                    });
                });
            }
        }

        public void Save(Dictionary<string, HashSet<string>> multiValueDictionary, string filePath)
        {
            var saveFile = new FileInfo(filePath);

            saveFile.Directory.Create();

            var json = JsonConvert.SerializeObject(multiValueDictionary);

            File.WriteAllText(saveFile.FullName, json);
        }
    }
}
