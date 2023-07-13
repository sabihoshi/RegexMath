namespace RegexMath.Calculation.Operation;

public interface IOperation
{
    bool TryEvaluate(string input, out string result);

    string Evaluate(string input);
}