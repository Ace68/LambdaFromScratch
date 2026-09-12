# LambdaFromScratch — v0.1 Specification

## 1. Project vision

**LambdaFromScratch** is a hands-on journey into Lambda Calculus for experienced C# developers who have never studied it.

The guiding question is:

> You know how to program in C#. Let's rebuild some of the fundamental pieces of programming using functions alone.

The project is not intended to provide a production-ready functional programming library.

Its purpose is educational: progressively remove familiar C# constructs and rebuild concepts such as booleans, numbers, logic, arithmetic, and data structures using functions and function application.

A central idea of the project is that the C# implementation should remain visually and conceptually close to the corresponding Lambda Calculus expression.

Example:

```text
TRUE = λx.λy.x
```

should have a C# representation structurally similar to:

```csharp
x => y => x
```

The Lambda Calculus must remain visible through the C# syntax.

---

## 2. Target audience

The primary audience is:

* experienced C# developers;
* comfortable with delegates, lambdas and generics;
* not expected to know Lambda Calculus;
* not expected to have an academic mathematics background;
* interested in understanding the foundations of functional programming.

The documentation should start from concepts familiar to a C# developer and progressively introduce Lambda Calculus notation.

Avoid assuming prior knowledge of:

* lambda abstraction;
* beta reduction;
* Church encoding;
* combinators;
* formal computation theory.

Introduce terminology only when it becomes useful.

---

## 3. Technology

Use:

* .NET 10;
* C# 14;
* xUnit;
* no additional assertion library;
* no mocking framework;
* nullable reference types enabled;
* implicit usings enabled where appropriate.

Target framework:

```xml
<TargetFramework>net10.0</TargetFramework>
```

Favor standard C# constructs and keep dependencies to an absolute minimum.

---

## 4. Repository structure

Use the following structure:

```text
LambdaFromScratch
│
├── LambdaFromScratch.slnx
├── README.md
│
├── docs/
│   ├── specs/
│   │   └── v0.1.md
│   │
│   ├── 01-functions.md
│   ├── 02-identity.md
│   ├── 03-booleans.md
│   ├── 04-logic.md
│   ├── 05-church-numerals.md
│   ├── 06-successor.md
│   ├── 07-addition.md
│   ├── 08-multiplication.md
│   ├── 09-pairs.md
│   └── 10-predicates.md
│
├── src/
│   ├── LambdaFromScratch/
│   │   ├── Functions/
│   │   ├── Booleans/
│   │   ├── Numerals/
│   │   ├── Pairs/
│   │   ├── Predicates/
│   │   └── Observation/
│   │
│   ├── LambdaFromScratch.Tests/
│   │
│   └── LambdaFromScratch.Kata.Tests/
│
└── exercises/
    └── LambdaFromScratch.Kata/
        ├── Functions/
        ├── Booleans/
        ├── Numerals/
        ├── Pairs/
        └── Predicates/
```

`LambdaFromScratch` contains the complete reference implementation.

`LambdaFromScratch.Tests` verifies the reference implementation.

`LambdaFromScratch.Kata` contains incomplete implementations intended for practice.

`LambdaFromScratch.Kata.Tests` guides the user through the kata.

`docs` explains the progression and concepts.

---

## 5. Fundamental design principle

The priority order is:

```text
conceptual fidelity
    ↓
educational readability
    ↓
type safety
    ↓
API elegance
```

Do not optimize the implementation for production usage.

Do not create abstractions simply to make the API look more idiomatic or object-oriented.

The user should be able to compare:

```text
λm.λn.λf.λx.m f (n f x)
```

with its C# implementation and recognize the same structure.

---

## 6. Lambda Core rules

Inside the implementation of Lambda Calculus concepts, favor:

* functions;
* higher-order functions;
* function application;
* currying;
* `Func<>`;
* named delegates when they make the type system easier to understand.

Avoid using language constructs that directly implement the concept currently being reconstructed.

General rule:

> A C# primitive or control-flow construct must not be used to implement the Lambda Calculus concept that the exercise is intended to build.

### Examples

When implementing Church booleans:

Do not use:

```csharp
bool
if
switch
?: 
```

When implementing Church numerals:

Do not represent the numeral internally using:

```csharp
int
long
decimal
```

When implementing Lambda Calculus arithmetic:

Do not use C# arithmetic to perform the encoded operation.

