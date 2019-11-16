namespace RegexMath.Calculation.Operation
{
    public interface IOperation
    {
        string Evaluate(string input);

        bool TryEvaluate(string input, out string result);
    }
}