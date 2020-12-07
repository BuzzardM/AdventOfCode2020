using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    class Day07 : BaseDay
    {

        private readonly Dictionary<string, Bag> _input;
        private const string BagName = "shiny gold";
        private readonly Regex _childrenFinderRegex = new Regex(@"(?:[\d].*?) bag+", RegexOptions.Compiled);

        public Day07()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            return GetChildBags(BagName).Count.ToString();
        }

        public override string Solve_2()
        {
            return GetChildBagsAmount(BagName).ToString();
        }

        private Dictionary<string, Bag> ParseInput()
        {
            var result = new Dictionary<string, Bag>();
            foreach (var line in File.ReadAllLines(InputFilePath))
            {
                var splitTrim = line.Split("contain");
                var parentName = splitTrim[0].Replace("bags", "").Trim();
                var children = splitTrim[1];
                if (!result.ContainsKey(parentName))
                    result.Add(parentName, new Bag(parentName));

                foreach (Match child in _childrenFinderRegex.Matches(children))
                {
                    var childString = child.Value.Replace("bag", "").Trim();
                    var amountOfBags = Convert.ToInt32(childString.Substring(0, childString.IndexOf(' ')));
                    var childName = childString[(childString.IndexOf(' ') + 1)..];
                    if (!result.TryGetValue(childName, out var bag))
                    {
                        bag = new Bag(childName);
                        result.Add(childName, bag);
                    }

                    result[parentName].Children.Add(bag, amountOfBags);
                }
            }

            return result;
        }

        private HashSet<Bag> GetChildBags(string bagName)
        {
            var result = new HashSet<Bag>();
            foreach (var bag in _input.Values.Where(bag => bag.Children.Any(children => children.Key.Name == bagName)))
            {
                result.Add(bag);
                foreach (var childBag in GetChildBags(bag.Name))
                {
                    result.Add(childBag);
                }
            }
            return result;
        }

        private int GetChildBagsAmount(string bagName)
        {
            var bag = _input[bagName];
            return bag.Children.Sum(child => child.Value) + bag.Children.Sum(child => child.Value * GetChildBagsAmount(child.Key.Name));
        }

        internal class Bag
        {
            public string Name { get; }
            public Dictionary<Bag, int> Children { get; }

            public Bag(string name)
            {
                Name = name;
                Children = new Dictionary<Bag, int>();
            }
        }
    }
}
