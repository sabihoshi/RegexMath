using System.Text.RegularExpressions;

namespace RegexMathLibrary.Operations
{
    public sealed class Parenthesis : IOperation
    {
        public Parenthesis()
        {
            Options = RegexOptions.Compiled
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.IgnoreCase
                    | RegexOptions.ExplicitCapture;
            Regex = new Regex(Pattern, Options);
        }

        private RegexOptions Options { get; }

        public string Pattern { get; } = /* language=REGEXP */ @"
            (?<bracket>\()
            (?<x>[+-]?
                (?<int>[0-9,]+)?\.?
                (?<decimal>(?(int)(?<-int>[0-9]*)|([0-9]+))([+-]e[0-9]+)?))
            (?(bracket)(?<-bracket>\)))";

        public Regex Regex { get; }

        public bool TryEvaluate(ref string input)
        {
            if (!Regex.IsMatch(input)) return false;
            input = Regex.Replace(input, match => match.Groups["x"].Value);
            return true;
        }

    }
}