using System;
using System.Text.RegularExpressions;

namespace RegexMath.Cleanup
{
    public class Constants : RegexBase
    {
        public Constants() : base(Pattern, repeat: false)
        {
        }

        private static string Pattern { get; } = 
            @"Math[.](?<constant>PI|E)";

        protected override string MatchEvaluator(Match match)
        {
            var constant = match.Groups["constant"].Value.ToUpper();
            switch (constant)
            {
                case "PI": return Math.PI.ToString();
                case "E": return Math.E.ToString();
                default: return string.Empty;
            }
        }
    }
}