using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4 : Day<long>
    {
        private record PassportField(string Field, string Value);
        private record Passport(List<PassportField> Fields);
        private IEnumerable<Passport> passports;

        public Day4() : base(4)
        {
            passports = input
                .Aggregate((p, s)
                    => string.IsNullOrEmpty(s)
                        ? string.Join(',', p, s)
                        : string.Join(' ', p, s))
                .Split(',', StringSplitOptions.TrimEntries)
                .Select(passportInfo
                    => new Passport(passportInfo
                            .Split(' ')
                            .Select(kv
                                => new PassportField(
                                        kv.Split(':')[0],
                                        kv.Split(':')[1]))
                            .ToList()));
        }

        protected override long GetAnswer1()
        {
            return passports.Where(p => HasValidFileds1(p.Fields)).Count();
        }

        protected override long GetAnswer2()
        {
            return passports.Where(p => HasValidFileds2(p.Fields)).Count();
        }

        private bool HasValidFileds1(List<PassportField> fields)
        {
            return fields.Count(f => !f.Field.Equals("cid")) == 7;
        }

        private bool HasValidFileds2(List<PassportField> fields)
        {
            if (!HasValidFileds1(fields))
            {
                return false;
            }

            var passportFieldsNamesValidation = new Dictionary<string, Func<string, bool>>()
            {
                ["byr"] = IsValidBirthYear,
                ["iyr"] = IsValidIssueYear,
                ["eyr"] = IsValidExpirationYear,
                ["hgt"] = IsValidHeight,
                ["hcl"] = IsValidHairColor,
                ["ecl"] = IsValidEyeColor,
                ["pid"] = IsValidPasswordId
            };

            return passportFieldsNamesValidation
                .Join(fields,
                    pn => pn.Key,
                    f => f.Field,
                    (pn, f) => pn.Value(f.Value))
                .All(v => v == true);
        }

        private bool IsValidPasswordId(string pid) => Regex.Match(pid, @"\b[0-9]{9}\b").Success;

        private bool IsValidEyeColor(string ecl) => Regex.Match(ecl, @"\bamb\b|\bblu\b|\bbrn\b|\bgry\b|\bgrn\b|\bhzl\b|\both\b").Success;

        private bool IsValidHairColor(string hcl) => Regex.Match(hcl, @"#[0-9a-f]{6}").Success;

        private bool IsValidHeight(string hgt)
        {
            var matches = Regex.Match(hgt, @"(?<height>[0-9]+)\s*(?<type>in|cm)");
            if (!matches.Success)
            {
                return false;
            }

            var height = int.Parse(matches.Groups["height"].Value);

            return matches.Groups["type"].Value.Equals("cm")
                ? height >= 150 && height <= 193
                : height >= 59 && height <= 76;
        }

        private bool IsValidExpirationYear(string eyr)
        {
            var year = int.Parse(eyr);
            return year >= 2020 && year <= 2030;
        }

        private bool IsValidIssueYear(string iyr)
        {
            var year = int.Parse(iyr);
            return year >= 2010 && year <= 2020;
        }

        private bool IsValidBirthYear(string byr)
        {
            var year = int.Parse(byr);
            return year >= 1920 && year <= 2002;
        }



    }
}
