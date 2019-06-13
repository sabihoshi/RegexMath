namespace RegexMath.Operations
{
    public sealed class Parenthesis : Calculation
    {
        /* language=REGEXP */

        protected override string Pattern { get; } =
            @"(?<bracket>[(])
              (?<x>[+-]?
                  (?<int>(?(bracket)[+-]?)[0-9,]+)? # Integer
                  (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?(bracket)(?<-bracket>[)]))";

        public override bool TryEvaluate(ref string input)
        {
            if (!Regex.IsMatch(input)) return false;
            input = Regex.Replace(input, match => match.Groups["x"].Value);
            return true;
        }
    }
}