using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day02 : BaseDay
    {
        private readonly List<Entry> _input;

        public Day02()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            var validAmount = 0;
            foreach (var entry in _input)
            {
                /*
                 * Calculate amount of char occurrences in password
                 * and check if they are within set boundaries
                 * if so, bump amount
                 */
                var amountOfOccurrences = entry.Password.Count(x => (x == entry.PolicyCharacter));

                if (amountOfOccurrences >= entry.FirstInt && amountOfOccurrences <= entry.SecondInt)
                    validAmount++;
            }
            return validAmount.ToString();
        }

        public override string Solve_2()
        {
            var validAmount = 0;
            foreach (var entry in _input)
            {
                /*
                 * Calculate amount of char occurrences in password
                 * and check if they are within set boundaries
                 * if so, bump amount
                 */
                char[] chars = { entry.Password[entry.FirstInt - 1], entry.Password[entry.SecondInt - 1] };

                var amountOfOccurrences = chars.Count(x => (x == entry.PolicyCharacter));

                if (amountOfOccurrences == 1)
                    validAmount++;
            }
            return validAmount.ToString();
        }

        private List<Entry> ParseInput()
        {
            var entries = new List<Entry>();
            foreach (var line in File.ReadAllLines(InputFilePath))
            {
                /*
                 * Split string into 2 halves;
                 * 0 = policy
                 * 1 = password
                 * and clean up password string
                 */
                var choppedString = line.Split(':');
                var password = choppedString[1].Replace(" ", "");

                /*
                 * Split policy into 2 halves;
                 * 0 = min and max int
                 * 1 = character
                 * and convert character string to char
                 */
                var choppedPolicy = choppedString[0].Split(' ');
                var policyCharacter = char.Parse(choppedPolicy[1]);

                /*
                 * Split choppedPolicy[0] into 2 halves;
                 * 0 = min int
                 * 1 = max int
                 * and convert to integers
                 */
                var boundaries = choppedPolicy[0].Split('-');
                var firstInt = Convert.ToInt32(boundaries[0]);
                var secondInt = Convert.ToInt32(boundaries[1]);

                entries.Add(new Entry(password, policyCharacter, firstInt, secondInt));
            }
            return entries;
        }
        internal class Entry
        {
            public string Password { get; }
            public char PolicyCharacter { get; }
            public int FirstInt { get; }
            public int SecondInt { get; }

            public Entry(string password, char policyChar, int first, int second)
            {
                Password = password;
                PolicyCharacter = policyChar;
                FirstInt = first;
                SecondInt = second;
            }
        }
    }

}