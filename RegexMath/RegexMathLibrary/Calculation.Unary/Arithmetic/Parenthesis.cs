﻿using System.Text.RegularExpressions;
using RegexMath.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public sealed class Parenthesis : RegexBase
    {
        public Parenthesis()
            : base(Pattern, recursive: false) { }

        /* language=REGEXP */

        private static string Pattern { get; } =
            $@"((?<=^|[(^*/+-]) # make sure to start at a lower order
               [(]{Number}[)]
               (?=$|[)^*/+-]))+ # make sure to end at a lower order";

        protected override string MatchEvaluator(Match match)
        {
            return match.Groups["x"].Value;
        }
    }
}