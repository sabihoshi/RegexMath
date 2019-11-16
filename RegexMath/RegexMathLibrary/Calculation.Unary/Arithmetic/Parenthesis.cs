using System.Text.RegularExpressions;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public sealed class Parenthesis : RegexBase
    {
        public Parenthesis()
            : base(Pattern) { }

        private static string Pattern { get; } =
            $@"((?<=^|[(^*/+-])
               [(]{Number}[)]
               (?=$|[)^*/+-]))+";

        protected override string MatchEvaluator(Match match) => match.Groups[$"{Token.Number}"].Value;
    }
}