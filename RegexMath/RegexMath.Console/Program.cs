using System;

while (true)
{
    Console.ResetColor();
    var input = Console.ReadLine();
    var success = RegexMath.RegexMath.TryEvaluate(input, out var result);
    Console.WriteLine(success);
    if (success)
        Console.WriteLine($"{input} = {result}");

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(" < Press any key to continue | Ctrl+X to exit > ");
    var keyInfo = Console.ReadKey();

    Console.Clear();
    if (keyInfo is { Modifiers: ConsoleModifiers.Control, Key: ConsoleKey.X }) break;
}

Console.ReadKey();