For example, `Add` must not internally convert Church numerals to `int`, add them, and recreate a Church numeral.

When implementing recursion in future versions, native C# recursion must not be used to simulate the Lambda Calculus solution.

---

## 7. Observation boundary

Primitive C# values are allowed only when observing Lambda Calculus values from the outside.

Observation code must be clearly separated from the Lambda Core.

Place such code under:

```text
LambdaFromScratch/Observation/
```

Examples:

```csharp
ToBool(...)
ToInt(...)
```

These functions do not define Lambda Calculus concepts.

They translate encoded values into familiar C# values so that humans and tests can observe the result.

For example:

```csharp
ToInt(Two)
```

may return:

```csharp
2
```

but `Two` itself must not internally contain the integer `2`.

The distinction between **representation** and **observation** should be explicit in both code and documentation.

---

## 8. Currying

Currying should remain explicit.

Prefer:

```csharp
Add(Two)(Three)
```

over:

```csharp
Add(Two, Three)
```

Prefer:

```csharp
x => y => x
```

over:

```csharp
(x, y) => x
```

The goal is to preserve the relationship:

```text
λx.λy.
```

to:

```csharp
x => y =>
```

Avoid tuple-based APIs or multi-argument delegates when curried functions better express the Lambda Calculus definition.

---

## 9. Naming conventions

Use normal PascalCase C# naming in code:

```text
Identity
True
False
Not
And
Or
If
Zero
One
Two
Three
Successor
Add
Multiply
Pair
First
Second
IsZero
```

Use conventional uppercase Lambda Calculus notation in documentation:

```text
ID
TRUE
FALSE
NOT
AND
OR
IF
ZERO
ONE
TWO
THREE
SUCC
ADD
MULT
PAIR
FIRST
SECOND
ISZERO
```

Do not use uppercase mathematical identifiers as C# member names merely to mimic textbooks.

---

## 10. Avoid unnecessary object orientation

Do not model Lambda Calculus concepts using classes containing state unless absolutely required by the C# type system.

Avoid APIs such as:

```csharp
var two = new ChurchNumber(2);
two.Add(three);
```

or:

```csharp
ChurchNumber.Two.Add(ChurchNumber.Three);
```

These abstractions hide the essential mechanism.

Prefer values represented directly as functions.

Named delegates are acceptable when they make otherwise unreadable nested `Func<>` signatures understandable.

Do not introduce:

* interfaces without a concrete need;
* dependency injection;
* factories;
* service classes;
* repositories;
* builders;
* unnecessary inheritance;
* generic abstraction frameworks.

YAGNI applies aggressively.

---

# 11. v0.1 learning path

Version `0.1` contains ten progressive stages.

Each stage should introduce as few new ideas as possible.

## 11.1 Functions

Documentation only.

Introduce how to read:

```text
λx.x
λx.λy.x
f x
```

Explain:

* variables;
* lambda abstraction;
* function application;
* nested functions;
* currying.

Relate each expression to C# lambda syntax.

Do not introduce Church encoding yet.

---

## 11.2 Identity

Introduce:

```text
ID = λx.x
```

C# shape:

```csharp
x => x
```

This is the first kata.

Expected behavior:

```csharp
Identity("hello")
```

returns:

```text
hello
```

The purpose is not the usefulness of Identity but learning how to read and translate the notation.

---

## 11.3 Booleans

Introduce Church booleans:

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

Explain boolean values as behavior rather than stored primitive values.

Conceptually:

```text
TRUE chooses the first argument.
FALSE chooses the second argument.
```

Expected observable behavior:

```csharp
True("yes")("no")  == "yes"
False("yes")("no") == "no"
```

No C# `bool` should participate in their implementation.

---

## 11.4 Logic

Build logical behavior from Church booleans.

Include:

```text
NOT
AND
OR
IF
```

The implementations must compose existing Church booleans.

No use of:

```csharp
if
switch
bool
?: 
```

inside their Lambda Core implementations.

The documentation must show the Lambda Calculus definition before the C# translation.

---

## 11.5 Church numerals

Introduce numbers as repeated function application.

Start with:

```text
ZERO  = λf.λx.x
ONE   = λf.λx.f x
TWO   = λf.λx.f (f x)
THREE = λf.λx.f (f (f x))
```

Explain them as:

```text
ZERO  → apply f zero times
ONE   → apply f once
TWO   → apply f twice
THREE → apply f three times
```

