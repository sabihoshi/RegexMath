using System;
using System.Text.RegularExpressions;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public class Constants : RegexBase
    {
        public Constants()
            : base(Pattern) { }

        private static string Pattern { get; } =
            $@"(?<{Token.Constant}>[π|ℇ]|epsilon) |
               ((Math[.])?(
               (?<{Token.Constant}>(?i:PI)) |

               (?<!{Number}{Decimal}) 
                 (?<{Token.Constant}>E)
               (?![+-]?[0-9]+))) | 

               (?<=Log_)(?<{Token.Constant}>E)";

        protected override string MatchEvaluator(Match match)
        {
            var constant = match.Groups[$"{Token.Constant}"].Value.ToUpper();
            var integer = string.IsNullOrWhiteSpace(match.Groups[$"{Token.Int}"].Value);
            var exponent = string.IsNullOrWhiteSpace(match.Groups[$"{Token.Exponent}"].Value);
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