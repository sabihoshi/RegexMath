# RegexMath
A math engine written in C# using RegEx.
```cs
RegexMath.Evaluate("5+(-5(0.431e4*(5)(4)))*3.430e2(.509194e-5)-40.05245");
// -787.80921602
```

```cs
var input = ("broken");
RegexMath.TryEvaluate(input, our var result);
// False
```

- Currently supports `^`, `**`, `*`, `/`, `%`, `+`, and `-`.
- Only valid numbers are ones that `double.TryParse()` can evaluate. This includes exponents.

If you'd like to test out the regex yourself, check this [link](https://awau.moe/4pHJQeU)