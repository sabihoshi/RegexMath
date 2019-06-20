using System.Text.RegularExpressions;

namespace RegexMath.Replace
{
    public sealed class Parenthesis : RegexBase
    {
        public Parenthesis()
            : base(Pattern) { }

        /* language=REGEXP */

        private static string Pattern { get; } =
            @"((?<=^|[(^*/+-])                    # make sure to start at a lower order
              (?<bracket>[(])+                    
              (?<x>
                (?<int>(?(bracket)[+-]?)[0-9,]+)? # match integer or commas
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |         # make decimal optional if there is an 'int'
                           [.][0-9]+) )           # else make decimal required
                (?<exponent>e[+-]?[0-9]+)?)
              (?<-bracket>[)])*                   # balance brackets
              (?=$|[)^*/+-]))+                    # make sure to end at a lower order";

        public override string MatchEvaluator(Match match)
        {
            return match.Groups["x"].Value;
        }
    }
}