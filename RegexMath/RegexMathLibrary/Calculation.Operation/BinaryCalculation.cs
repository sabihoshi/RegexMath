using System;
using System.Linq;
using System.Text.RegularExpressions;
using RegexMath.Operation;

namespace RegexMath.Calculation.Operation
{
    public abstract class BinaryCalculation : RegexBase
    {
        protected BinaryCalculation(string pattern,
                                    RegexOptions options = RegexOptions.IgnoreCase,
                                  bool recursive = true, bool brackets = false)
                           : base(pattern, options, recursive)
        {
            _brackets = brackets;
        }

        private readonly bool _brackets;

        protected abstract Func<double, double, double> GetOperation(string operation);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups["operation"].Value);
            var numbers = match.Groups["x"].Captures.Cast<Capture>()
                               .Where(x => double.TryParse(x.Value, out _))
                               .Select(x => double.Parse(x.Value));
            var result = numbers.Aggregate(operation).ToString();
            return _brackets ? $"({result})" : result;
        }
    }
}