using System.Text.RegularExpressions;

namespace RegexMath.Calculation.Operation
{
    public abstract class RegexBase : IOperation
    {
        protected RegexBase(string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            options |= RegexOptions.Compiled
                     | RegexOptions.IgnorePatternWhitespace
                     | RegexOptions.ExplicitCapture;
            Regex = new Regex(pattern, options);
        }

        public enum Token
        {
            Operator,
            Bracket,
            Number,
            Int,
            Exponent,
            Decimal,
            Constant
        }

        private Regex Regex { get; }

        protected static string UInt { get; } =
            $@"(?<{Token.Int}>(?({Token.Bracket})[+-]?)[0-9,]+)";

        protected static string Int { get; } =
            $@"(?<{Token.Int}>[+-]?[0-9,]+)";

        protected static string Decimal { get; } =
            $@"(?<{Token.Decimal}>(?({Token.Int})
                  (?<-{Token.Int}>([.][0-9]*)?) |
                                   [.][0-9]+) )";

        protected static string Exponent { get; } =
            $@"(?<{Token.Exponent}>e[+-]?[0-9]+)?";

        protected static string UNumber { get; } =
            $@"(?<{Token.Bracket}>[(])*
               (?<{Token.Number}>{UInt}?{Decimal}{Exponent})
               (?({Token.Bracket})(?<-{Token.Bracket}>[)])+)";

        protected static string Number { get; } =
            $@"(?<{Token.Bracket}>[(])*
               (?<{Token.Number}>{Int}?{Decimal}{Exponent})
               (?({Token.Bracket})(?<-{Token.Bracket}>[)])+)";

        public virtual string Evaluate(string input)
        {
            // If recursive is not used, then make sure to replace everything
            // Otherwise only match one at a time
            return Regex.Replace(input, MatchEvaluator, 1);
        }

        public virtual bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return Regex.IsMatch(input);
        }

        protected abstract string MatchEvaluator(Match match);
    }
}