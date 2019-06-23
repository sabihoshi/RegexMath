﻿using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class Factorial : UnaryCalculation
    {
        public Factorial() 
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"{Number}! |
               (?<operation>Factorial(Ln)?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "factorialln": return x => SpecialFunctions.GammaLn(x + 1);
                default: return x => SpecialFunctions.Gamma(x + 1);
            }
        }
    }
}