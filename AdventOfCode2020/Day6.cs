﻿using AdventOfCode2020.Helpers;
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
                .SplitByEmptyLines(GetAffirmativeAnswersFromAnyone)
                .SelectMany(answer => answer)
                .Count();
        }

        protected override int GetAnswer2()
        {
            return input
                .SplitByEmptyLines(GetAffirmativeAnswersFromEveryone)
                .SelectMany(answer => answer)
                .Count();
        }

        public IEnumerable<char> GetAffirmativeAnswersFromAnyone(IEnumerable<string> personAnswers)
        {
            return personAnswers
                .SelectMany(answers => answers)
                .Distinct();
        }

        public IEnumerable<char> GetAffirmativeAnswersFromEveryone(IEnumerable<string> personAnswers)
        {
            return personAnswers
                .SelectMany(answers => answers)
                .GroupBy(answer => answer)
                .Where(groupedAnswer => groupedAnswer.Count() == personAnswers.Count())
                .Select(yesEveryoneGroupedAnswer => yesEveryoneGroupedAnswer.Key);
        }


    }
}
