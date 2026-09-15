# Milestone 3 — Church Booleans

## Goal

Introduce Church booleans:

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

The central idea of this milestone is:

> A boolean does not have to be represented as a stored `bool`.  
> It can be represented as behavior.

In Church encoding:

```text
TRUE  → choose the first value
FALSE → choose the second value
```

By the end of this milestone, you should recognize the direct structural relationship between:

```text
TRUE = λx.λy.x
```

and:

```csharp
x => y => x
```

and between:

```text
FALSE = λx.λy.y
```

and:

```csharp
x => y => y
```

Do not implement `NOT`, `AND`, `OR`, `IF`, Church numerals, or any later concept yet.

---

## Before you start

Read:

```text
Milestones/milestone-00-before-to-start.md
Principles/01-functions.md
Principles/02-identity.md
```

Make sure you are comfortable reading:

```text
λx.λy.x
```

as:

```csharp
x => y => x
```

and understanding it as:

> Take `x`, take `y`, return `x`.

---

## 1. Start from familiar C#

In ordinary C#, a boolean is usually represented as data:

```csharp
bool value = true;
```

And choosing between two alternatives often involves control flow:

```csharp
if (value)
{
    return "yes";
}

return "no";
```

Now ask a different question:

> What if we remove both `bool` and `if`?

Do not solve the problem with another conditional construct.

The goal of this milestone is to represent truth entirely through functions.

---

## 2. TRUE

Start from:

```text
TRUE = λx.λy.x
```

Read it slowly:

> Take `x`.  
> Take `y`.  
> Return `x`.

Now compare it with C#:

```csharp
x => y => x
```

Its behavior should be visible directly from usage:

```csharp
True("yes")("no")
```

should produce:

```text
"yes"
```

The important interpretation is:

> `TRUE` means: when given two alternatives, choose the first one.

---

## 3. FALSE

Now introduce:

```text
FALSE = λx.λy.y
```

Read it slowly:

> Take `x`.  
> Take `y`.  
> Return `y`.

Compare it with:

```csharp
x => y => y
```

Its usage should look like:

```csharp
False("yes")("no")
```

and produce:

```text
"no"
```

The interpretation is:

> `FALSE` means: when given two alternatives, choose the second one.

---

## 4. Reference implementation

Work under:

```text
src/LambdaFromScratch/Booleans/
```

Expose two generic Church boolean functions:

```text
True
False
```

The API should stay as close as reasonably possible to the Lambda Calculus shape:

```csharp
True("yes")("no")
False("yes")("no")
```

with:

```text
True("yes")("no")  → "yes"
False("yes")("no") → "no"
```

A Church boolean must fundamentally remain a function.

Do not:

- wrap the alternatives in objects;
- create a stateful `ChurchBoolean` class;
- store a C# boolean internally;
- replace the behavior with ordinary conditionals.

If a small named delegate improves readability, it is acceptable.

Avoid elaborate generic abstractions.

### Mandatory restriction

The implementation of `True` and `False` must not use:

```csharp
bool
if
switch
?:
```

Selection must happen through functions alone.

The implementation should remain recognizable as the translation of:

```text
λx.λy.x
```

and:

```text
λx.λy.y
```

---

## 5. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Booleans/
```

Test at least:

```text
True selects the first string

False selects the second string

True works with another value type

False works with another value type
```

Keep the tests direct.

For example:

```csharp
[Fact]
public void True_selects_the_first_value()
{
    var result = True("yes")("no");

    Assert.Equal("yes", result);
}
```

and:

```csharp
[Fact]
public void False_selects_the_second_value()
{
    var result = False("yes")("no");

    Assert.Equal("no", result);
}
```

Use xUnit only.

Do not use reflection to prove that `bool` was not used.

That restriction should be evident from the implementation itself.

---

## 6. Observation boundary

This milestone introduces an important distinction:

> A Lambda Calculus value and the way we observe it from C# are not necessarily the same thing.

A Church boolean does not need to contain or use a C# `bool`.

However, C# code may still want to inspect the result.

If it keeps the design simple, add a minimal observation adapter under:

```text
src/LambdaFromScratch/Observation/
```

Conceptually:

```csharp
ToBool(...)
```

may observe a Church boolean by supplying the ordinary C# values:

```csharp
true
false
```

so that:

```text
ToBool(True)  → true
ToBool(False) → false
```

Using `bool` here is acceptable because this is an **observation boundary**.

It is not part of the Church boolean implementation.

Do not use the observation adapter to implement `True` or `False`.

If introducing it makes the API noticeably more complicated, leave it out for now.

---

## 7. Kata implementation

Create the corresponding incomplete exercise under:

```text
exercises/LambdaFromScratch.Kata/Booleans/
```

Expose the same conceptual API:

```text
True
False
```

The kata project must compile.

The implementation must intentionally remain incomplete or incorrect so that the kata tests fail until the learner solves the exercise.

Do not include the solution in comments.

Short instructions are enough:

```text
Implement TRUE = λx.λy.x
```

and:

```text
Implement FALSE = λx.λy.y
```

Do not explain the C# implementation inside the source file.

---

## 8. Kata tests

Create the corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Booleans/
```

