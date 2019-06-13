using System;

namespace RegexMath
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
