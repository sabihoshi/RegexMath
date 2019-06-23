using System;
using System.Text.RegularExpressions;
using RegexMath.Operation;

namespace RegexMath.Calculation.Operation
{
    public abstract class UnaryCalculation : RegexBase
    {
        protected UnaryCalculation(string pattern,
                                  RegexOptions options = RegexOptions.IgnoreCase,
                                  bool recursive = true, bool brackets = false)
                           : base(pattern, options, recursive)
        {
            _brackets = brackets;
        }

        private readonly bool _brackets;

        protected abstract Func<double, double> GetOperation(string operation);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups["operation"].Value);
            var x = match.Groups["x"].Value;

            if (double.TryParse(x, out var number))
                return operation(number).ToString();
            return _brackets ? $"({x})" : x;
        }
    }
}