using System;

namespace Math
{
    static class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine();
                bool success = RegexMath.RegexMath.TryEvaluate(input, out double result);
                Console.WriteLine(success);
                if (success)
                    Console.WriteLine($"{input} = {result}");
            }
        }
    }
}