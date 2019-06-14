using System;

namespace RegexMath
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine($"{input} = {RegexMath.Evaluate(input)}");
            }
        }
    }
}