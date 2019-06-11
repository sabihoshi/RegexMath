using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using RegexMath.Models;

namespace RegexMath
{
    public class RegexMath
    {
        private static readonly string AddRegex =
            @"(^|[+-])(-?(?:\d+\.\d*|\.?\d+))([+])([+-]?(?:\d+\.\d*|\.?\d+))(?=[+-]|$)|(^|[+-])\(([+-]?(?:\d+\.\d*|\.?\d+))\)([+])\(([+-]?(?:\d+\.\d*|\.?\d+))\)|(^|[+-])\(([+-]?(?:\d+\.\d*|\.?\d+))\)([+])(-?(?:\d+\.\d*|\.?\d+))(?=[+-]|$)|(^|[+-])(-?(?:\d+\.\d*|\.?\d+))([+])\((-?(?:\d+\.\d*|\.?\d+))\)(?=[+-]|$)|()\(([+-]?(?:\d+\.\d*|\.?\d+))([+])([+-]?(?:\d+\.\d*|\.?\d+))\)";

        private static readonly string ExponentRegex =
            @"(\d+\.\d*|\.?\d+)(\^|\*\*)([+-]?(?:\d+\.\d*|\.?\d+))|\((-?(?:\d+\.\d*|\.?\d+))\)(\^|\*\*)\(([+-]?(?:\d+\.\d*|\.?\d+))\)|\((-?(?:\d+\.\d*|\.?\d+))\)(\^|\*\*)([+-]?(?:\d+\.\d*|\.?\d+))|(\d+\.\d*|\.?\d+)(\^|\*\*)\(([+-]?(?:\d+\.\d*|\.?\d+))\)";

        private static readonly string SubtractRegex =
            @"(^|[+-])(-?(?:\d+\.\d*|\.?\d+))([-])([+-]?(?:\d+\.\d*|\.?\d+))(?=[+-]|$)|(^|[+-])\(([+-]?(?:\d+\.\d*|\.?\d+))\)([-])\(([+-]?(?:\d+\.\d*|\.?\d+))\)|(^|[+-])\((-?(?:\d+\.\d*|\.?\d+))\)([-])(-?(?:\d+\.\d*|\.?\d+))(?=[+-]|$)|(^|[+-])([+-]?(?:\d+\.\d*|\.?\d+))([-])\((-?(?:\d+\.\d*|\.?\d+))\)(?=[+-]|$)|()\(([+-]?(?:\d+\.\d*|\.?\d+))([-])([+-]?(?:\d+\.\d*|\.?\d+))\)";

        private static readonly string MultiplyRegex =
            @"(\d+\.\d*|\.?\d+)([*])([+-]?(?:\d+\.\d*|\.?\d+))|\((-?(?:\d+\.\d*|\.?\d+))\)([*])\(([+-]?(?:\d+\.\d*|\.?\d+))\)|\((-?(?:\d+\.\d*|\.?\d+))\)([*])([+-]?(?:\d+\.\d*|\.?\d+))|(\d+\.\d*|\.?\d+)([*])\(([+-]?(?:\d+\.\d*|\.?\d+))\)";

        private static readonly string DivideRegex =
            @"(\d+\.\d*|\.?\d+)([/])([+-]?(?:\d+\.\d*|\.?\d+))|\((-?(?:\d+\.\d*|\.?\d+))\)([/])\(([+-]?(?:\d+\.\d*|\.?\d+))\)|\((-?(?:\d+\.\d*|\.?\d+))\)([/])([+-]?(?:\d+\.\d*|\.?\d+))|(\d+\.\d*|\.?\d+)([/])\(([+-]?(?:\d+\.\d*|\.?\d+))\)";

        private static readonly string ModuloRegex =
            @"(\d+\.\d*|\.?\d+)([%])([+-]?(?:\d+\.\d*|\.?\d+))|\((-?(?:\d+\.\d*|\.?\d+))\)([%])\(([+-]?(?:\d+\.\d*|\.?\d+))\)|\((-?(?:\d+\.\d*|\.?\d+))\)([%])([+-]?(?:\d+\.\d*|\.?\d+))|(\d+\.\d*|\.?\d+)([%])\(([+-]?(?:\d+\.\d*|\.?\d+))\)";

        private static readonly Calculation Addition = new Calculation(AddRegex, Add);
        private static readonly Calculation Subtraction = new Calculation(SubtractRegex, Subtract);
        private static readonly Calculation Multiplication = new Calculation(MultiplyRegex, Multiply);
        private static readonly Calculation Division = new Calculation(DivideRegex, Divide);
        private static readonly Calculation Modulus = new Calculation(ModuloRegex, Modulo);
        private static readonly Calculation Exponential = new Calculation(ExponentRegex, Exponent);

