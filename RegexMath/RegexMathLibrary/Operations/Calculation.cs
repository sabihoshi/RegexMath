using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMathLibrary.Operations
{
    public abstract class Calculation : IOperation
    {
        protected Calculation()
        {
            Options = RegexOptions.Compiled
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.IgnoreCase
                    | RegexOptions.ExplicitCapture;
            Regex = new Regex(Pattern, Options);
        }

        protected abstract string Operator { get; set; }

        protected RegexOptions Options { get; }

        // language=REGEXP
        public virtual string Pattern => @"(?<expression>(?<number>(?<bracket>[(])?(?<x>[+-]?(?<int>[0-9,]+)?[.]?(?<decimal>(?(int)(?<-int>[0-9]*)|([0-9]+))([+-]e[0-9]+)?))(?(bracket)(?<-bracket>[)])))" + Operator + @"){2,}";

        public Regex Regex { get; protected set; }

        public virtual bool TryEvaluate(ref string input)
        {
            if (!Regex.IsMatch(input)) return false;
            input = Regex.Replace(input, match =>
            {
                var numbers = match.Groups["x"].Captures
                                   .Select(y => double.Parse(y.Value));
                return Calculate(numbers).ToString(CultureInfo.CurrentCulture);
            }, 1);
            return true;
        }

        protected double Calculate(IEnumerable<double> numbers)
        {
            return numbers.Aggregate(Operation);
        }

        protected abstract double Operation(double x, double y);
    }
}