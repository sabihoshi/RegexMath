using System;
using System.Text.RegularExpressions;

namespace RegexMath.Calculation.Operation
{
    public abstract class UnaryCalculation : RegexBase
    {
        protected UnaryCalculation(string pattern,
                                  RegexOptions options = RegexOptions.IgnoreCase, bool brackets = false)
                           : base(pattern, options)
        {
            _brackets = brackets;
        }

        private readonly bool _brackets;

        protected abstract Func<double, double> GetOperation(string operation);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups[$"{Token.Operator}"].Value);
            var number = match.Groups[$"{Token.Number}"].Value;

            if (double.TryParse(number, out var result))
                return operation(result).ToString();
            return _brackets ? $"({number})" : number;
        }
    }
}