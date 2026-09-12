# 04 — Logic

We already have two functions:

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

`TRUE` chooses the first alternative. `FALSE` chooses the second.

> If choosing between two alternatives is enough to represent true and false, can we build logical operations using nothing but those choices?

## NOT

First, watch `TRUE` select normally:

```text
TRUE a b
   ↓
   a
```

To reverse that choice, give the alternatives to the boolean in reverse order:

```text
NOT TRUE a b
     ↓
TRUE b a
     ↓
     b
```

`NOT` does not calculate a new boolean value. It reverses the alternatives.

### Lambda Calculus

```text
NOT = λp.λx.λy.p y x
```

### C#

```csharp
p => x => y => p(y)(x)
```

### Meaning

> Swap the alternatives selected by `p`.

Therefore:

```text
NOT TRUE  → FALSE
NOT FALSE → TRUE
```

## IF

Ordinary C# makes the condition part of an `if` statement:

```csharp
if (condition)
{
    return whenTrue;
}

return whenFalse;
```

But a Church boolean already chooses between two alternatives:

```text
condition whenTrue whenFalse
```

The condition does not need an `if` statement. The condition itself knows which branch to select.

### Lambda Calculus

```text
IF = λp.λx.λy.p x y
```

### C#

```csharp
p => x => y => p(x)(y)
```

### Meaning

> Give both alternatives to `p` and let it choose.

In C#:

```csharp
If(condition)("yes")("no")
```

`IF` does almost nothing because the Church boolean already is the decision.

## AND

Reason about `p AND q` before looking at the formula:

- if `p` is `FALSE`, the result must be `FALSE`, regardless of `q`;
- if `p` is `TRUE`, the result depends on `q`.

So `p` chooses between the result selected by `q` and the false alternative.

### Lambda Calculus

```text
AND = λp.λq.λx.λy.p (q x y) y
```

### C#

```csharp
p => q => x => y => p(q(x)(y))(y)
```

### Meaning

> Let `q` choose between `x` and `y`, then let `p` choose between that result and `y`.

Consider `TRUE AND FALSE`:

1. `FALSE` chooses `y` from `x` and `y`.
2. `TRUE` receives that result as its first alternative and `y` as its second.
3. `TRUE` chooses the first alternative, which is `y`.
4. The combined function therefore behaves like `FALSE`.

This gives the expected behavior:

```text
TRUE  AND TRUE  → TRUE
TRUE  AND FALSE → FALSE
FALSE AND TRUE  → FALSE
FALSE AND FALSE → FALSE
```

## OR

Reason similarly about `p OR q`:

- if `p` is `TRUE`, the result must be `TRUE`, regardless of `q`;
- if `p` is `FALSE`, the result depends on `q`.

So `p` chooses between the true alternative and the result selected by `q`.

### Lambda Calculus

```text
OR = λp.λq.λx.λy.p x (q x y)
```

### C#

```csharp
p => q => x => y => p(x)(q(x)(y))
```

### Meaning

> Let `p` choose between `x` and the result that `q` selects.

Consider `FALSE OR TRUE`:

1. `TRUE` chooses `x` from `x` and `y`.
2. `FALSE` receives `x` as its first alternative and that result as its second.
3. `FALSE` chooses the second alternative, which is `x`.
4. The combined function therefore behaves like `TRUE`.

This gives:

```text
TRUE  OR TRUE  → TRUE
TRUE  OR FALSE → TRUE
FALSE OR TRUE  → TRUE
FALSE OR FALSE → FALSE
```

## What we have learned

No operator inspected or converted a stored boolean. Each one composed functions that choose between alternatives:

```text
NOT → reverse the alternatives
IF  → let the condition choose
AND → choose between q's result and false
OR  → choose between true and q's result
```

Logical behavior emerged from function application alone.
