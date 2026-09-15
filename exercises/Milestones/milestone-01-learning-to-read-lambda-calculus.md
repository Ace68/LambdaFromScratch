# Milestone 1 — Learning to Read Lambda Calculus

## Goal

Before implementing anything, learn how to **read simple Lambda Calculus expressions** and relate them to familiar C# syntax.

At the end of this milestone, you should be able to look at expressions such as:

```text
λx.x

λx.λy.x

f x
```

and begin translating them mentally into C#.

This is not a mathematical introduction to Lambda Calculus.  
The goal is to build just enough notation literacy to continue the kata.

---

## Before you start

Read:

```text
milestone-00-before-to-start.md
```

For this milestone, work on documentation only.

Do **not** implement `Identity` or any other Lambda Calculus function yet.

---

## Your task

Create:

```text
Principles/01-functions.md
```

Write a short introductory chapter for experienced C# developers who have never studied Lambda Calculus.

Assume the reader:

- knows C# well;
- understands lambda expressions such as `x => x`;
- understands `Func<>`;
- understands methods and function calls;
- does not know Lambda Calculus notation.

Start from familiar C# syntax and gradually introduce the corresponding Lambda Calculus notation.

---

## 1. Start from C#

Begin with:

```csharp
x => x
```

Explain in plain language what this function does:

1. it receives `x`;
2. it returns `x`.

Then introduce:

```text
λx.x
```

Explain how to read it:

> A function that takes x and returns x.

Describe the role of:

```text
λ
x
.
x
```

Keep the explanation practical.

You may introduce the term **lambda abstraction**, but only after the idea itself is clear.

---

## 2. Understand the dot

Explain the role of the dot in:

```text
λx.x
```

The reader should understand that it separates the parameter declaration from the function body.

Relate the Lambda Calculus expression directly to C#:

```text
λx.   →   x =>
x     →   x
```

and therefore:

```text
λx.x
```

corresponds approximately to:

```csharp
x => x
```

---

## 3. Functions returning functions

Now introduce:

```text
λx.λy.x
```

Do not assume that its structure is obvious.

Try formatting it progressively:

```text
λx.
    λy.
        x
```

Read it step by step:

> Take x.  
> Return another function.  
> That function takes y.  
> Return x.

Compare it with:

```csharp
x => y => x
```

Make explicit that:

```csharp
x => y => x
```

means:

```csharp
x => (y => x)
```

This is your first encounter with the idea behind **currying**. Keep the explanation practical rather than formal.

---

## 4. Function application

Introduce:

```text
f x
```

Explain that it means:

> Apply function `f` to `x`.

Compare it with familiar C# syntax:

```csharp
f(x)
```

Highlight the syntactic difference:

```text
Lambda Calculus: f x
C#:              f(x)
```

Then try the nested expression:

```text
f (f x)
```

and compare it with:

```csharp
f(f(x))
```

Do not introduce Church numerals yet.

---

## 5. Application associates to the left

Learn this small but important reading rule:

```text
f x y
```

means:

```text
(f x) y
```

not:

```text
f (x y)
```

Relate it to a curried C# function:

```csharp
f(x)(y)
```

A short explanation is enough.

---

## 6. Lambda bodies extend to the right

Consider:

```text
λx.f x
```

Read it as:

```text
λx.(f x)
```

The body of the lambda continues to the right.

You do not need to study formal precedence rules yet.  
The goal is simply to learn how to group simple expressions correctly.

---

## 7. Reading exercise

Before looking at the explanations, try to read these expressions aloud and translate them mentally into C#:

```text
λx.x

λx.λy.y

λf.λx.f x

λf.λx.f (f x)
```

For each expression, write:

1. a short plain-English reading;
2. the corresponding C# shape.

For example, your explanations should be understandable to a C# developer without requiring mathematical terminology.

Do not investigate Church numerals yet, even if some expressions look related to something you have seen before. That discovery belongs to a later milestone.

---

## 8. What you should know at the end

By the end of this milestone, you should recognize:

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

You do not need a complete understanding of Lambda Calculus yet.

You only need enough notation to begin building functions with it.

> We now know enough Lambda Calculus to build our first function from scratch.

---

## Constraints

Keep `docs/01-functions.md`:

- practical;
- conversational;
- precise;
- short;
- aimed at developers rather than mathematicians.

Do not introduce topics that are not needed yet, including:

- a detailed history of Lambda Calculus;
- Alonzo Church's biography;
- Turing completeness;
- beta reduction;
- alpha conversion;
- substitution rules;
- free and bound variables;
- Church encodings;
- combinators beyond what is necessary for this exercise.

---

## Completion checklist

Before moving to Milestone 2, verify that:

- [ ] `docs/01-functions.md` exists;
- [ ] the document starts from familiar C# syntax;
- [ ] `λx.x` is explained clearly;
- [ ] the role of the dot is explained;
- [ ] `λx.λy.x` is related to `x => y => x`;
- [ ] function application such as `f x` is related to `f(x)`;
- [ ] left-associative application is mentioned;
- [ ] lambda bodies extending to the right are explained;
- [ ] the reading exercise is included;
- [ ] no Lambda Calculus implementation has been added.

Then run:

```bash
dotnet build LambdaFromScratch.slnx
dotnet test LambdaFromScratch.slnx
```

Both commands should succeed before continuing.

**Stop here. Do not start Milestone 2 yet.**
