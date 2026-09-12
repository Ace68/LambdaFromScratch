# 05 — Church Numerals

## What if we remove numbers too?

In familiar C#, a number is a stored value:

```csharp
var number = 3;
```

Now remove that representation:

> What if we remove numbers too?

Instead of storing `3`, we can describe its behavior:

> Apply this transformation three times.

A Church numeral is not primarily an encoded integer. It is a function that controls repeated application:

```text
ZERO  → apply f zero times
ONE   → apply f once
TWO   → apply f twice
THREE → apply f three times
```

## ZERO

### Lambda Calculus

```text
ZERO = λf.λx.x
```

Read it as:

> Take a function `f`.  
> Take a starting value `x`.  
> Do nothing with `f`.  
> Return `x`.

### C#

```csharp
f => x => x
```

### Meaning

> Apply `f` zero times to `x`.

The function `f` is available, but `ZERO` never calls it.

## ONE

### Lambda Calculus

```text
ONE = λf.λx.f x
```

### C#

```csharp
f => x => f(x)
```

### Meaning

> Apply `f` once to `x`.

`ONE` differs from `ZERO` only because it applies the supplied function once.

## TWO

### Lambda Calculus

```text
TWO = λf.λx.f (f x)
```

### C#

```csharp
f => x => f(f(x))
```

### Meaning

> Apply `f` twice to `x`.

The repetition is visible:

```text
x
↓ f
f(x)
↓ f
f(f(x))
```

There is no counter. The nested applications are the numeral.

## THREE

### Lambda Calculus

```text
THREE = λf.λx.f (f (f x))
```

### C#

```csharp
f => x => f(f(f(x)))
```

### Meaning

> Apply `f` three times to `x`.

Again, the structure shows the repetition:

```text
x
↓ f
f(x)
↓ f
f(f(x))
↓ f
f(f(f(x)))
```

## Observing repetition with C#

To see this behavior as familiar integers, supply an increment function and a starting value:

```csharp
Func<int, int> increment = x => x + 1;
```

Then:

```text
ZERO  increment 0 → 0
ONE   increment 0 → 1
TWO   increment 0 → 2
THREE increment 0 → 3
```

The `int`, addition, and zero belong to the observation. They are not inside the Church numeral.

The `ToInt` observation adapter makes this boundary explicit:

```csharp
ToInt(Zero)  // 0
ToInt(One)   // 1
ToInt(Two)   // 2
ToInt(Three) // 3
```

The dependency moves in one direction:

```text
Church numeral
      ↓
Observation
      ↓
int
```

The numeral itself never converts from or stores an integer.

## Numerals are not integers

Incrementing is only one possible transformation. Consider:

```csharp
Func<string, string> addStar = x => x + "*";
```

The same Church numerals now describe string transformations:

```text
ZERO  addStar "" → ""
ONE   addStar "" → "*"
TWO   addStar "" → "**"
THREE addStar "" → "***"
```

They can also repeat any other `T => T` function. For example:

```csharp
Func<int, int> doubleValue = x => x * 2;
```

Starting from `1`:

```text
ZERO  → 1
ONE   → 2
TWO   → 4
THREE → 8
```

The multiplication belongs to the function supplied from outside. Each numeral only determines how many times that function is applied.

> This is why a Church numeral should not be thought of primarily as an integer. It represents repeated application.

## The important conceptual shift

Traditional representation:

```text
2 = a stored numeric value
```

Church representation:

```text
TWO = apply a transformation twice
```

> TWO does not know what the number 2 is. It only knows how to repeat a function twice.

That behavior is captured directly by:

```text
TWO = λf.λx.f (f x)
```

and:

```csharp
f => x => f(f(x))
```

## What comes next?

We have written each numeral by hand.

> If numbers are repeated function application, how do we create the next number without writing it manually?

That is the question for the next milestone.
