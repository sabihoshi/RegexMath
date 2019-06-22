using System;
using System.Text.RegularExpressions;

namespace RegexMath.Cleanup
{
    public class Constants : RegexBase
    {
        public Constants() 
            : base(Pattern, RegexOptions.None, false)
        {
        }

        /* language=REGEXP */
        private static string Pattern { get; } = 
            $@"(Math[.])?(
               (?<constant>PI) |

               (?<!{Number}{Decimal})
                 (?<constant>E)(?![+-]?[0-9]+)) # don't match if there is an exponent";

        protected override string MatchEvaluator(Match match)
        {
            var constant = match.Groups["constant"].Value.ToUpper();
            var integer = string.IsNullOrWhiteSpace(match.Groups["int"].Value);
            var exponent = string.IsNullOrWhiteSpace(match.Groups["exponent"].Value);
            switch (constant)
            {
                case "PI": return $"({Math.PI})";
                case "E":  return integer || exponent ? $"({Math.E})" : "e";
                default:   return string.Empty;
            }
        }
    }
}