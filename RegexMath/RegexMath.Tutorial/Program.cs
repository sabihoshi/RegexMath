using System.Text.RegularExpressions;

var input = Console.ReadLine()!;
var regex = new Regex(
    """
    (?<number>     \d+(\.\d+)?     )
    (?<operator>   [-+*/]          )
    (?<number>     \d+(\.\d+)?     )
    """,
    RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

while (regex.IsMatch(input))
{
    input = regex.Replace(input, match =>
    {
        WriteBeforeResult();

        var x = double.Parse(match.Groups["number"].Captures[0].Value);
        var y = double.Parse(match.Groups["number"].Captures[1].Value);
        var operation = match.Groups["operator"].Value;

        var result = operation switch
        {
            "+" => x + y,
            "-" => x - y,
            "*" => x * y,
            "/" => x / y,
            _   => throw new InvalidOperationException()
        };

        WriteAfterResult();

        return result.ToString();

        void WriteBeforeResult()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(match.Value);
            Console.ResetColor();
            Console.Write(input[match.Length..]);
        }

        void WriteAfterResult()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(result);

            Console.ResetColor();
            Console.WriteLine(input[match.Length..]);
        }
    }, 1);
}

Console.WriteLine($"Final result: {input}");