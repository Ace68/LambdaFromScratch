# 10 — Predicates

## Asking numbers a question

We now have numbers, but we still cannot ask them questions.

For example:

```text
Is this numeral zero?
```

In normal C#, we might write:

```csharp
value == 0
```

That would completely bypass the model we have built. A Church numeral is not
a stored integer that we can compare with zero.

> Can the behavior of a Church numeral itself tell us whether it is zero?

## The crucial difference

Recall what Church numerals do:

```text
ZERO               → applies a function zero times
every positive one → applies a function at least once
```

This difference is enough to build a predicate.

Suppose we start from:

```text
TRUE
```

and use a function that always returns:

```text
FALSE
```

Conceptually:

```text
alwaysFalse = λx.FALSE
```

Now let a Church numeral decide how many times to apply it.

With ZERO:

```text
TRUE
```

is never touched, so the result stays TRUE.

With ONE:

```text
TRUE
 ↓ alwaysFalse
FALSE
```

With TWO:

```text
TRUE
 ↓ alwaysFalse
FALSE
 ↓ alwaysFalse
FALSE
```

Any positive numeral therefore produces FALSE.

## Revealing ISZERO

The Church predicate is:

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

Read it slowly:

> Take `n`.
>
> Use `n` to apply the function `λx.FALSE`.
>
> Start from TRUE.

The expression:

```text
n (λx.FALSE) TRUE
```

means:

> Let `n` decide how many times "turn this into FALSE" is applied to TRUE.

If the number of applications is zero, TRUE survives. Otherwise, the first
application produces FALSE and every later application keeps producing FALSE.

## Walking through ISZERO ZERO

Start with:

```text
ISZERO ZERO
```

Substitute the definition:

```text
ZERO (λx.FALSE) TRUE
```

Recall:

```text
ZERO = λf.λx.x
```

ZERO ignores the supplied function and returns the starting value:

```text
ZERO (λx.FALSE) TRUE
→ TRUE
```

Therefore:

```text
ISZERO ZERO → TRUE
```

## Walking through ISZERO TWO

Now start with:

```text
ISZERO TWO
```

This becomes:

```text
TWO (λx.FALSE) TRUE
```

TWO applies its function twice. The first application is:

```text
(λx.FALSE) TRUE
→ FALSE
```

The second application is:

```text
(λx.FALSE) FALSE
→ FALSE
```

Therefore:

```text
ISZERO TWO → FALSE
```

We never inspected TWO and never converted it to the integer 2. Its behavior
told us the answer.

## Connecting numbers and booleans

Earlier milestones built these encodings independently:

```text
ChurchNumeral<T>
ChurchBoolean<T>
```

`IsZero` connects them:

```text
Church numeral
      ↓
   IsZero
      ↓
Church boolean
```

For C# to express that connection, the numeral applies transformations to
Church booleans:

```csharp
ChurchNumeral<ChurchBoolean<T>>
```

The resulting value is:

```csharp
ChurchBoolean<T>
```

No new representation is needed. The existing encodings now compose into a
small computational system.

## Three views of IsZero

### Lambda Calculus

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

### C#

```csharp
public static ChurchBoolean<T> IsZero<T>(
    ChurchNumeral<ChurchBoolean<T>> n) =>
    n(_ => False<T>)(True<T>);
```

The method parameter represents `λn`. The remaining body stays close to:

```text
n (λx.FALSE) TRUE
```

The explicit type argument tells C# which homogeneous Church boolean is being
used. `False<T>` and `True<T>` are the existing Church boolean functions.

### Meaning

> Start from TRUE and let the numeral repeatedly replace the value with FALSE.

## The surprising part

`IsZero` does not ask the numeral:

> What number are you?

It asks:

> How many times do you apply this behavior?

We did not inspect the encoded data. We chose a behavior that makes the
distinction observable.

That gives:

```text
ISZERO ZERO  → TRUE
ISZERO ONE   → FALSE
ISZERO TWO   → FALSE
ISZERO THREE → FALSE
```

## The v0.1 journey

Starting from functions and function application, we progressively built:

```text
Identity
Booleans
Logical operators
Numbers
Successor
Addition
Multiplication
Pairs
Predicates
```

These functions now represent values, control-like behavior, arithmetic, data
structures, and questions about encoded values.

Future milestones might explore predecessor, subtraction, comparisons,
recursion, factorial, and lists. They are not part of v0.1.
