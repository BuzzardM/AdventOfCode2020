using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode
{
    class Day11 : BaseDay
    {
        private char[][] _input;

        public Day11()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            throw new NotImplementedException();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }

        private void ChangeSeats(char[][] seats)
        {
            
        } 

        private char[][] ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(i => i.ToArray()).ToArray();
        }
    }
}
