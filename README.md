# LambdaFromScratch

**You know C#. Now remove almost everything.**

No numbers.  
No booleans.  
No conditions.  
No loops.  
No data structures.

Keep functions.

**Can we build everything back?**

LambdaFromScratch is a hands-on journey into Lambda Calculus for experienced C# developers who have never studied it.

You know how to program in C#. Let's rebuild some of the fundamental pieces of programming using functions alone.

## Prerequisites

- .NET 10 SDK
- Familiarity with C# delegates, lambdas, and generics

## Repository structure

- `src/LambdaFromScratch` - complete reference implementation
- `src/LambdaFromScratch.Tests` - tests for the reference implementation
- `exercises/LambdaFromScratch.Kata` - exercises to complete
- `src/LambdaFromScratch.Kata.Tests` - tests that guide the kata
- `docs` - progressive learning material

## Build

```bash
dotnet build LambdaFromScratch.slnx
```

## Test

```bash
dotnet test LambdaFromScratch.slnx
```

## Learning path

1. Functions
2. Identity
3. Booleans
4. Logic
5. Church Numerals
6. Successor
7. Addition
8. Multiplication
9. Pairs
10. Predicates

The project is intentionally built one concept at a time. Start with `docs/01-functions.md`.
