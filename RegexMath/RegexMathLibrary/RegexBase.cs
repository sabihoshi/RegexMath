using System.Text.RegularExpressions;

namespace RegexMath
{
    public abstract class RegexBase : IOperation
    {
        private readonly bool _repeat;

        protected RegexBase(string pattern, RegexOptions options = RegexOptions.None, bool repeat = true)
        {
            _repeat = repeat;
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

        public virtual bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return _repeat && Regex.IsMatch(input);
        }

        protected abstract string MatchEvaluator(Match match);
    }
}