using System.Text.RegularExpressions;

namespace RegexMath.Operation
{
    public abstract class RegexBase : IOperation
    {
        protected RegexBase(string pattern, RegexOptions options = RegexOptions.IgnoreCase, bool recursive = true)
        {
            _recursive = recursive;
            options |= RegexOptions.Compiled
                     | RegexOptions.IgnorePatternWhitespace
                     | RegexOptions.ExplicitCapture;
            Regex = new Regex(pattern, options);
        }

        private readonly bool _recursive;
        private Regex Regex { get; }

        protected static string UInt { get; } =
            @"(?<int>(?(bracket)[+-]?)[0-9,]+)  (?# match integer or commas)";

        protected static string Int { get; } =
            @"(?<int>[+-]?[0-9,]+)              (?# allow sign for right side)";

        protected static string Decimal { get; } =
            @"(?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |       (?# make decimal optional if there is an 'int')
                           [.][0-9]+) )         (?# else make decimal required)";

        protected static string Exponent { get; } =
            @"(?<exponent>e[+-]?[0-9]+)?";

        protected static string UNumber { get; } =
            $@"(?<bracket>[(])*
               (?<x>{UInt}?{Decimal}{Exponent})  (?# save 'x' as the full number)
               (?(bracket)(?<-bracket>[)])+)    (?# balance brackets)";

        protected static string Number { get; } =
            $@"(?<bracket>[(])*
               (?<x>{Int}?{Decimal}{Exponent})   (?# save 'x' as the full number)
               (?(bracket)(?<-bracket>[)])+)    (?# balance brackets)";

        public virtual string Evaluate(string input)
        {
            // If recursive is not used, then make sure to replace everything
            // Otherwise only match one at a time
            return Regex.Replace(input, MatchEvaluator, _recursive ? 1 : -1);
        }

        public virtual bool TryEvaluate(string input, out string result)
        {
            result = Evaluate(input);
            return _recursive && Regex.IsMatch(input);
        }

        protected abstract string MatchEvaluator(Match match);
    }
}