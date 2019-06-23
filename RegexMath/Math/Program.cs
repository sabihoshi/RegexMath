using System;

namespace Math
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                var success = RegexMath.RegexMath.TryEvaluate(input, out var result);
                Console.WriteLine(success);
                if(success)
                    Console.WriteLine($"{input} = {result}");
            }
        }
    }
}