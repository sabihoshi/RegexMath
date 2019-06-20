using System.Text.RegularExpressions;

namespace RegexMath.Cleanup
{
    public sealed class Parenthesis : RegexBase
    {
        public Parenthesis()
            : base(Pattern) { }

        /* language=REGEXP */

        private static string Pattern { get; } =
            @"((?<=^|[(^*/+-])                    # make sure to start at a lower order
              (?<bracket>[(])                     # save 'bracket' if there is one
              (?<x>
                (?<int>(?(bracket)[+-]?)[0-9,]+)? # match integer or commas
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |         # make decimal optional if there is an 'int'
                           [.][0-9]+) )           # else make decimal required
                (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)]))        # if there is an opening bracket, include a closing one
              (?=$|[)^*/+-]))+                    # make sure to end at a lower order";

        protected override string MatchEvaluator(Match match)
        {
            return match.Groups["x"].Value;
        }
    }
}