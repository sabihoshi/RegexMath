using System;
using System.Text.RegularExpressions;

namespace RegexMath.Calculation.Operation
{
    public abstract class UnaryCalculation : RegexBase
    {
        private readonly bool _brackets;

        protected UnaryCalculation(string pattern,
            RegexOptions options = RegexOptions.IgnoreCase, bool brackets = false)
            : base(pattern, options) =>
            _brackets = brackets;

        protected abstract Func<double, double> GetOperation(string operation);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups[$"{Token.Operator}"].Value);
            string number = match.Groups[$"{Token.Number}"].Value;

            if (double.TryParse(number, out double result))
                return operation(result).ToString();

            return _brackets ? $"({number})" : number;
        }
    }
}