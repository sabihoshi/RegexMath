using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations
{
    public abstract class UnaryCalculation : RegexBase
    {
        protected UnaryCalculation(string pattern,
                                  RegexOptions options = RegexOptions.IgnoreCase,
                                  bool recursive = true)
                           : base(pattern, options, recursive)
        {
        }

        protected abstract Func<double, double> GetOperation(string operation = null);

        protected override string MatchEvaluator(Match match)
        {
            var operation = GetOperation(match.Groups["operation"].Value);
            var x = match.Groups["x"].Value;

            if(double.TryParse(x, out var number))
                return operation(number).ToString();
            return x;
        }
    }
}