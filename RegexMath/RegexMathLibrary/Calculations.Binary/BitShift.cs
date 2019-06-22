using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations.Binary
{
    public sealed class BitShift : BinaryCalculation
    {
        public BitShift() : base(Pattern) { }

        private static string Pattern { get; } =
            $@"(?<x>{Int}) (?<operation><<|>>) (?<x>{UInt})";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            switch (operation)
            {
                case "<<": return (x, y) => (int)x << (int)y;
                case ">>": return (x, y) => (int)x >> (int) y;
                default: throw new InvalidOperationException($"The operation '{operation}' does not exist.");
            }
        }
    }
}
