using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMath.Operations
{
    public abstract class Calculation
    {
        protected Calculation()
        {
            Regex = new Regex(Pattern, Options);
        }

        protected abstract string Pattern { get; }

        private Regex Regex { get; }

        protected virtual RegexOptions Options { get; } = RegexOptions.Compiled
                                                        | RegexOptions.IgnorePatternWhitespace
                                                        | RegexOptions.IgnoreCase
                                                        | RegexOptions.ExplicitCapture;

        public bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return Regex.IsMatch(input);
        }

        public string Evaluate(string input)
        {
            return Regex.Replace(input, Replace);
        }

        protected virtual string Replace(Match match)
        {
            var operation = GetOperation(match.Groups["operation"].Value);
            var numbers = match.Groups["x"].Captures
                               .Where(x => double.TryParse(x.Value, out _))
                               .Select(x => double.Parse(x.Value));
            return numbers.Aggregate(operation).ToString(CultureInfo.CurrentCulture);
        }

        protected virtual Func<double, double, double> GetOperation(string operation = null)
        {
            throw new NotImplementedException();
        }
    }
}