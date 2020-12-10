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
            throw new NotImplementedException();
        }

        private List<int> ParseInput()
        {
            var result = File.ReadAllLines(InputFilePath).Select(line => Convert.ToInt32(line)).ToList();
            result.Sort();
            return result;
        }
    }
}
