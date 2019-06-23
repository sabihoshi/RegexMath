using System;
using System.Text.RegularExpressions;
using RegexMath.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public class Constants : RegexBase
    {
        public Constants()
            : base(Pattern, recursive:false) { }

        private static string Pattern { get; } =
            $@"(?<constant>[π|ℇ]|epsilon) |
               ((Math[.])?(
               (?<constant>(?i:PI)) |

               (?<!{UNumber}{Decimal}) # don't include number or decimal
                 (?<constant>E)
               (?![+-]?[0-9]+))) |     # don't include if scientific notation

               (?<=Log_)(?<constant>E)";

        protected override string MatchEvaluator(Match match)
        {
            var constant = match.Groups["constant"].Value.ToUpper();
            var integer = string.IsNullOrWhiteSpace(match.Groups["int"].Value);
            var exponent = string.IsNullOrWhiteSpace(match.Groups["exponent"].Value);
            switch (constant)
            {
                case "π":
                case "PI": return $"({Math.PI})";
                case "ℇ":
                case "epislon": return $"({double.Epsilon})";
                case "E": return integer || exponent ? $"({Math.E})" : "e";
                default: return string.Empty;
            }
        }
    }
}