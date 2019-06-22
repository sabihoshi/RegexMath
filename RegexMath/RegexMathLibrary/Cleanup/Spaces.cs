using System.Text.RegularExpressions;

namespace RegexMath.Cleanup
{
    public class Spaces : RegexBase
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