# RegexMath [![Build Status](https://dev.azure.com/kaworinakano/RegexMath/_apis/build/status/hizamakura.RegexMath?branchName=master)](https://dev.azure.com/kaworinakano/RegexMath/_build/latest?definitionId=1&branchName=master) [![CodeFactor](https://www.codefactor.io/repository/github/hizamakura/regexmath/badge/master)](https://www.codefactor.io/repository/github/hizamakura/regexmath/overview/master)

A math engine written in C# using RegEx.

## Why?
Because why not?

## Examples
```cs
RegexMath.Evaluate("5+(-5(0.431e4*(5)(4)))*3.430e2(.509194e-5)-40.05245");
// -787.80921602

Regexmath.Evaluate("sqrt(5)+5^2PI+H_3/log(e, 3)");
// 82.444656232727
```

```cs
var input = "broken";
RegexMath.TryEvaluate(input, our var result);
// False
```

## Features

### Standard Arithmetic:
`^`, `**`, `*`, `/`, `%`, `+`, and `-`

### Constants:
`PI` and `E`

### Standard Math functions:
- `sqrt(x)`
- `cbrt(x)`
- `root(x, y)`
- `log_x(y)` or `log(x, y)`
- `min(x, y)` or `max(x, y)`

and a lot more...

### Programming constructs:
- Modulo
- Remainder
- Bitshift

### Complex calcuations:
- Factorial on non-natural numbers
- Euler Beta functions
- Error functions
- Sigmoid: Logistic functions
- Gamma functions
- Harmonic numbers
- Trigonometry

## TODO:
- Summation in the form of `Σ/sum/sigma[i=1, 6][3i + 4]`
- Product in the form of `Π/prod/product[i=1, 3][3i + 4]`
- Variable substitution
- Differential equations
- Numerical stability
- Conversion between radian, degree, and grad

Some of the functions can be found in [Math.Net](https://numerics.mathdotnet.com/Functions.html) or [Typing Math as text](https://www.purplemath.com/modules/mathtext3.htm)
