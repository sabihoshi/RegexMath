using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class BinaryDifferential : UnaryCalculation
    {
        public BinaryDifferential()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"I_(?<operation>[0-1])[(]{Number}[)] -
               L_\k<operation>      [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "0": return SpecialFunctions.BesselI0MStruveL0;
                case "1": return SpecialFunctions.BesselI1MStruveL1;


                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
