using System;
using System.Text.RegularExpressions;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public sealed class Signs : RegexBase
    {
        public Signs()
            : base(Pattern) { }

        private static string Pattern { get; } = @"[+-]{2}";

        protected override string MatchEvaluator(Match match)
        {
            return match.Value switch
            {
                "++" => "+",
                "--" => "+",
                "+-" => "-",
                "-+" => "-",
                _    => throw new InvalidOperationException($"Sign '{match.Value}' does not exist.")
            };
        }
    }
}