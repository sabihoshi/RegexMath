using System.Text.RegularExpressions;
using RegexMath.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public sealed class Spaces : RegexBase
    {
        public Spaces()
            : base(Pattern, recursive: false) { }

        /* language=REGEXP */
        private static string Pattern { get; } = @"\s+";

        protected override string MatchEvaluator(Match match)
        {
            return string.Empty;
        }
    }
}