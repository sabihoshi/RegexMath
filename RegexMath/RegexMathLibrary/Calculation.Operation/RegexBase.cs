using System.Text.RegularExpressions;

namespace RegexMath.Calculation.Operation
{
    public abstract class RegexBase : IOperation
    {
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

        protected RegexBase(string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            options |= RegexOptions.Compiled
                     | RegexOptions.IgnorePatternWhitespace
                     | RegexOptions.ExplicitCapture;
            Regex = new Regex(pattern, options);
        }

        private Regex Regex { get; }

        protected static string Int { get; } =
            $@"(?<{Token.Int}>[+-]?[0-9,]+)";

        protected static string Decimal { get; } =
            $@"(?<{Token.Decimal}>(?({Token.Int})
                  (?<-{Token.Int}>([.][0-9]*)?) |
                                   [.][0-9]+) )";

        protected static string Exponent { get; } =
            $@"(?<{Token.Exponent}>e[+-]?[0-9]+)?";

        protected static string Number { get; } =
            $@"(?<{Token.Bracket}>[(])*
               (?<{Token.Number}>{Int}?{Decimal}{Exponent})
               (?({Token.Bracket})(?<-{Token.Bracket}>[)])+)";

        public virtual string Evaluate(string input) => Regex.Replace(input, MatchEvaluator, 1);

        public virtual bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return Regex.IsMatch(input);
        }

        protected abstract string MatchEvaluator(Match match);
    }
}