using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    class Day11 : BaseDay
    {
        private readonly char[][] _input;
        private const int OccupationRulePartOne = 4;
        private const int OccupationRulePartTwo = 5;

        public Day11()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            (char[][] changedSeats, int amountOfSeatsChanged) result = (_input, -1);
            while (result.amountOfSeatsChanged != 0)
            {
                result = ChangeSeats(result.changedSeats);
            }
            return result.changedSeats.SelectMany(value => value).Count(c => c.Equals('#')).ToString();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }

        private static (char[][] changedSeats, int amountOfSeatsChanged) ChangeSeats(char[][] seats)
        {
            var result = new char[seats.Length][];
            var amountOfSeatsChanged = 0;
            for (var i = 0; i < seats.Length; i++)
            {
                result[i] = new char[seats[i].Length];
                for (var j = 0; j < seats[i].Length; j++)
                {
                    switch (seats[i][j])
                    {
                        case '.':
                            result[i][j] = '.';
                            break;
                        case 'L':
                            if (GetRelativePositions(j, i, seats).All(value => value != '#'))
                            {
                                result[i][j] = '#';
                                amountOfSeatsChanged++;
                            }
                            else
                            {
                                result[i][j] = 'L';
                            }
                            break;
                        case '#':
                            if (GetRelativePositions(j, i, seats).Count(value => value == '#') >= OccupationRulePartOne)
                            {
                                result[i][j] = 'L';
                                amountOfSeatsChanged++;
                            }
                            else
                            {
                                result[i][j] = '#';
                            }
                            break;
                    }
                }
            }

            return (result, amountOfSeatsChanged);
        }

        private static List<char> GetRelativePositions(int x, int y, char[][] seats)
        {
            var result = new List<char>
            {
                {
                    y-1 >= 0
                    ? seats[y-1][x]
                    : ' '
                },
                {
                    y+1 < seats.Length
                    ? seats[y+1][x]
                    : ' '
                },
                {
                    x-1 >= 0
                    ? seats[y][x-1]
                    : ' '
                },
                {
                    x+1 < seats[x].Length
                    ? seats[y][x+1]
                    : ' '
                },
                {
                    y-1 >= 0 && x-1 >= 0
                    ? seats[y-1][x-1]
                    : ' '
                },
                {
                    y-1 >= 0 && x+1 < seats[x].Length
                    ? seats[y-1][x+1]
                    : ' '
                },
                {
                    y+1 < seats.Length && x-1 >= 0
                    ? seats[y+1][x-1]
                    : ' '
                },
                {
                    y+1 < seats.Length && x+1 < seats[x].Length
                    ? seats[y+1][x+1]
                    : ' '
                }
            };
            return result;
        }

        private char[][] ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(i => i.ToArray()).ToArray();
        }
    }
}
