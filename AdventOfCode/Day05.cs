using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    class Day05 : BaseDay
    {
        private readonly List<BoardingPass> _input;

        public Day05()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            return _input.Max(boardingPass => boardingPass.SeatId).ToString();
        }

        public override string Solve_2()
        {
            var seatIds = _input.Select(boardingPass => boardingPass.SeatId).ToList();
            foreach (var seatId in seatIds)
            {
                if (!seatIds.Contains(seatId + 1) && seatIds.Contains(seatId + 2))
                {
                    return (seatId + 1).ToString();
                }
            }

            throw new SolvingException("No seat found!");
        }

        private List<BoardingPass> ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(line => new BoardingPass(line)).ToList();
        }

        internal class BoardingPass
        {
            private const int SeatMultiplier = 8;

            public int Row { get; private set; }
            public int Column { get; private set; }
            public int SeatId { get; private set; }

            public BoardingPass(string binaryString)
            {
                ParseData(binaryString);
            }

            private void ParseData(string binaryString)
            {
                var rowBinaryString = binaryString.Substring(0, 7);
                var columnBinaryString = binaryString.Substring(7, 3);

                Row = CalculateRow(rowBinaryString);
                Column = CalculateColumn(columnBinaryString);
                SeatId = CalculateSeatId();
            }

            private static int CalculateRow(string binaryString)
            {
                return Convert.ToInt32(binaryString.Replace("B", "1").Replace("F", "0"), 2);
            }

            private static int CalculateColumn(string binaryString)
            {
                return Convert.ToInt32(binaryString.Replace("R", "1").Replace("L", "0"), 2);
            }

            private int CalculateSeatId()
            {
                return Row * SeatMultiplier + Column;
            }
        }
    }
}
