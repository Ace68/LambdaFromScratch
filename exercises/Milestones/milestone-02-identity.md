# Milestone 2 — Identity

## Goal

Implement the first executable Lambda Calculus function:

```text
ID = λx.x
```

You should understand this as:

> Take `x` and return `x`.

And recognize the direct C# equivalent:

```csharp
x => x
```

This milestone introduces the first complete cycle of the kata:

- reference implementation;
- reference tests;
- kata implementation;
- kata tests;
- documentation.

Do not introduce booleans or any later Lambda Calculus concept yet.

---

## Before you start

Read:

```text
docs/specs/v0.1.md
docs/01-functions.md
```

Make sure you are comfortable reading:

```text
λx.x
```

as:

```csharp
x => x
```

---

## 1. Reference implementation

Work under:

```text
src/LambdaFromScratch/Functions/
```

Implement an `Identity` function.

The implementation should stay as close as possible to:

```text
λx.x
```

and therefore to:

```csharp
x => x
```

Keep it deliberately small.

A generic implementation is appropriate because Identity should work with any type.

The API should be simple to use:

```csharp
Identity("hello")
```

should return:

```text
"hello"
```

and:

```csharp
Identity(42)
```

should return:

```text
42
```

Choose the smallest C# representation that satisfies these requirements.

Do not introduce:

- class hierarchies;
- interfaces;
- services;
- extension-method infrastructure;
- general-purpose functional abstractions.

The point of this step is to express one Lambda Calculus function clearly in C#.

---

## 2. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Functions/
```

Write tests for at least these behaviors:

```text
Identity returns the same string

Identity returns the same integer

Identity returns the same reference
```

Keep the tests simple and explicit.

A test should read clearly enough that its purpose is immediately obvious:

```csharp
[Fact]
public void Identity_returns_the_same_string()
{
    var result = Identity("hello");

    Assert.Equal("hello", result);
}
```

For a reference type, verify that `Identity` returns the **same instance**, not just an equivalent value.

Do not introduce shared fixtures or test abstractions.

---

## 3. Kata implementation

Create the corresponding incomplete exercise under:

```text
exercises/LambdaFromScratch.Kata/Functions/
```

Expose an API that matches the reference implementation closely enough that the learner is solving the same conceptual problem.

The kata project must still compile.

Do not include the solution in comments.

Use only a minimal placeholder that:

- allows compilation;
- does not implement Identity correctly;
- causes the corresponding kata tests to fail.

A short comment is enough:

```text
Implement ID = λx.x
```

Do not explain how to solve the exercise in the source code.

The explanation belongs in the documentation.

---

## 4. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Functions/
```

The tests should guide the learner toward this behavior:

```text
Identity(x) == x
```

without exposing the implementation.

Test at least:

- a string;
- an integer.

Keep the tests intentionally small.

The kata tests are expected to fail until the learner implements Identity.

Do not disable them.

Do not skip them.

---

## 5. Documentation

Create:

```text
docs/02-identity.md
```

Start from familiar C#.

For example:

```csharp
static T Identity<T>(T value)
{
    return value;
}
```

Then simplify the idea toward:

```csharp
x => x
```

Finally introduce the Lambda Calculus form:

```text
ID = λx.x
```

Explain the structure:

```text
λx.   → take x
x     → return x
```

Use the three views that will recur throughout LambdaFromScratch.

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

Identity may look trivial.

That is intentional.

The goal here is not to build useful business functionality. The goal is to establish the translation between Lambda Calculus and C# before moving to more interesting encodings.

---

## 6. Small reading exercise

Look at:

```text
λmessage.message
```

Before reading further, translate it mentally into C#.

The equivalent shape is:

```csharp
message => message
```

Now compare:

```text
λx.x
λmessage.message
λanything.anything
```

These functions all have the same structure.

The parameter name does not change what the function does.

Do not introduce formal terminology for this yet. It is enough to notice that renaming the parameter does not change the behavior.

---

## 7. What you should understand

At the end of this milestone, you should be able to move comfortably between these three representations:

```text
ID = λx.x
```

```csharp
x => x
```

```text
Take a value and return exactly that value.
```

You should also have seen the basic structure that the rest of the kata will follow:

```text
Lambda Calculus
      ↓
reference implementation
      ↓
reference tests
      ↓
kata implementation
      ↓
kata tests
```

The implementation is deliberately small so that the notation remains the focus.

---

## Constraints

For this milestone:

- do not introduce Church booleans;
- do not introduce Church numerals;
- do not introduce combinators beyond Identity;
- do not introduce beta reduction terminology unless strictly necessary;
- do not add new dependencies;
- do not introduce interfaces;
- do not introduce services;
- do not introduce extension-method infrastructure;
- do not create a general-purpose functional library.

Keep this milestone deliberately small.

---

## Completion checklist

Before moving to Milestone 3, verify that:

- [ ] the reference `Identity` implementation exists;
- [ ] `Identity("hello")` returns `"hello"`;
- [ ] `Identity(42)` returns `42`;
- [ ] `Identity` returns the same reference for reference types;
- [ ] the reference tests are present and pass;
- [ ] the kata implementation compiles but is still incomplete;
- [ ] the kata tests are present and fail until the learner implements Identity;
- [ ] `docs/02-identity.md` exists;
- [ ] the documentation explains `ID = λx.x`;
- [ ] no concept beyond Identity has been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests separately and verify that they pass.

Run the kata tests separately.

They are expected to fail while the kata implementation is still incomplete.

If:

```bash
dotnet test LambdaFromScratch.slnx
```

reports failure because the kata tests intentionally fail, that is acceptable at this stage.

Do not modify, disable, or skip the kata tests just to make the full solution green.

**Stop here. Do not start Milestone 3 yet.**