At minimum, verify:

```text
True selects the first value

False selects the second value
```

Use simple alternatives such as:

```text
"yes"
"no"
```

The tests should guide the learner toward the behavior without revealing the implementation.

They are expected to fail before the exercise is completed.

Do not disable or skip them.

---

## 9. Documentation

Create:

```text
docs/03-booleans.md
```

The chapter should make the conceptual shift visible.

Start from ordinary C#:

```csharp
bool value = true;
```

and familiar control flow:

```csharp
if (value)
{
    return "yes";
}

return "no";
```

Then ask:

> What if we remove both `bool` and `if`?

Lead the reader toward the idea that truth can be modeled as a choice between two alternatives.

Do not immediately jump to logical operators.

The surprising result of this chapter is simple enough:

```text
TRUE  = choose the first value
FALSE = choose the second value
```

---

## 10. The important conceptual shift

Make this distinction explicit.

A normal C# boolean is usually thought of as data:

```text
true
false
```

A Church boolean can instead be understood as behavior:

```text
TRUE  → choose the first alternative
FALSE → choose the second alternative
```

A useful sentence to keep in mind is:

> We did not store the information "true" anywhere. We encoded what "true" does.

Do not interpret this as a recommendation for production C# applications.

The point of the kata is to discover how much behavior can be built from functions alone.

---

## 11. The three views

Use the standard LambdaFromScratch presentation.

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

---

## 12. Reading exercise

Look at this expression before naming it:

```text
λfirst.λsecond.second
```

Ask yourself:

> Which argument does this function return?

Now translate it to C#:

```csharp
first => second => second
```

Compare it with:

```text
λx.λy.y
```

The names are different, but the structure is the same.

Therefore the behavior is the same as `FALSE`.

You do not need formal terminology for parameter renaming yet.

---

## 13. What you should understand

At the end of this milestone, you should be able to move between these representations:

```text
TRUE = λx.λy.x
```

```csharp
x => y => x
```

```text
Choose the first value.
```

and:

```text
FALSE = λx.λy.y
```

```csharp
x => y => y
```

```text
Choose the second value.
```

Most importantly, you should have seen that a boolean can be modeled without storing a boolean value.

It can be modeled entirely through behavior.

---

## 14. Prepare the next milestone

Now that `TRUE` and `FALSE` are functions, a natural question appears:

> If TRUE and FALSE are functions, can logical operations themselves also be functions?

That is the subject of the next milestone.

Do not implement or explain `NOT`, `AND`, `OR`, or `IF` yet.

---

## Constraints

For this milestone, do not implement:

```text
NOT
AND
OR
IF
ZERO
ONE
TWO
THREE
SUCC
ADD
MULT
PAIR
ISZERO
```

Also:

- do not add unrelated helpers;
- do not introduce production-style architecture;
- do not add dependencies;
- do not introduce boolean algebra theory;
- do not add truth tables yet;
- do not introduce beta reduction terminology;
- do not introduce later Church encodings.

Keep the surprise in the simplicity of the functions.

---

## Completion checklist

Before moving to Milestone 4, verify that:

- [ ] `True` exists in the reference implementation;
- [ ] `False` exists in the reference implementation;
- [ ] `True("yes")("no")` returns `"yes"`;
- [ ] `False("yes")("no")` returns `"no"`;
- [ ] the implementation uses no `bool`, `if`, `switch`, or `?:`;
- [ ] reference tests pass;
- [ ] the kata implementation compiles but remains incomplete;
- [ ] the kata tests fail until the learner solves the exercise;
- [ ] `docs/03-booleans.md` exists;
- [ ] the documentation explains booleans as behavior;
- [ ] no logical operator or later Church encoding has been implemented;
- [ ] any observation adapter, if present, remains separate from the Church boolean implementation.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently and verify that the new Church boolean tests fail for the expected reason.

If:

```bash
dotnet test LambdaFromScratch.slnx
```

reports failure because the kata tests intentionally fail, that is acceptable.

Do not modify or skip intentionally failing kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 4 yet.**
