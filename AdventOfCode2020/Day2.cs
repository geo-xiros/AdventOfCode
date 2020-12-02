using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day2 : Day<int>
    {
        private IEnumerable<(string Policy, string Password)> passwordsInfo;

        public Day2() : base(2)
        {
            passwordsInfo = input
                .Select(s => s.Split(':'))
                .Select(pp => (pp[0], pp[1].Trim()));
        }

        protected override int GetAnswer1()
            => passwordsInfo.Where(pi => IsValid1(GetPolicyFrom(pi.Policy), pi.Password)).Count();

        protected override int GetAnswer2() 
            => passwordsInfo.Where(pi => IsValid2(GetPolicyFrom(pi.Policy), pi.Password)).Count();

        private bool IsValid1((int Min, int Max, char Letter) policy, string password)
        {
            var letterCount = password.Count(c => c == policy.Letter);

            return letterCount >= policy.Min
                && letterCount <= policy.Max;
        }

        private bool IsValid2((int Min, int Max, char Letter) policy, string password)
        {
            var foundAtMinPosition = password[policy.Min - 1] == policy.Letter;
            var foundAtMaxPosition = password[policy.Max - 1] == policy.Letter;

            return (foundAtMinPosition || foundAtMaxPosition) &&
                !(foundAtMinPosition && foundAtMaxPosition);
        }

        private (int Min, int Max, char Letter) GetPolicyFrom(string policy)
        {
            var minMaxLetter = policy.Split(' ');
            var letter = minMaxLetter[1];
            var minMax = minMaxLetter[0].Split('-');

            return (int.Parse(minMax[0]), int.Parse(minMax[1]), letter[0]);
        }

    }
}
