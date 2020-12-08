using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    class Day08 : BaseDay
    {
        private readonly List<AssemblyInstruction> _input;

        public Day08()
        {
            _input = ParseInput().ToList();
        }

        public override string Solve_1()
        {
            return SolveInstructions().accumulatorValue.ToString();
        }

        public override string Solve_2()
        {
            for (var instructionPointer = 0; instructionPointer < _input.Count; instructionPointer++)
            {
                var instruction = _input[instructionPointer];
                if (instruction.Name == "acc")
                    continue;
                var replacementInstruction = instruction.Name switch
                {
                    "nop" => new AssemblyInstruction("jmp", instruction.Value),
                    "jmp" => new AssemblyInstruction("nop", instruction.Value),
                    _ => throw new SolvingException("Unexpected input in " + InputFileDirPath)
                };

                _input.RemoveAt(instructionPointer);
                _input.Insert(instructionPointer, replacementInstruction);

                var (success, accumulatorValue) = SolveInstructions();
                if (success)
                    return accumulatorValue.ToString();
                
                _input.RemoveAt(instructionPointer);
                _input.Insert(instructionPointer, instruction);
            }
            throw new SolvingException("No solution found!");
        }

        private IEnumerable<AssemblyInstruction> ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(line => line.Split(" ")).Select(splitLine => new AssemblyInstruction(splitLine[0], Convert.ToInt32(splitLine[1]))).ToList();
        }

        private (bool success, int accumulatorValue) SolveInstructions()
        {
            var executedInstructions = new HashSet<int>();
            var instructionPointer = 0;
            var accumulatorValue = 0;

            while (true)
            {
                executedInstructions.Add(instructionPointer);
                var instruction = _input[instructionPointer];
                switch (instruction.Name)
                {
                    case "acc":
                        instructionPointer++;
                        accumulatorValue += instruction.Value;
                        break;
                    case "jmp":
                        instructionPointer += instruction.Value;
                        break;
                    case "nop":
                        instructionPointer++;
                        break;
                }

                if (instructionPointer >= _input.Count)
                {
                    return (true, accumulatorValue);
                }
                if (executedInstructions.Contains(instructionPointer))
                {
                    return (false, accumulatorValue);
                }
            }
        }

        internal class AssemblyInstruction
        {
            public string Name { get; }
            public int Value { get; }

            public AssemblyInstruction(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}
