using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations
{
    public abstract class BinaryCalculation : RegexBase
    {
        protected BinaryCalculation(string pattern,
                                    RegexOptions options = RegexOptions.IgnoreCase,
                                  bool recursive = true)
                           : base(pattern, options, recursive)
        {
        }

        protected abstract Func<double, double, double> GetOperation(string operation = null);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups["operation"].Value);
            var numbers = match.Groups["x"].Captures.Cast<Capture>()
                               .Where(x => double.TryParse(x.Value, out _))
                               .Select(x => double.Parse(x.Value));
            return numbers.Aggregate(operation).ToString();
        }
    }
}