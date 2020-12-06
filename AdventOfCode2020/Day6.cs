using AdventOfCode2020.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day6 : Day<int>
    {
        public Day6() : base(6)
        {
        }

        protected override int GetAnswer1()
        {
            return input
                .SplitByEmptyLines(GetYesAnswersForAnyone)
                .SelectMany(s => s)
                .Count();
        }

        protected override int GetAnswer2()
        {
            return input
                .SplitByEmptyLines(GetYesAnswersForEveryone)
                .SelectMany(s => s)
                .Count();
        }

        public IEnumerable<char> GetYesAnswersForAnyone(IEnumerable<string> personAnswers)
        {
            return personAnswers
                .SelectMany(answers => answers)
                .Distinct();
        }

        public IEnumerable<char> GetYesAnswersForEveryone(IEnumerable<string> personAnswers)
        {
            return personAnswers
                .SelectMany(answers => answers)
                .GroupBy(answer => answer)
                .Where(groupedAnswer => groupedAnswer.Count() == personAnswers.Count())
                .Select(yesEveryoneGroupedAnswer => yesEveryoneGroupedAnswer.Key);
        }


    }
}
