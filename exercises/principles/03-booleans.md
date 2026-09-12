# 03 — Booleans

## Start from C#

In everyday C#, a boolean is data:

```csharp
bool value = true;
```

We often inspect that data with control flow:

```csharp
if (value)
{
    return "yes";
}

return "no";
```

Now remove both tools:

> What if we remove both `bool` and `if`?

Instead of storing truth as data and later asking which branch to take, we can model truth as the choice itself. Give it two alternatives and let its behavior select one.

## TRUE

Church encoding defines `TRUE` as:

```text
TRUE = λx.λy.x
```

Read it slowly:

> Take `x`.  
> Take `y`.  
> Return `x`.

The Lambda Calculus and C# shapes line up directly:

```text
λx.λy.x
x => y => x
```

Using the C# function:

```csharp
True("yes")("no")
```

returns:

```text
"yes"
```

`TRUE` means: when given two alternatives, choose the first one.

## FALSE

Church encoding defines `FALSE` as:

```text
FALSE = λx.λy.y
```

Read it just as slowly:

> Take `x`.  
> Take `y`.  
> Return `y`.

Its C# shape is:

```csharp
x => y => y
```

Using the function:

```csharp
False("yes")("no")
```

returns:

```text
"no"
```

`FALSE` means: when given two alternatives, choose the second one.

## The important conceptual shift

A normal C# boolean is usually understood as data:

```text
true
false
```

A Church boolean is behavior:

```text
TRUE  → choose the first alternative
FALSE → choose the second alternative
```

We did not store the information "true" anywhere. We encoded what "true" does.

This is not a recommendation for representing booleans in production C# applications. It is an experiment in discovering how much we can construct using functions alone.

## Three views of Church booleans

### Lambda Calculus

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

### C#

```csharp
x => y => x
x => y => y
```

### Meaning

```text
TRUE  → choose the first value
FALSE → choose the second value
```

## Reading exercise

Without naming this function, look at:

```text
λfirst.λsecond.second
```

> Which argument does this function return?

Its C# shape is:

```csharp
first => second => second
```

It returns the second argument. The variable names changed, but its structure is identical to:

```text
λx.λy.y
```

It therefore behaves exactly like `FALSE`.

## What comes next?

> If TRUE and FALSE are functions, can logical operations themselves also be functions?

The next milestone will construct logical behavior from the booleans we have just built.