Do not describe a Church numeral primarily as an encoded integer.

Describe it primarily as repeated application of a transformation.

Provide an observation function similar to:

```csharp
ToInt(...)
```

only outside the Lambda Core.

---

## 11.6 Successor

Introduce:

```text
SUCC
```

Explain that successor adds one additional application of the supplied function.

Expected behavior:

```text
SUCC ZERO  -> ONE
SUCC ONE   -> TWO
SUCC TWO   -> THREE
```

Tests may observe these through `ToInt`.

The implementation itself must not use integer addition.

---

## 11.7 Addition

Introduce Church numeral addition.

Lambda definition:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

Focus strongly on teaching how to read the expression.

Explain it conceptually as:

1. apply `f` `n` times to `x`;
2. take the resulting value;
3. apply `f` another `m` times.

Expected behavior:

```csharp
ToInt(Add(Two)(Three))
```

returns:

```text
5
```

The implementation must not use C# arithmetic.

---

## 11.8 Multiplication

Introduce Church multiplication.

Explain multiplication as repeated function composition/application rather than ordinary numeric multiplication.

Expected examples:

```text
TWO × THREE = SIX
THREE × THREE = NINE
```

Tests may observe results using `ToInt`.

No primitive multiplication is allowed in the Lambda Core.

---

## 11.9 Pairs

Introduce data structures encoded as behavior.

Include:

```text
PAIR
FIRST
SECOND
```

Use this stage to reinforce the project theme:

> Data does not necessarily need to be represented as fields stored inside an object.

Do not implement the Lambda pair internally using:

```csharp
Tuple
ValueTuple
record
class with First/Second fields
```

The Lambda pair itself must be function-based.

Observation helpers may exist if necessary.

---

## 11.10 Predicates

Introduce:

```text
ISZERO
```

The result must be a Church boolean, not a C# `bool`.

Examples:

```text
ISZERO ZERO -> TRUE
ISZERO ONE  -> FALSE
ISZERO TWO  -> FALSE
```

C# `bool` may only appear when observing the resulting Church boolean.

This is the final stage of v0.1.

---

# 12. Explicitly out of scope for v0.1

Do not implement yet:

```text
PREDECESSOR
SUBTRACT
LEQ
EQ
Y combinator
fixed-point combinators
recursive algorithms
FACTORIAL
Church lists
Church strings
```

These concepts belong to later releases.

Do not implement them merely because they are natural extensions of the existing code.

---

# 13. Kata format

Each exercise should follow the same learning sequence.

## Step 1 — Familiar C# concept

Start from something the reader already understands.

Example:

```csharp
bool enabled = true;
```

Ask how the same idea could be represented without using the corresponding primitive.

## Step 2 — Lambda definition

Show the mathematical expression.

Example:

```text
TRUE = λx.λy.x
```

## Step 3 — Read it aloud

Explain the expression in plain language.

Example:

```text
Take x.
Take y.
Return x.
```

## Step 4 — Structural translation

Show how notation maps to C#.

Example:

```text
λx.       → x =>
λy.       → y =>
x         → x
```

## Step 5 — Exercise

The kata project should contain a compilable placeholder or explicit TODO.

Do not put the complete solution in comments.

The user should need to derive the implementation.

## Step 6 — Tests

Tests should describe behavior clearly.

Example:

```csharp
[Fact]
public void True_selects_the_first_value()
{
    var result = True("yes")("no");

    Assert.Equal("yes", result);
}
```

Tests are part of the teaching material.

Prefer simple, explicit assertions over clever test abstractions.

## Step 7 — Reference solution

The complete implementation exists under:

```text
src/LambdaFromScratch
```

It should remain easy to compare with the kata implementation.

---

# 14. Testing principles

Use xUnit only.

Do not use:

* FluentAssertions;
* Shouldly;
* mocking libraries;
* AutoFixture;
* snapshot testing frameworks.

Tests should be intentionally boring and readable.

Prefer:

```csharp
Assert.Equal(expected, actual);
Assert.True(...);
Assert.False(...);
```

Observation of encoded concepts may use normal C# primitive values because tests operate outside the Lambda Core.

Tests should verify behavior, not implementation details.

Avoid reflection-based tests unless necessary.

---

# 15. Documentation principles

Every chapter under `docs/` should be short enough to read before attempting the corresponding kata.

