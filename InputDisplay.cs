using Spreetail.Constants;
using Spreetail.Interfaces;
using System;

namespace Spreetail
{
    internal class InputDisplay
    {
        //Could also be injected into the constructor for this and utilize DI after registering the classes in the container
        private readonly IDataBook _book = new DataBook();
        private readonly ILogger _logger = new Logger();

        public void HUD()
        {
            Console.WriteLine("Welcome to the Multi-Value Dictionary. Type HELP for commands.");
            string input = string.Empty;

            while (input != "exit")
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
                Console.WriteLine("Input: ");
                input = Console.ReadLine();

                string[] inputArgs = input.Split(" ");

                string action = string.Empty;
                string key = string.Empty;
                string value = string.Empty;
                if (inputArgs.Length > 0)
                {
                    action = HasElement(0, inputArgs) ? inputArgs[0] : null;
                    key = HasElement(1, inputArgs) ? inputArgs[1] : null;
                    value = HasElement(2, inputArgs) ? inputArgs[2] : null;
                }
                try
                {
                    switch (action.ToUpperInvariant())
                    {
                        case ActionsConstants.Add:
                            _logger.DisplayResults(_book.AddMember(key, value), ActionsConstants.Add);
                            break;
                        case ActionsConstants.Remove:
                            _logger.DisplayResults(_book.RemoveMember(key, value), ActionsConstants.Remove);
                            break;
                        case ActionsConstants.RemoveAll:
                            _logger.DisplayResults(_book.RemoveAllMembers(key), ActionsConstants.RemoveAll);
                            break;
                        case ActionsConstants.Clear:
                            _logger.DisplayResults(_book.ClearEntries(), ActionsConstants.Clear);
                            break;
                        case ActionsConstants.Keys:
                            _logger.DisplayResults(_book.ListKeys());
                            break;
                        case ActionsConstants.KeyExists:
                            _logger.DisplayResults(_book.KeyExists(key), ActionsConstants.KeyExists);
                            break;
                        case ActionsConstants.Members:
                            _logger.DisplayResults(_book.ListMembers(key), ActionsConstants.Members);
                            break;
                        case ActionsConstants.MemberExists:
                            _logger.DisplayResults(_book.MemberExists(key, value), ActionsConstants.MemberExists);
                            break;
                        case ActionsConstants.AllMembers:
                            _logger.DisplayResults(_book.ListAllMembers());
                            break;
                        case ActionsConstants.Items:
                            _logger.DisplayResults(_book.ListAllItems());
                            break;
                        case ActionsConstants.Help:
                            DisplayHelp();
                            break;
                        case ActionsConstants.CLS:
                            Console.Clear();
                            break;
                        case ActionsConstants.Save:
                            _logger.Save(_book.DataMemberList, key);
                            break;
                        case ActionsConstants.Exit:
                            Console.WriteLine("Thanks for playing!");
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
                catch (ArgumentNullException e)
                {

                    Console.WriteLine($"Error - Missing Input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }


            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("MULTI-VALUE DICTIONARY COMMANDS");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"{ActionsConstants.Add} - Adds a member value to the specified key. USAGE: {ActionsConstants.Add} <key> <member>");
            Console.WriteLine($"{ActionsConstants.Remove} - Removes a member value from the specified key. USAGE: {ActionsConstants.Remove} <key> <member>");
            Console.WriteLine($"{ActionsConstants.RemoveAll} - Removes all members for a key and removes the key from the dictionary. USAGE: {ActionsConstants.RemoveAll} <key>");
            Console.WriteLine($"{ActionsConstants.Clear} - Removes all keys and all members from the dictionary. USAGE: {ActionsConstants.Clear}");
            Console.WriteLine($"{ActionsConstants.Keys} - Returns all the keys in the dictionary. USAGE: {ActionsConstants.Keys}");
            Console.WriteLine($"{ActionsConstants.KeyExists} - Returns whether a key exists or not. USAGE: {ActionsConstants.KeyExists} <key>");
            Console.WriteLine($"{ActionsConstants.Members} - Returns the collection of strings for the given key. USAGE: {ActionsConstants.Members} <key> ");
            Console.WriteLine($"{ActionsConstants.MemberExists} - Returns whether a member exists within a key. Returns false if the key does not exist. USAGE: {ActionsConstants.MemberExists} <key> <member> ");
            Console.WriteLine($"{ActionsConstants.AllMembers} - Returns all the members in the dictionary. USAGE: {ActionsConstants.AllMembers}");
            Console.WriteLine($"{ActionsConstants.Items} - Returns all keys in the dictionary and all of their members. USAGE: {ActionsConstants.Items} ");
            Console.WriteLine($"{ActionsConstants.CLS} - Clears the screen. USAGE: CLS");
            Console.WriteLine($"{ActionsConstants.Save} - Saves the dictionary to a file. USAGE: {ActionsConstants.Save} <filepath>");
            Console.WriteLine($"{ActionsConstants.Help} - Brings up this menu.");
        }

        private static bool HasElement(int index, string[] array)
        {
            return (index >= 0) && (index < array.Length);
        }

    }
}
