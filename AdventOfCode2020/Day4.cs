using AdventOfCode2020.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4 : Day<long>
    {
        private Dictionary<string, Func<string, bool>> passportFieldsNamesValidation;

        private record PassportField(string Field, string Value);
        private record Passport(IEnumerable<PassportField> Fields);
        private IEnumerable<Passport> passports;

        public Day4() : base(4)
        {
            passportFieldsNamesValidation = new Dictionary<string, Func<string, bool>>()
            {
                ["byr"] = IsValidBirthYear,
                ["iyr"] = IsValidIssueYear,
                ["eyr"] = IsValidExpirationYear,
                ["hgt"] = IsValidHeight,
                ["hcl"] = IsValidHairColor,
                ["ecl"] = IsValidEyeColor,
                ["pid"] = IsValidPasswordId
            };

            passports = input
                .SplitByEmptyLines(ConvertToPassportFields)
                .Select(pf => new Passport(pf));
        }

        protected override long GetAnswer1()
            => passports.Where(AllFieldsAreValid1).Count();

        protected override long GetAnswer2()
            => passports.Where(AllFieldsAreValid2).Count();

        #region Passport convertions

        private IEnumerable<PassportField> ConvertToPassportFields(IEnumerable<string> passportLines)
            => passportLines
                .SelectMany(passportFields => passportFields.Split(' '))
                .Select(ConvertToPassportField);

        private PassportField ConvertToPassportField(string field)
            => new PassportField(
                field.Split(':')[0],
                field.Split(':')[1]);

        #endregion

        #region Validation methods

        private bool AllFieldsAreValid1(Passport passport)
            => passport.Fields.Count(f => !f.Field.Equals("cid")) == 7;

        private bool AllFieldsAreValid2(Passport passport)
        {
            if (!AllFieldsAreValid1(passport))
            {
                return false;
            }

            return passportFieldsNamesValidation
                .Join(passport.Fields,
                    fnv => fnv.Key,
                    f => f.Field,
                    (fnv, f) => fnv.Value(f.Value))
                .All(v => v);
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

        #endregion

    }

}
