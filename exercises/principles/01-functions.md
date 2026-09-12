# 01 — Functions

## Start from C#

This C# lambda should look familiar:

```csharp
x => x
```

It is a function that:

1. receives `x`;
2. returns `x`.

Lambda Calculus writes the same shape like this:

```text
λx.x
```

Read it aloud as:

> A function that takes x and returns x.

Each part has a small job:

```text
λ   begin a function
x   name its parameter
.   begin the function body
x   return x
```

After the plain idea is clear, we can give it its usual name: an expression that defines a function this way is a **lambda abstraction**.

## The dot

In:

```text
λx.x
```

the dot separates the parameter declaration from the body. C# uses `=>` for much the same visual boundary:

```text
λx.   →   x =>
x     →   x
```

So the mental translation is:

```text
λx.x   →   x => x
```

The syntax differs, but the function has the same shape.

## Functions returning functions

Now consider:

```text
λx.λy.x
```

Do not try to read the whole expression at once. Expand its structure:

```text
λx.
    λy.
        x
```

Then read it one step at a time:

> Take x.  
> Return another function.  
> That function takes y.  
> Return x.

The corresponding C# shape is:

```csharp
x => y => x
```

C# groups this as:

```csharp
x => (y => x)
```

The outer function takes `x` and returns the inner function. The inner function takes `y` and returns the `x` remembered from the outer function.

Writing a function as a sequence of one-parameter functions is called **currying**. For now, the practical rule is simply: each new `λ` introduces another function, just as each new `=>` does in the C# version.

## Function application

Defining functions is only half the story. We also need to use them.

In Lambda Calculus:

```text
f x
```

means:

> Apply function `f` to `x`.

C# expresses the same call with parentheses:

```text
Lambda Calculus: f x
C#:              f(x)
```

Application can be nested:

```text
f (f x)
```

The inner `f x` is evaluated as an argument to the outer `f`. Its familiar C# shape is:

```csharp
f(f(x))
```

This repeated application will become useful later. For now, only notice how to read it.

## Application associates to the left

When several values follow one another:

```text
f x y
```

read them from the left:

```text
(f x) y
```

not:

```text
f (x y)
```

For a curried C# function, the equivalent shape is:

```csharp
f(x)(y)
```

## Lambda bodies extend to the right

The body of a lambda continues as far to the right as the expression allows. Therefore:

```text
λx.f x
```

means:

```text
λx.(f x)
```

Mentally, read it as a function that takes `x` and returns the result of applying `f` to `x`.

## A first reading exercise

Try reading each expression before looking at its explanation.

### 1

```text
λx.x
```

Plain English: Take `x` and return `x`.

```csharp
x => x
```

### 2

```text
λx.λy.y
```

Plain English: Take `x`, return a function that takes `y`, then return `y`.

```csharp
x => y => y
```

### 3

```text
λf.λx.f x
```

Plain English: Take a function `f`, then take `x`, and apply `f` to `x`.

```csharp
f => x => f(x)
```

### 4

```text
λf.λx.f (f x)
```

Plain English: Take a function `f`, then take `x`, apply `f` to `x`, and apply `f` again to that result.

```csharp
f => x => f(f(x))
```

These examples are only reading exercises. Their broader uses will appear in later chapters.

## What we have learned

You should now recognize:

```text
λx.x
```

as approximately:

```csharp
x => x
```

You should recognize:

```text
f x
```

as approximately:

```csharp
f(x)
```

And you should recognize:

```text
λx.λy.x
```

as approximately:

```csharp
x => y => x
```

We now know enough Lambda Calculus to build our first function from scratch.
