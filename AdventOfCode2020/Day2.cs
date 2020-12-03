using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day2 : Day<int>
    {
        private record PasswordPolicy(int Min, int Max, char Letter);
        private record PasswordInfo(PasswordPolicy PasswordPolicy, string Password);
        
        private IEnumerable<PasswordInfo> passwordsInfo;

        public Day2() : base(2)
        {
            passwordsInfo = input
                .Select(s => s.Split(':'))
                .Select(pp => new PasswordInfo(GetPolicyFrom(pp[0]), pp[1].Trim()));
        }

        protected override int GetAnswer1()
            => passwordsInfo.Where(IsValid1).Count();

        protected override int GetAnswer2()
            => passwordsInfo.Where(IsValid2).Count();

        private bool IsValid1(PasswordInfo pi)
        {
            var letterCount = pi.Password.Count(c => c == pi.PasswordPolicy.Letter);

            return letterCount >= pi.PasswordPolicy.Min
                && letterCount <= pi.PasswordPolicy.Max;
        }

        private bool IsValid2(PasswordInfo pi)
        {
            var foundAtMinPosition = pi.Password[pi.PasswordPolicy.Min - 1] == pi.PasswordPolicy.Letter;
            var foundAtMaxPosition = pi.Password[pi.PasswordPolicy.Max - 1] == pi.PasswordPolicy.Letter;

            return foundAtMinPosition ^ foundAtMaxPosition;
        }

        private PasswordPolicy GetPolicyFrom(string policy)
        {
            var minMaxLetter = policy.Split(' ');
            var letter = minMaxLetter[1];
            var minMax = minMaxLetter[0].Split('-');

            return new PasswordPolicy(int.Parse(minMax[0]), int.Parse(minMax[1]), letter[0]);
        }

    }
}
