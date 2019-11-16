using System.Text.RegularExpressions;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public sealed class Spaces : RegexBase
    {
        public Spaces()
            : base(Pattern) { }

        private static string Pattern { get; } = @"\s+";

        protected override string MatchEvaluator(Match match) => string.Empty;
    }
}