        private static string Add(string input)
        {
            var addend = Regex.Match(input,
                AddRegex);
            if (addend.Success)
            {
                var extra = GetOne(addend.Groups, 1, 5, 9, 13, 17);
                var x = double.Parse(GetOne(addend.Groups, 2, 6, 10, 14, 18));
                var y = double.Parse(GetOne(addend.Groups, 4, 8, 12, 16, 20));
                input = input.Remove(addend.Index, addend.Length)
                             .Insert(addend.Index, $"{extra}({x + y})");
                input = Add(input);
            }

            return input;
        }

        private static string CalculationGroup(string input, params Calculation[] calculations)
        {
            while (Regex.IsMatch(input, string.Join("|", calculations.Select(x => x.Regex))))
            {
                foreach (var calculation in calculations.Select(x => x.Operation)) input = calculation(input);

                input = CalculationGroup(input, calculations);
            }

            return input;
        }

        private static string Divide(string input)
        {
            var dividend = Regex.Match(input,
                DivideRegex,
                RegexOptions.RightToLeft);
            if (dividend.Success)
            {
                var x = double.Parse(GetOne(dividend.Groups, 1, 4, 7, 10));
                var y = double.Parse(GetOne(dividend.Groups, 3, 6, 9, 12));
                input = input.Remove(dividend.Index, dividend.Length)
                             .Insert(dividend.Index, $"{x / y}");
                input = Divide(input);
            }

            return input;
        }

        public static double Evaluate(string input)
        {
            var current = input;
            var lastEval = string.Empty;
            do
            {
                lastEval = current;

                // Spaces, newlines, and commas
                current = Regex.Replace(current, @"[\s,]", "");

                // Converting words
                var wordReplacements = new Dictionary<string, string>
                {
                    { @"plus|add", "+" },
                    { @"minus|subtract", "-" },
                    { @"divided?(?:by|)", "/" },
                    { @"multipl(?:y|ied)(?:by|)|times?", "*" },
                    { @"(?:to|)(?:the|)powerof|raised(?:to|by|)", "^" },
                    { @"mod(?:ulo)/ig", "%" },
                    { @"factor(?:ial|)(?:of|by|)", "!" }
                };
                foreach (var replacement in wordReplacements)
                    current = Regex.Replace(current, replacement.Key, replacement.Value);

                // Fix sign convention
                current = WhileRegex(current, "+", @"--|\+\+");
                current = WhileRegex(current, "-", @"-\+|\+-");

                // Fix exponents
                var exponents = Regex.Matches(current,
                    @"(?:(\d+)\.(\d*)|\.?(\d+))e([-+])?(\d+)",
                    RegexOptions.IgnoreCase);
                foreach (Match exponent in exponents)
                    current = current.Remove(exponent.Index, exponent.Length)
                                     .Insert(exponent.Index,
                                          double.Parse(exponent.Value).ToString(CultureInfo.InvariantCulture));

                // Convert constants
                current = Regex.Replace(current,
                    @"(?:Math\.|)PI", $"({Math.PI})",
                    RegexOptions.IgnoreCase);
                current = Regex.Replace(current,
                    @"(?:Math\.|)E", $"({Math.E})",
                    RegexOptions.IgnoreCase);

                // Math methods
                var squares = Regex.Matches(current,
                    @"(?:Math\.|)Sqrt\((-?(?:\d+\.\d*|\.?\d+))\)",
                    RegexOptions.IgnoreCase);
                foreach (Match square in squares)
                    current = current.Remove(square.Index, square.Length)
                                     .Insert(square.Index, $"({Math.Sqrt(double.Parse(square.Groups[1].Value))})");
                var cubes = Regex.Matches(current,
                    @"(?:Math\.|)Cqrt\((-?(?:\d+\.\d*|\.?\d+))\)",
                    RegexOptions.IgnoreCase);
                foreach (Match cube in cubes)
                    current = current.Remove(cube.Index, cube.Length)
                                     .Insert(cube.Index, $"({Math.Cbrt(double.Parse(cube.Groups[1].Value))})");

                var roots = Regex.Matches(current,
                    @"(?:Math\.|)Root\(([+-]?(?:\d+\.\d*|\.?\d+))\|([+-]?(?:\d+\.\d*|\.?\d+))\)",
                    RegexOptions.IgnoreCase);
                foreach (Match root in roots)
                    current = current.Remove(root.Index, root.Length)
                                     .Insert(root.Index,
                                          $"({Math.Pow(double.Parse(root.Groups[1].Value), 1d / double.Parse(root.Groups[2].Value))})");

                var powers = Regex.Matches(current,
                    @"(?:Math\.|)Pow\(([+-]?(?:\d+\.\d*|\.?\d+))\|([+-]?(?:\d+\.\d*|\.?\d+))\)",
                    RegexOptions.IgnoreCase);
                foreach (Match power in powers)
                    current = current.Remove(power.Index, power.Length)
                                     .Insert(power.Index,
                                          $"({Math.Pow(double.Parse(power.Groups[1].Value), double.Parse(power.Groups[2].Value))})");

                // Solve !
                var factorials = Regex.Matches(current, @"(\d+\.\d*|\.?\d+)!",
                    RegexOptions.IgnoreCase);
                foreach (Match factorial in factorials)
                {
                    var x = Factorial(double.Parse(factorial.Groups[1].Value));
                    current = current.Remove(factorial.Index, factorial.Length)
                                     .Insert(factorial.Index, x.ToString());
                }

                // Solve %, *, /, ^
                current = CalculationGroup(current, Modulus, Multiplication, Division, Exponential);

                // Resolve missing * in operations.
                current = WhileRegex(current, "$1$3*$2$4",
                    @"(\d)(\()|(\))(\d)");
                current = WhileRegex(current, "($1*$2)",
                    @"(\d+\.\d*|\.?\d+)\((-?(?:\d+\.\d*|\.?\d+))\)",
                    @"\((-?(?:\d+\.\d*|\.?\d+))\)(\d+\.\d*|\.?\d+)",
                    @"\((-?(?:\d+\.\d*|\.?\d+))\)\((-?(?:\d+\.\d*|\.?\d+))\)");

                // Resolve (x) -> x
                // Resolve [%*/^+-](x) -> [%*/^+-]x
                current = WhileRegex(current, "$1$2",
                    @"\(([+-]?)(\d+\.\d*|\.?\d+)\)",
                    @"([%*/^+-])\(([+-]?(?:\d+\.\d*|\.?\d+))\)");

                // Resolve x) -> x, (x -> x 
                current = WhileRegex(current, "$1",
                    @"^[(]+([+-]?(?:\d+\.\d*|\.?\d+))[)]*$",
                    @"^[(]*([+-]?(?:\d+\.\d*|\.?\d+))[)]+$");

                // Solve +, -
                current = CalculationGroup(current, Addition, Subtraction);
            } while (current != lastEval);

            return double.Parse(current);
        }

