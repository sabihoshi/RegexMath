using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public sealed class Log : BinaryCalculation
    {
        public Log()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"Log_{Number} [(]{Number}[)]|
               Log[(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return (newBase, n) => Math.Log(n, newBase);
        }
    }
}