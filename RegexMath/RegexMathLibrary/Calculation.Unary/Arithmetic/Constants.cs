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
            string constant = match.Groups[$"{Token.Constant}"].Value.ToUpper();
            bool integer = string.IsNullOrWhiteSpace(match.Groups[$"{Token.Int}"].Value);
            bool exponent = string.IsNullOrWhiteSpace(match.Groups[$"{Token.Exponent}"].Value);
            return constant switch
            {
                "π"       => $"({Math.PI})",
                "PI"      => $"({Math.PI})",
                "ℇ"       => $"({double.Epsilon})",
                "epislon" => $"({double.Epsilon})",
                "E"       => (integer || exponent ? $"({Math.E})" : "e"),
                _         => string.Empty
            };
        }
    }
}