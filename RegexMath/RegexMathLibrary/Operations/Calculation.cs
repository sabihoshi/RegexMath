using System;
using System.Data;
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

        protected virtual RegexOptions Options => RegexOptions.Compiled
                                                | RegexOptions.IgnorePatternWhitespace
                                                | RegexOptions.IgnoreCase
                                                | RegexOptions.ExplicitCapture;

        protected abstract string Pattern { get; }

        protected Regex Regex { get; }

        public virtual bool TryEvaluate(ref string input)
        {
            if (!Regex.IsMatch(input)) return false;
            input = Evaluate(input);
            return true;
        }

        public string Evaluate(string input)
        {
            return Regex.Replace(input, match =>
            {
                var operation = GetOperation(match.Groups["operation"].Value);
                var numbers = match.Groups["x"].Captures
                                   .Select(x => x.Value)
                                   .Where(x => double.TryParse(x, out _))
                                   .Select(x => double.Parse(x));
                return numbers.Aggregate(operation).ToString(CultureInfo.CurrentCulture);
            }, 1);
        }

        protected virtual Func<double, double, double> GetOperation(string operation = null)
        {
            throw new NotImplementedException();
        }
    }
}