# 07 — Addition

We already understand Church numerals as repeated application:

```text
TWO   → apply f twice
THREE → apply f three times
```

> What should TWO plus THREE mean if numbers are only repeated function application?

It should apply `f` three times, then continue by applying the same `f` two more times.

## Building TWO plus THREE

Start with a value `x` and let `THREE` work:

```text
x
↓ f
f(x)
↓ f
f(f(x))
↓ f
f(f(f(x)))
```

Now let `TWO` continue from that result:

```text
f(f(f(x)))
↓ f
f(f(f(f(x))))
↓ f
f(f(f(f(f(x)))))
```

The resulting transformation applies `f` five times.

> Addition is just one numeral continuing where the other one stopped.

## The definition

That behavior is captured by:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

Read it progressively.

First:

```text
n f x
```

means:

> Let `n` apply `f` to `x`.

Then:

```text
m f (...)
```

means:

> Starting from that result, let `m` apply `f` again.

Together:

```text
m f (n f x)
```

means:

> Apply `f` `n` times, then `m` more times.

No stored numbers or primitive arithmetic are involved.

## Reading the application

Function application associates to the left, so:

```text
n f x
```

means:

```text
(n f) x
```

In C#:

```csharp
n(f)(x)
```

Therefore:

```text
m f (n f x)
```

maps directly to:

```csharp
m(f)(n(f)(x))
```

## Three views of Addition

### Lambda Calculus

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

### C#

```csharp
m => n => f => x => m(f)(n(f)(x))
```

The project keeps that shape visible:

```csharp
public static Func<ChurchNumeral<T>, ChurchNumeral<T>> Add<T>(ChurchNumeral<T> m) =>
    n => f => x => m(f)(n(f)(x));
```

### Meaning

> Let `n` apply `f`, then let `m` apply the same function to the result.

## Walking through ADD TWO THREE

Start with the body:

```text
m f (n f x)
```

Use:

```text
m = TWO
n = THREE
```

The expression becomes:

```text
TWO f (THREE f x)
```

`THREE f x` applies `f` three times. `TWO` then applies `f` twice more:

```text
f(f(f(f(f(x)))))
```

The result of `Add(Two)(Three)` is already the Church numeral representing five applications. We do not need a special constant to construct that result.

## A non-numeric example

Use the same string transformation as before:

```csharp
Func<string, string> addStar = value => value + "*";
```

Individually:

```text
TWO addStar ""   → "**"
THREE addStar "" → "***"
```

Combined:

```text
ADD TWO THREE addStar ""
→ "*****"
```

At no point did `ADD` know that `TWO` means 2, `THREE` means 3, or that the result means 5. It only composed repetitions.

## Observing the result

At the observation boundary:

```text
ToInt(Add(Two)(Three)) → 5
```

The integer appears only after the Church numeral has been constructed:

```text
Add
 ↓
ChurchNumeral
 ↓
Observation
 ↓
int
```

`Add` itself contains no numeric values, arithmetic, or conversion.

Successor could help describe addition informally, but this implementation does not repeatedly call it. It expresses the direct Church definition instead.

## What comes next?

> If addition combines repeated applications, what would multiplication look like?

One hint: multiplication will involve repeating a repetition. That is the question for the next milestone.
