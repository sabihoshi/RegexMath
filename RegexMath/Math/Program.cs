using System;
using RegexMathLibrary;

namespace Math
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine($"{input} = {RegexMath.Evaluate(input)}");
            }
            
        }
    }
}
