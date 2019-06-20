using System;

namespace Math
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine($"{input} = {RegexMath.RegexMath.Evaluate(input)}");
            }
        }
    }
}