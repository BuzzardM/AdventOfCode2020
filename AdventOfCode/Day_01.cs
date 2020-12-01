using AoCHelper;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly List<int> _input;
        private const int MaxNum = 2020;

        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath).Select(int.Parse).ToList();
        }

        public override string Solve_1()
        {
            foreach (int i in _input)
            {
                if (i > MaxNum)
                    continue;

                foreach (int j in _input)
                {
                    if (i + j == MaxNum)
                        return (i*j).ToString();
                }
            }
            throw new SolvingException("No solution found!");
        }

        public override string Solve_2()
        {
            foreach (int i in _input)
            {
                if (i > MaxNum)
                    continue;

                foreach (int j in _input)
                {
                    if (j > MaxNum)
                        continue;

                    foreach (int k in _input)
                    {
                        if (i + j + k == MaxNum)
                            return (i * j * k).ToString();
                    }
                }
            }
            throw new SolvingException("No solution found!");
        }
    }
}
