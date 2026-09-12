# 06 — Successor

Church numerals describe repeated function application:

```text
ZERO  = apply f zero times
ONE   = apply f once
TWO   = apply f twice
THREE = apply f three times
```

So far we wrote each numeral manually.

> Can we construct the next numeral from an existing one?

## One more application

Start with `TWO`:

```text
TWO = λf.λx.f (f x)
```

It applies `f` twice. To obtain `THREE`, we want to:

```text
take TWO
apply what TWO already does
apply f once more
```

Visually:

```text
x
↓ f
f(x)
↓ f
f(f(x))
↓ one more f
f(f(f(x)))
```

Successor captures exactly that operation:

```text
SUCC = λn.λf.λx.f (n f x)
```

## Reading the expression

Expand its structure:

```text
λn.
    λf.
        λx.
            f (n f x)
```

Read it as:

> Take a Church numeral `n`.  
> Take a transformation `f`.  
> Take a starting value `x`.  
> Let `n` apply `f` to `x`.  
> Apply `f` one more time to the result.

The body has two important parts.

First:

```text
n f x
```

means:

> Let numeral `n` apply `f` its number of times to `x`.

Then:

```text
f (...)
```

means:

> Apply `f` one more time.

`SUCC n` therefore means: do what `n` already does, plus one additional application. There is no stored number to increment.

## Three views of Successor

### Lambda Calculus

```text
SUCC = λn.λf.λx.f (n f x)
```

### C#

```csharp
n => f => x => f(n(f)(x))
```

The project expresses that shape as:

```csharp
public static ChurchNumeral<T> Successor<T>(ChurchNumeral<T> n) =>
    f => x => f(n(f)(x));
```

### Meaning

> Let `n` apply `f`, then apply `f` one more time.

## Walking through SUCC TWO

Begin with:

```text
SUCC TWO
```

Putting `TWO` into the successor body gives:

```text
λf.λx.f (TWO f x)
```

Remember:

```text
TWO f x = f (f x)
```

Therefore:

```text
f (TWO f x)
```

becomes:

```text
f (f (f x))
```

That is exactly `THREE`.

The same reasoning gives:

```text
SUCC ZERO → ONE
SUCC ONE  → TWO
SUCC TWO  → THREE
```

## A non-numeric example

Use a string transformation:

```csharp
Func<string, string> addStar = value => value + "*";
```

`TWO` applies it twice:

```text
TWO addStar ""
→ "**"
```

Its successor applies it once more:

```text
SUCC TWO addStar ""
→ "***"
```

> Successor does not add the integer 1. It adds one more application of the transformation.

That is the key idea of this milestone.

## Observing the result

We may observe a successor as an integer:

```text
ToInt(Successor(Two)) → 3
```

This conversion happens only after the successor has been constructed. `ToInt` does not participate in its implementation.

The dependency remains:

```text
Successor
    ↓
Church numeral
    ↓
Observation
    ↓
int
```

Never the reverse.

## What comes next?

> If Successor adds one application, could we apply Successor repeatedly to add two Church numerals?

That is the question for the next milestone.
