using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    class Day11 : BaseDay
    {
        private readonly SeatingMapItem[][] _inputPartOne;
        private readonly SeatingMapItem[][] _inputPartTwo;
        private const int OccupationRulePartOne = 4;
        private const int OccupationRulePartTwo = 5;

        public Day11()
        {
            _inputPartOne = ParseInput();
            _inputPartTwo = ParseInput();
            FillAdjacentSeatingMapItems(_inputPartOne, false);
            FillAdjacentSeatingMapItems(_inputPartTwo, true);
        }

        public override string Solve_1()
        {
            (SeatingMapItem[][] changedSeats, int amountOfSeatsChanged) result = (_inputPartOne, -1);
            while (result.amountOfSeatsChanged != 0)
            {
                result = ChangeSeats(result.changedSeats, false);
            }
            return result.changedSeats.SelectMany(value => value).Count(c => c.Icon.Equals('#')).ToString();
        }

        public override string Solve_2()
        {
            (SeatingMapItem[][] changedSeats, int amountOfSeatsChanged) result = (_inputPartTwo, -1);
            while (result.amountOfSeatsChanged != 0)
            {
                result = ChangeSeats(result.changedSeats, true);
            }
            return result.changedSeats.SelectMany(value => value).Count(c => c.Icon.Equals('#')).ToString();
        }

        private static (SeatingMapItem[][] changedSeats, int amountOfSeatsChanged) ChangeSeats(SeatingMapItem[][] seats, bool partTwo)
        {
            var occupationRule = partTwo ? OccupationRulePartTwo : OccupationRulePartOne;
            var seatsToChange = new List<SeatingMapItem>();
            foreach (var seatingMapItemArray in seats)
            {
                foreach (var seatingMapItem in seatingMapItemArray)
                {
                    switch (seatingMapItem.Icon)
                    {
                        case 'L':
                            if (seatingMapItem.AdjacentSeatingMapItems.All(value => value.Icon != '#'))
                            {
                                seatsToChange.Add(seatingMapItem);
                            }
                            break;
                        case '#':
                            if (seatingMapItem.AdjacentSeatingMapItems.Count(value => value.Icon == '#') >= occupationRule)
                            {
                                seatsToChange.Add(seatingMapItem);
                            }
                            break;
                    }
                }
            }

            foreach (var seat in seatsToChange)
            {
                seat.ChangeIcon();
            }

            return (seats, seatsToChange.Count);
        }

        private SeatingMapItem[][] ParseInput()
        {
            var file = File.ReadAllLines(InputFilePath);
            var result = new SeatingMapItem[file.Length][];
            for (var i = 0; i < file.Length; i++)
            {
                result[i] = new SeatingMapItem[file[i].Length];
                for (var j = 0; j < file[i].Length; j++)
                {
                    result[i][j] = new SeatingMapItem(j, i, file[i][j]);
                }
            }

            return result;
        }

        private static void FillAdjacentSeatingMapItems(SeatingMapItem[][] input, bool partTwo)
        {
            if (!partTwo)
            {
                for (var i = 0; i < input.Length; i++)
                {
                    for (var j = 0; j < input[i].Length; j++)
                    {
                        input[i][j].AdjacentSeatingMapItems = new List<SeatingMapItem>();
                        if (j - 1 >= 0)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i][j - 1]);
                        if (j + 1 < input[i].Length)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i][j + 1]);
                        if (i - 1 >= 0)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i - 1][j]);
                        if (i + 1 < input.Length)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i + 1][j]);
                        if (j - 1 >= 0 && i - 1 >= 0)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i - 1][j - 1]);
                        if (j - 1 >= 0 && i + 1 < input.Length)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i + 1][j - 1]);
                        if (j + 1 < input[i].Length && i - 1 >= 0)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i - 1][j + 1]);
                        if (j + 1 < input[i].Length && i + 1 < input.Length)
                            input[i][j].AdjacentSeatingMapItems.Add(input[i + 1][j + 1]);
                    }
                }
            }
            else
            {
                for (var i = 0; i < input.Length; i++)
                {
                    for (var j = 0; j < input[i].Length; j++)
                    {
                        input[i][j].AdjacentSeatingMapItems = new List<SeatingMapItem>();
                        SeatingMapItem left = null;
                        SeatingMapItem right = null;
                        SeatingMapItem up = null;
                        SeatingMapItem down = null;
                        SeatingMapItem left_up = null;
                        SeatingMapItem left_down = null;
                        SeatingMapItem right_up = null;
                        SeatingMapItem right_down = null;
                        var x = j;
                        var y = i;
                        while (x - 1 >= 0 && left == null)
                        {
                            if (input[y][x - 1].Icon != '.')
                                left = input[y][x - 1];
                            else
                                x--;
                        }
                        x = j;
                        y = i;
                        while (x + 1 < input[i].Length && right == null)
                        {
                            if (input[y][x + 1].Icon != '.')
                                right = input[y][x + 1];
                            else
                                x++;
                        }
                        x = j;
                        y = i;
                        while (y - 1 >= 0 && up == null)
                        {
                            if (input[y - 1][x].Icon != '.')
                                up = input[y - 1][x];
                            else
                                y--;
                        }
                        x = j;
                        y = i;
                        while (y + 1 < input.Length && down == null)
                        {
                            if (input[y + 1][x].Icon != '.')
                                down = input[y + 1][x];
                            else
                                y++;
                        }
                        x = j;
                        y = i;
                        while (x - 1 >= 0 && y - 1 >= 0 && left_up == null)
                        {
                            if (input[y - 1][x - 1].Icon == '.')
                                left_up = input[y - 1][x - 1];
                            else
                            {
                                x--;
                                y--;
                            }
                        }
                        x = j;
                        y = i;
                        while (x - 1 >= 0 && y + 1 < input.Length && left_down == null)
                        {
                            if (input[y + 1][x - 1].Icon == '.')
                                left_down = input[y + 1][x - 1];
                            else
                            {
                                x--;
                                y++;
                            }
                        }
                        x = j;
                        y = i;
                        while (x + 1 < input[i].Length && y - 1 >= 0 && right_up == null)
                        {
                            if (input[y - 1][x + 1].Icon == '.')
                                right_up = input[y - 1][x + 1];
                            else
                            {
                                x++;
                                y--;
                            }
                        }
                        x = j;
                        y = i;
                        while (x + 1 < input[i].Length && y + 1 < input.Length && right_down == null)
                        {
                            if (input[y + 1][x + 1].Icon == '.')
                                right_down = input[y + 1][x + 1];
                            else
                            {
                                x++;
                                y++;
                            }
                        }

                        input[i][j].AdjacentSeatingMapItems.AddRange(new[] { left, right, up, down, left_up, left_down, right_up, right_down }.Where(seatItem => seatItem is not null));
                    }
                }
            }
        }

        internal class SeatingMapItem
        {
            public int X { get; }
            public int Y { get; }
            public char Icon { get; private set; }
            public List<SeatingMapItem> AdjacentSeatingMapItems { get; set; }

            public SeatingMapItem(int x, int y, char icon)
            {
                X = x;
                Y = y;
                Icon = icon;
            }

            public void ChangeIcon()
            {
                Icon = Icon == '#'
                        ? 'L'
                        : '#';
            }
        }
    }
}
