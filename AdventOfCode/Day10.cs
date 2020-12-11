using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    class Day10 : BaseDay
    {
        private readonly List<int> _input;

        public Day10()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            // both are initalized at one because outlet is 1 jolt difference and built in jolts always has 3 jolts difference.
            var oneJoltDifferences = 1;
            var threeJoltDifferences = 1;

            // calculate differences between adapters
            for (var i = 1; i < _input.Count; i++)
            {
                if (_input[i] - _input[i - 1] == 1)
                    oneJoltDifferences++;
                else
                    threeJoltDifferences++;
            }

            return (oneJoltDifferences * threeJoltDifferences).ToString();
        }

        public override string Solve_2()
        {
            long amountOfWays = 1;
            var tempInput = _input;
            tempInput.Insert(0, 0);
            var optionalList = GetOptionalList(tempInput).ToList();

            foreach (var item in optionalList)
            {
                if (optionalList.Contains(item - 1) && optionalList.Contains(item - 2))
                    amountOfWays += 3 * amountOfWays / 4;
                else
                    amountOfWays += amountOfWays;
            }

            return amountOfWays.ToString();
        }

        private static IEnumerable<int> GetOptionalList(IReadOnlyList<int> input)
        {
            var result = new List<int>();
            for (var i = 1; i < input.Count - 1; i++)
            {
                if (input[i + 1] - input[i - 1] <= 3)
                    result.Add(input[i]);
            }

            return result;
        }

        private List<int> ParseInput()
        {
            var result = File.ReadAllLines(InputFilePath).Select(line => Convert.ToInt32(line)).ToList();
            result.Sort();
            return result;
        }
    }
}
