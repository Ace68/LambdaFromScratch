# 08 — Multiplication

We already understand Church numerals as repetition:

```text
TWO   → apply a transformation twice
THREE → apply a transformation three times
```

> What should TWO multiplied by THREE mean if numbers are repetitions?

Before looking at a formula, consider what `THREE` does to a transformation `f`.

## Repeating a repetition

`THREE f` is itself a transformation:

```text
x → f(f(f(x)))
```

It takes a value and applies `f` three times. We can think of its behavior as:

```text
f ∘ f ∘ f
```

Now let `TWO` repeat that entire three-step transformation:

```text
x
↓ THREE f
f(f(f(x)))
↓ THREE f
f(f(f(f(f(f(x))))))
```

The result applies `f` six times.

> Multiplication is repetition of repetition.

This is behavior composed from functions, not arithmetic performed on stored values.

## The definition

That idea is captured directly by:

```text
MULT = λm.λn.λf.m (n f)
```

Read it progressively.

First:

```text
n f
```

means:

> Turn `f` into a transformation that applies `f` `n` times.

Then:

```text
m (n f)
```

means:

> Repeat that whole transformation `m` times.

Expanded to show the starting value explicitly, the same behavior is:

```text
MULT = λm.λn.λf.λx.m (n f) x
```

The shorter form works because `m (n f)` already returns the function that accepts `x`.

## Connecting the syntax

Lambda Calculus application:

```text
n f
```

maps to C#:

```csharp
n(f)
```

The larger expression:

```text
m (n f)
```

therefore maps to:

```csharp
m(n(f))
```

So:

```text
λm.λn.λf.m (n f)
```

has the C# shape:

```csharp
m => n => f => m(n(f))
```

## Three views of Multiplication

### Lambda Calculus

```text
MULT = λm.λn.λf.m (n f)
```

### C#

```csharp
public static Func<ChurchNumeral<T>, ChurchNumeral<T>> Multiply<T>(ChurchNumeral<T> m) =>
    n => f => m(n(f));
```

### Meaning

> Build a transformation that repeats `f` `n` times, then repeat that transformation `m` times.

## Multiplication is different from Addition

Addition was:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

It lets `n` apply `f`, then lets `m` continue applying the same `f`:

```text
n applications, then m more applications
```

Multiplication is:

```text
MULT = λm.λn.λf.m (n f)
```

It first builds `n f`, a transformation that already repeats `f`. It then asks `m` to repeat that entire transformation:

```text
m repetitions of an n-step transformation
```

The implementation does not call `Add`. It expresses Church multiplication directly.

## Walking through MULT TWO THREE

Start from the body:

```text
m (n f)
```

Use:

```text
m = TWO
n = THREE
```

The expression becomes:

```text
TWO (THREE f)
```

`THREE f` applies `f` three times. `TWO` applies that three-step transformation twice:

```text
first repetition  → three applications of f
second repetition → three more applications of f
```

The final behavior is six applications of `f`.

The result of `Multiply(Two)(Three)` is already the Church numeral that behaves as six repetitions. No `SIX` constant is needed to construct it.

## A non-numeric example

Use a string transformation:

```csharp
Func<string, string> addStar = value => value + "*";
```

`THREE addStar` is a transformation that adds three stars:

```text
THREE addStar ""
→ "***"
```

Now repeat that transformation twice:

```text
MULT TWO THREE addStar ""
→ "******"
```

> MULT never knows that TWO means 2, THREE means 3, or that the result means 6.

It only composes repeated transformations.

## Observing the result

At the observation boundary:

```text
ToInt(Multiply(Two)(Three)) → 6
```

The integer appears only after the Church numeral has been constructed:

```text
Multiply
    ↓
ChurchNumeral
    ↓
Observation
    ↓
int
```

`Multiply` contains no numeric values, primitive arithmetic, or conversion.

## What comes next?

So far we have represented:

```text
booleans
numbers
logic
arithmetic
```

using functions.

> Can functions represent data structures too?

That is the question for the next milestone.
