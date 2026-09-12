# 02 — Identity

## Start from C#

Here is a small generic C# method:

```csharp
static T Identity<T>(T value)
{
    return value;
}
```

It accepts a value and returns that same value. It does not inspect, copy, or transform anything.

The method body can be expressed as a C# lambda:

```csharp
x => x
```

Lambda Calculus gives this function a name and writes it as:

```text
ID = λx.x
```

Read it as:

> Take `x` and return `x`.

Its structure is direct:

```text
λx.   → take x
x     → return x
```

## Three views of Identity

### Lambda Calculus

```text
ID = λx.x
```

### C#

```csharp
x => x
```

### Meaning

> Take a value and return exactly that value.

Identity works with any type because its behavior does not depend on the value:

```csharp
Identity("hello") // "hello"
Identity(42)      // 42
```

Identity may appear trivial. That is intentional.

The purpose of this kata is not to build useful business functionality. It is to establish the basic translation between Lambda Calculus and C# before we introduce more interesting encodings.

## A small reading exercise

Before reading further, translate this expression mentally into C#:

```text
λmessage.message
```

The C# shape is:

```csharp
message => message
```

The name `message` has no special meaning. These functions all have the same structure:

```text
λx.x

λmessage.message

λanything.anything
```

Each takes one value and returns that same value. Renaming the parameter does not change what the function does.