        private static string Exponent(string input)
        {
            var power = Regex.Match(input,
                ExponentRegex,
                RegexOptions.RightToLeft);
            if (power.Success)
            {
                var x = double.Parse(GetOne(power.Groups, 1, 4, 7, 10));
                var y = double.Parse(GetOne(power.Groups, 3, 6, 9, 12));
                input = input.Remove(power.Index, power.Length)
                             .Insert(power.Index, $"{Math.Pow(x, y)}");
                input = Exponent(input);
            }

            return input;
        }

        private static double Factorial(double number)
        {
            if (number == 1)
                return 1;
            return number * Factorial(number - 1);
        }

        private static string GetOne(GroupCollection collection, params int[] groups)
        {
            foreach (var group in groups)
            {
                var value = collection[group].Value;
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }

            return string.Empty;
        }

        private static void Main(string[] args)
        {
            while (true) Console.WriteLine(Evaluate(Console.ReadLine()));
        }

        private static string Modulo(string input)
        {
            var modulo = Regex.Match(input,
                ModuloRegex,
                RegexOptions.RightToLeft);
            if (modulo.Success)
            {
                var x = double.Parse(GetOne(modulo.Groups, 1, 4, 7, 10));
                var y = double.Parse(GetOne(modulo.Groups, 3, 6, 9, 12));
                input = input.Remove(modulo.Index, modulo.Length)
                             .Insert(modulo.Index, $"{x % y}");
                input = Modulo(input);
            }

            return input;
        }

        private static string Multiply(string input)
        {
            var multiplicand = Regex.Match(input,
                MultiplyRegex);
            if (multiplicand.Success)
            {
                var x = double.Parse(GetOne(multiplicand.Groups, 1, 4, 7, 10));
                var y = double.Parse(GetOne(multiplicand.Groups, 3, 6, 9, 12));
                input = input.Remove(multiplicand.Index, multiplicand.Length)
                             .Insert(multiplicand.Index, $"{x * y}");
                input = Subtract(input);
            }

            return input;
        }

        private static string Subtract(string input)
        {
            var subtrahend = Regex.Match(input,
                SubtractRegex);
            if (subtrahend.Success)
            {
                var extra = GetOne(subtrahend.Groups, 1, 5, 9, 13, 17);
                var x = double.Parse(GetOne(subtrahend.Groups, 2, 6, 10, 14, 18));
                var y = double.Parse(GetOne(subtrahend.Groups, 4, 8, 12, 16, 20));
                input = input.Remove(subtrahend.Index, subtrahend.Length)
                             .Insert(subtrahend.Index, $"{extra}({x - y})");
                input = Subtract(input);
            }

            return input;
        }

        private static string WhileRegex(string input, string replacement, params string[] patterns)
        {
            while (patterns.Any(pattern => Regex.IsMatch(input, pattern)))
                foreach (var pattern in patterns)
                    input = Regex.Replace(input, pattern, replacement);

            return input;
        }
    }
}