The documentation should repeatedly use three views:

## Lambda Calculus

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

## C#

```csharp
m => n => f => x =>
    m(f)(n(f)(x))
```

## Meaning

Explain the behavior in natural language.

The reader should learn to move progressively between these three representations.

Do not make the documentation primarily theoretical.

Avoid long historical or mathematical digressions in v0.1.

The objective is that an experienced C# developer eventually looks at:

```text
λm.λn.λf.λx.m f (n f x)
```

and thinks:

> I think I can read that.

---

# 16. README requirements

The README should open with a strong, concise introduction.

Suggested opening:

> **You know C#. Now remove almost everything.**
>
> No numbers.
> No booleans.
> No conditions.
> No loops.
> No data structures.
>
> Keep functions.
>
> Can we build everything back?

Follow with:

> LambdaFromScratch is a hands-on journey into Lambda Calculus for experienced C# developers who have never studied it.

And:

> You know how to program in C#. Let's rebuild some of the fundamental pieces of programming using functions alone.

The README should then explain:

* who the project is for;
* what it is not;
* prerequisites;
* repository structure;
* how to run tests;
* how to start the kata;
* learning path;
* Lambda Core restrictions;
* distinction between Lambda values and observation adapters.

Keep the README approachable.

Detailed explanations belong under `docs/`.

---

# 17. Code style

Favor extremely small functions.

Avoid comments that merely repeat the code.

Comments are appropriate when they explain the relationship between a C# expression and Lambda Calculus notation.

Use file-scoped namespaces.

Prefer meaningful named delegates when nested `Func<>` types become difficult to read.

However, before introducing a named delegate, ask:

> Does this make the Lambda Calculus easier to see, or does it hide it?

If it hides the expression, prefer the more explicit functional form.

Do not create production-style XML documentation for every member unless it adds educational value.

---

# 18. Copilot implementation rules

When generating code for this project:

1. Read this specification before proposing implementation.
2. Implement only the current requested learning stage.
3. Do not implement future stages preemptively.
4. Do not introduce architectural abstractions without a concrete requirement.
5. Do not convert Lambda values to primitive C# values to implement operations.
6. Keep observation adapters separate.
7. Keep currying explicit.
8. Preserve structural similarity with Lambda Calculus expressions.
9. Prefer readability over clever generic type tricks.
10. Write tests before or together with the implementation.
11. Use xUnit only.
12. Keep every test independently understandable.
13. Do not silently relax Lambda Core restrictions to make C# typing easier.
14. If the C# type system makes a faithful implementation awkward, prefer introducing a small named delegate and explain why.
15. Do not solve TODOs in the kata project unless explicitly asked to generate the solution version.
16. Do not add dependencies unless explicitly requested.
17. Do not implement anything listed as out of scope for v0.1.

---

# 19. Recommended implementation order

Implement one milestone at a time:

```text
Milestone 0
Solution and project scaffolding

Milestone 1
Functions documentation

Milestone 2
Identity

Milestone 3
Church booleans

Milestone 4
Logical operators

Milestone 5
Church numerals

Milestone 6
Successor

Milestone 7
Addition

Milestone 8
Multiplication

Milestone 9
Pairs

Milestone 10
IsZero

Milestone 11
README cleanup and complete v0.1 verification
```

After every milestone:

```text
dotnet build
dotnet test
```

must succeed for the reference implementation.

Kata tests may intentionally fail only when the exercise is intentionally incomplete and this behavior is clearly documented.

---

# 20. Definition of Done for v0.1

Version `0.1` is complete when:

* the solution targets .NET 10;
* the reference project builds without warnings introduced by the project;
* all reference tests pass;
* every Lambda Core concept is implemented without cheating through primitive equivalents;
* observation adapters are clearly separated;
* every stage has corresponding documentation;
* every stage after the introductory chapter has a kata;
* kata tests clearly describe expected behavior;
* the reference implementation can be compared easily with the exercise;
* README explains how to start and follow the learning path;
* the project uses no unnecessary external dependencies;
* no post-v0.1 features have been implemented;
* the source remains visibly connected to the Lambda Calculus definitions.

The most important acceptance criterion is qualitative:

> A C# developer with no previous Lambda Calculus experience should be able to complete the progression and begin reading simple Lambda Calculus expressions without treating them as mysterious mathematical notation.
