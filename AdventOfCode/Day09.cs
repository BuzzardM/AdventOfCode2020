using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    class Day09 : BaseDay
    {
        private const int PreambleSize = 25;
        private readonly List<long> _input;

        public Day09()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            return GetWrongNumber(25).ToString();
        }

        public override string Solve_2()
        {
            var contiguousRange = GetContiguousRange();

            return (contiguousRange.Min() + contiguousRange.Max()).ToString();
        }

        private long GetWrongNumber(int preambleSize)
        {
            for (var i = PreambleSize; i < _input.Count; i++)
            {
                if (IsWrongNumber(_input[i], _input.GetRange(i - preambleSize, preambleSize)))
                    return _input[i];
            }
            throw new SolvingException("No wrong number found");
        }

        private static bool IsWrongNumber(long number, IReadOnlyCollection<long> preambleList)
        {
            return !(from i in preambleList where i <= number from j in preambleList where i + j == number && i != j select i).Any();
        }

        private List<long> GetContiguousRange()
        {
            var wrongNumber = Convert.ToInt32(Solve_1());
            
            for (var i = 0; i < _input.Count; i++)
            {
                var sum = _input[i];
                for (var j = i + 1; j < _input.Count; j++)
                {
                    sum += _input[j];
                    if (sum > wrongNumber)
                        break;
                    if (sum == wrongNumber)
                        return _input.GetRange(i, j - i);
                }
            }
            throw new SolvingException("No contiguous range found!");
        }

        private List<long> ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(line => Convert.ToInt64(line)).ToList();
        }
    }
}
