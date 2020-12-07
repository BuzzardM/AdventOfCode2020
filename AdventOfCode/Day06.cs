using System;
using System.Collections.Generic;
using System.Linq;
using AoCHelper;
using FileParser;

namespace AdventOfCode
{
    class Day06 : BaseDay
    {
        private readonly List<List<string>> _input;

        public Day06()
        {
            _input = ParsedFile.ReadAllGroupsOfLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var answers = 0;
            foreach (var groupOfLines in _input)
            {
                answers += groupOfLines.SelectMany(line => line).Distinct().Count();
            }
            return answers.ToString();
        }

        public override string Solve_2()
        {
            var answers = 0;
            foreach (var groupOfLines in _input)
            {
                answers += groupOfLines.SelectMany(line => line).Distinct()
                    .Count(c => groupOfLines.All(answer => answer.Contains(c)));
            }

            return answers.ToString();
        }
    }
}
