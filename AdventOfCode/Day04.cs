using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    class Day04 : BaseDay
    {
        private readonly List<Passport> _input;

        public Day04()
        {
            _input = ParseInput();
        }

        public override string Solve_1()
        {
            return _input.Count(passport => passport.IsComplete()).ToString();
        }

        public override string Solve_2()
        {
            return _input.Count(passport => passport.IsValid()).ToString();
        }

        private List<Passport> ParseInput()
        {
            var passportLines = new List<string>();
            var passports = new List<Passport>();

            foreach (var line in File.ReadAllLines(InputFilePath))
            {
                if (line.Equals(""))
                {
                    var passportInfo = new Dictionary<string, string>();
                    foreach (var passportLine in passportLines)
                    {
                        foreach (var pair in passportLine.Split(" "))
                        {
                            var keyValueArray = pair.Split(":");
                            passportInfo.Add(keyValueArray[0], keyValueArray[1]);
                        }
                    }
                    passports.Add(new Passport(passportInfo));
                    passportLines.Clear();
                    continue;
                }
                passportLines.Add(line);
            }
            return passports;
        }
    }

    internal class Passport
    {
        public int? BirthYear { get; }
        public int? IssueYear { get; }
        public int? ExpirationYear { get; }
        public string Height { get; }
        public string HairColor { get; }
        public string EyeColor { get; }
        public string PasswordId { get; }
        public string CountryId { get; }

        public Passport(IDictionary<string, string> passportInfo)
        {
            foreach (var (key, value) in passportInfo)
            {
                switch (key)
                {
                    case "byr":
                        BirthYear = Convert.ToInt32(value);
                        break;
                    case "iyr":
                        IssueYear = Convert.ToInt32(value);
                        break;
                    case "eyr":
                        ExpirationYear = Convert.ToInt32(value);
                        break;
                    case "hgt":
                        Height = value;
                        break;
                    case "hcl":
                        HairColor = value;
                        break;
                    case "ecl":
                        EyeColor = value;
                        break;
                    case "pid":
                        PasswordId = value;
                        break;
                    case "cid":
                        CountryId = value;
                        break;
                }
            }
        }

        public bool IsComplete()
        {
            return BirthYear != null && IssueYear != null && ExpirationYear != null && Height != null &&
                   HairColor != null && EyeColor != null && PasswordId != null;
        }

        public bool IsValid()
        {
            var validPasswordIdRegex = new Regex("^[0-9]{9}$", RegexOptions.Compiled);
            string[] validEyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            var validHairColorRegex = new Regex("^#[0-9a-f]{6}$", RegexOptions.Compiled);

            if (!IsComplete()) return false;
            if (BirthYear < 1920 || BirthYear > 2002)
                return false;
            if (IssueYear < 2010 || IssueYear > 2020)
                return false;
            if (ExpirationYear < 2020 || ExpirationYear > 2030)
                return false;
            if (!validPasswordIdRegex.IsMatch(PasswordId))
                return false;
            if (!validEyeColors.Contains(EyeColor))
                return false;
            if (!validHairColorRegex.IsMatch(HairColor))
                return false;

            if (!Height.EndsWith("cm") && !Height.EndsWith("in"))
                return false;

            var heightNumber = Convert.ToInt32(Height.Substring(0, Height.Length - 2));

            if (Height.EndsWith("in") && (heightNumber < 59 || heightNumber > 76))
                return false;
            if (Height.EndsWith("cm") && (heightNumber < 150 || heightNumber > 193))
                return false;

            return true;
        }
    }
}
