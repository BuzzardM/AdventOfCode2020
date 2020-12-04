using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    class Day03 : BaseDay
    {
        private readonly char[][] _input;

        public Day03()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            return GetTreeEncouters(1, 3).ToString();
        }

        public override string Solve_2()
        {
            return ((long) GetTreeEncouters(1, 1) * GetTreeEncouters(1, 3) * GetTreeEncouters(1, 5) * GetTreeEncouters(1, 7) *
                    GetTreeEncouters(2, 1)).ToString();
        }

        private char[][] ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(i => i.ToArray()).ToArray();
        }

        private int GetTreeEncouters(int amountOfStepsDown, int amountOfStepsRight)
        {
            var amountOfTrees = 0;
            (int x, int y) currentPosition = (0, 0);

            for (var i = 0; i < _input.Length; i += amountOfStepsDown)
            {
                if (currentPosition.x + amountOfStepsRight >= _input[i].Length)
                {
                    currentPosition.x = (_input[i].Length - (currentPosition.x + amountOfStepsRight)) * -1;
                }
                else
                {
                    currentPosition.x += amountOfStepsRight;
                }

                if (currentPosition.y + amountOfStepsDown < _input.Length)
                {
                    currentPosition.y += amountOfStepsDown;
                    if (_input[currentPosition.y][currentPosition.x] == '#')
                        amountOfTrees++;
                }
            }

            return amountOfTrees;
        }
    }
}
