using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMath.Operations
{
    public abstract class Calculation
    {
        protected Calculation(string pattern, RegexOptions options = RegexOptions.None)
        {
            options |= RegexOptions.Compiled
                     | RegexOptions.IgnorePatternWhitespace
                     | RegexOptions.IgnoreCase
                     | RegexOptions.ExplicitCapture;
            Regex = new Regex(pattern, options);
        }

        private Regex Regex { get; }

        public string Evaluate(string input)
        {
            return Regex.Replace(input, Replace);
        }

        public bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return Regex.IsMatch(input);
        }

        protected virtual Func<double, double, double> GetOperation(string operation = null)
        {
            throw new NotImplementedException();
        }

        protected virtual string Replace(Match match)
        {
            var operation = GetOperation(match.Groups["operation"].Value);
            var numbers = match.Groups["x"].Captures.Cast<Capture>()
                               .Where(x => double.TryParse(x.Value, out _))
                               .Select(x => double.Parse(x.Value));
            return numbers.Aggregate(operation).ToString(CultureInfo.CurrentCulture);
        }
    }
}