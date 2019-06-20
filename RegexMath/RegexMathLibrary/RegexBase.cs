using System.Text.RegularExpressions;

namespace RegexMath
{
    public abstract class RegexBase : IOperation
    {
        protected RegexBase(string pattern, RegexOptions options = RegexOptions.None)
        {
            options |= RegexOptions.Compiled
                     | RegexOptions.IgnorePatternWhitespace
                     | RegexOptions.IgnoreCase
                     | RegexOptions.ExplicitCapture;
            Regex = new Regex(pattern, options);
        }

        private Regex Regex { get; }

        public virtual string Evaluate(string input)
        {
            return Regex.Replace(input, MatchEvaluator);
        }

        public abstract string MatchEvaluator(Match match);

        public bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return Regex.IsMatch(input);
        }
    }
}