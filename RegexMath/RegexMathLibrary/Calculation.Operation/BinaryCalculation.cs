using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMath.Calculation.Operation
{
    public abstract class BinaryCalculation : RegexBase
    {
        private readonly bool _brackets;

        protected BinaryCalculation(string pattern,
            RegexOptions options = RegexOptions.IgnoreCase, bool brackets = false)
            : base(pattern, options) =>
            _brackets = brackets;

        protected abstract Func<double, double, double> GetOperation(string operation);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups[$"{Token.Operator}"].Value);
            var numbers = match.Groups[$"{Token.Number}"]
               .Captures
               .Where(x => double.TryParse(x.Value, out _))
               .Select(x => double.Parse(x.Value));
            string result = numbers.Aggregate(operation).ToString();
            return _brackets ? $"({result})" : result;
        }
    }
}