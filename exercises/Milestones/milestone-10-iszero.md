# Milestone 10 — IsZero

## Goal

Implement the Church predicate:

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

The result must be a **Church boolean**, not a C# `bool`.

The central idea is:

> ZERO applies the supplied function zero times, so the initial TRUE survives.  
> Every positive Church numeral applies the supplied function at least once, replacing the result with FALSE.

Expected behavior:

```text
ISZERO ZERO  → TRUE
ISZERO ONE   → FALSE
ISZERO TWO   → FALSE
ISZERO THREE → FALSE
```

This is the final functional milestone of v0.1.

Do not implement predecessor, subtraction, comparisons, recursion, factorial, lists, strings, or any later concept.

---

## Before you start

Read:

```text
docs/specs/v0.1.md
docs/01-functions.md
docs/02-identity.md
docs/03-booleans.md
docs/04-logic.md
docs/05-church-numerals.md
docs/06-successor.md
docs/07-addition.md
docs/08-multiplication.md
docs/09-pairs.md
```

The project already contains:

```text
Church booleans
Church numerals
```

Reuse those encodings directly.

Do not introduce new boolean or numeral representations.

---

## 1. Start from the problem

You now have Church numerals, but you still cannot ask them questions.

For example:

> Is this numeral zero?

In ordinary C#, you might write:

```csharp
value == 0
```

But that would bypass everything built so far.

Instead ask:

> Can the behavior of a Church numeral itself tell us whether it is zero?

That is the goal of this milestone.

---

## 2. Recall the crucial difference

Remember:

```text
ZERO
```

applies a supplied function:

```text
zero times
```

while every positive numeral applies it:

```text
at least once
```

That difference is enough to build a predicate.

No inspection is required.

No integer conversion is required.

---

## 3. Build the intuition before the formula

Start from:

```text
TRUE
```

Now imagine a function that ignores its input and always returns:

```text
FALSE
```

In Lambda Calculus:

```text
λx.FALSE
```

Call this idea:

```text
alwaysFalse
```

Now let a Church numeral decide how many times to apply it.

### With ZERO

Start from:

```text
TRUE
```

ZERO applies the function zero times.

So nothing happens:

```text
TRUE
```

The result stays `TRUE`.

### With ONE

Start from:

```text
TRUE
↓ alwaysFalse
FALSE
```

The result becomes `FALSE`.

### With TWO

```text
TRUE
↓ alwaysFalse
FALSE
↓ alwaysFalse
FALSE
```

Once the first application produces `FALSE`, every later application still produces `FALSE`.

Therefore:

```text
ZERO      → TRUE
positive  → FALSE
```

---

## 4. Reveal the formula

Now introduce:

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

Read it slowly:

> Take numeral `n`.  
> Ask `n` to repeatedly apply the function `λx.FALSE`.  
> Start from `TRUE`.

Or more compactly:

> Let `n` decide how many times “replace this with FALSE” is applied to TRUE.

If the function is applied zero times, `TRUE` survives.

If it is applied at least once, the result becomes `FALSE`.

---

## 5. Understand the important part

Focus on:

```text
n (λx.FALSE) TRUE
```

The first argument is:

```text
λx.FALSE
```

which means:

> Ignore the current value and return FALSE.

The starting value is:

```text
TRUE
```

Then the numeral decides how many times the transformation runs.

This is why the predicate works.

`IsZero` never needs to ask which numeral it received.

---

## 6. Walk through ISZERO ZERO

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

ZERO ignores the function and returns the initial value.

Therefore:

```text
ZERO (λx.FALSE) TRUE
→ TRUE
```

So:

```text
ISZERO ZERO → TRUE
```

No formal beta-reduction notation is needed.

Just follow the behavior.

---

## 7. Walk through ISZERO TWO

Now consider:

```text
ISZERO TWO
```

which becomes:

```text
TWO (λx.FALSE) TRUE
```

Recall that `TWO` applies its function twice.

First application:

```text
(λx.FALSE) TRUE
→ FALSE
```

Second application:

```text
(λx.FALSE) FALSE
→ FALSE
```

Therefore:

```text
ISZERO TWO → FALSE
```

The important point is:

> We never inspected TWO and never converted it to the integer 2.

Its behavior told us the answer.

---

## 8. Reuse the existing encodings

Work with the Church numeral and Church boolean abstractions already present in the project.

Do not create:

- another boolean representation;
- another numeral representation;
- wrapper classes;
- predicate objects;
- interfaces;
- primitive-backed abstractions.

`IsZero` should connect the existing models directly.

Conceptually:

```text
Church numeral
      ↓
    IsZero
      ↓
Church boolean
```

This is an important moment in the kata.

Previously separate encodings now compose.

---

## 9. Reference implementation

Work under:

```text
src/LambdaFromScratch/Predicates/
```

Expose:

```text
IsZero
```

Keep the implementation as close as practical to:

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

The exact C# syntax will depend on the existing:

```text
ChurchBoolean<T>
ChurchNumeral<T>
```

delegates.

A conceptual shape is:

```csharp
n => n(_ => False)(True)
```

or the nearest typed equivalent supported by the current API.

Do not distort the design just to mimic this pseudocode exactly.

Prefer the smallest implementation that keeps the Lambda structure visible.

---

## 10. Typing considerations

`IsZero` must return a Church boolean.

A conceptual type relation is:

```text
ChurchNumeral<ChurchBoolean<T>>
    →
ChurchBoolean<T>
```

or the nearest practical equivalent supported by the existing delegate design.

If C# requires:

- explicit generic type arguments;
- a small local helper;
- slightly more verbose delegate syntax;

that is acceptable.

Keep any typing workaround local and minimal.

Do not introduce interfaces or wrapper classes just to make the types look elegant.

Clarity of the Lambda encoding is more important.

---

## 11. Lambda Core restrictions

Inside `IsZero`, do not use:

```csharp
int
bool
ToInt
ToBool
==
!=
<
>
switch
if
?:
for
foreach
while
Enumerable
```

Also:

- do not compare the numeral with `Zero`;
- do not inspect the numeral structurally;
- do not convert it to an integer;
- do not convert the result to a C# boolean;
- do not use C# control flow.

The predicate must emerge from function application alone.

---

## 12. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Predicates/
```

Test at least:

```text
IsZero(Zero)  → TRUE
IsZero(One)   → FALSE
IsZero(Two)   → FALSE
IsZero(Three) → FALSE
```

Prefer observing the result directly as a Church boolean.

For example:

```csharp
var result = IsZero(Zero);

Assert.Equal("yes", result("yes")("no"));
```

and:

```csharp
var result = IsZero(Two);

Assert.Equal("no", result("yes")("no"));
```

Adapt the exact syntax to the generic signatures already present in the repository.

This direct behavioral observation should be the primary test style.

---

## 13. Observation tests

If a `ToBool` observation adapter already exists, you may also add concise tests such as:

```text
ToBool(IsZero(Zero)) → true
ToBool(IsZero(One))  → false
ToBool(IsZero(Two))  → false
```

These are secondary.

`ToBool` must not participate in the implementation of `IsZero`.

The dependency remains:

```text
IsZero
  ↓
Church boolean
  ↓
Observation
  ↓
bool
```

Never the reverse.

---

## 14. Kata implementation

Create the corresponding incomplete exercise under:

```text
exercises/LambdaFromScratch.Kata/Predicates/
```

Expose:

```text
IsZero
```

with the same conceptual API as the reference implementation.

The kata project must compile.

Do not include the completed C# implementation in comments.

A comment containing only the Lambda definition is enough:

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

The learner must derive the implementation from the formula and the documentation.

---

## 15. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Predicates/
```

At minimum test:

```text
ISZERO ZERO → TRUE
ISZERO ONE  → FALSE
ISZERO TWO  → FALSE
```

Observe the returned Church boolean using two alternative values.

For example, the result should be able to choose between:

```text
"yes"
"no"
```

The kata tests should fail until the learner implements `IsZero`.

Do not skip or disable them.

---

## 16. Documentation

Create:

```text
docs/10-predicates.md
```

Start from the problem:

> We now have numbers, but we still cannot ask them questions.

Show the ordinary C# temptation:

```csharp
value == 0
```

Then reject it for this exercise.

Ask instead:

> Can the behavior of a Church numeral itself tell us whether it is zero?

Build the intuition from:

```text
ZERO applies a function zero times
positive numerals apply it at least once
```

Only then reveal the Lambda formula.

---

## 17. Use the three views

### Lambda Calculus

```text
ISZERO = λn.n (λx.FALSE) TRUE
```

### C#

Show the actual project implementation.

Keep it structurally close, where possible, to:

```csharp
n => n(_ => False)(True)
```

or the exact typed equivalent required by the existing delegates.

### Meaning

> Start from TRUE and let the numeral repeatedly replace the value with FALSE.

Keep this presentation consistent with the previous chapters.

---

## 18. The surprising part

`IsZero` does not ask:

> What number are you?

It asks:

> How many times do you apply this behavior?

The result emerges from the numeral's behavior.

A useful way to state the idea is:

> We did not inspect the encoded data. We chose a behavior that makes the distinction observable.

That connects directly to the broader theme of LambdaFromScratch.

---

## 19. What you should understand

At the end of this milestone, you should be able to read:

```text
λn.n (λx.FALSE) TRUE
```

and understand the mechanism:

```text
start with TRUE
apply "always FALSE" as many times as n says
```

Therefore:

```text
ZERO      → TRUE
non-zero  → FALSE
```

You should also understand that `IsZero` connects two encodings built independently:

```text
Church numerals
Church booleans
```

The result is a small computational system built entirely from functions and application.

---

## 20. Close the v0.1 journey

This is the final functional milestone of v0.1.

You started with:

```text
λx.x
```

and progressively built:

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

All from functions and function application.

The important conclusion is:

> We started with functions and function application, and progressively rebuilt values, control-like behavior, arithmetic, data structures, and predicates.

Possible future explorations include:

```text
Predecessor
Subtraction
Comparisons
Recursion
Factorial
Lists
```

Do not implement them in this version.

---

## Constraints

For this milestone, do not introduce:

- predecessor;
- subtraction;
- comparison operators;
- recursion;
- fixed-point combinators;
- factorial;
- Church lists;
- Church strings.

Do not implement:

```text
PREDECESSOR
SUBTRACT
LEQ
EQ
Y combinator
fixed-point combinators
FACTORIAL
Church lists
Church strings
```

Also:

- do not add external dependencies;
- do not refactor previous milestones unless strictly necessary;
- do not use primitive-backed shortcuts inside `IsZero`.

---

## Completion checklist

Before considering v0.1 complete, verify that:

- [ ] `IsZero` exists in the reference implementation;
- [ ] `IsZero(Zero)` behaves like Church `True`;
- [ ] `IsZero(One)` behaves like Church `False`;
- [ ] `IsZero(Two)` behaves like Church `False`;
- [ ] `IsZero(Three)` behaves like Church `False`;
- [ ] the result is a Church boolean, not a C# `bool`;
- [ ] direct Church-boolean behavioral tests are present;
- [ ] any `ToBool` tests are observation-only;
- [ ] `IsZero` contains no `int`;
- [ ] `IsZero` contains no `bool`;
- [ ] `IsZero` contains no equality comparison;
- [ ] `IsZero` contains no C# control flow;
- [ ] `IsZero` does not use `ToInt` or `ToBool`;
- [ ] existing Church numeral and boolean encodings are reused;
- [ ] reference tests pass;
- [ ] the kata project compiles;
- [ ] kata tests fail until the learner implements `IsZero`;
- [ ] `docs/10-predicates.md` exists;
- [ ] no post-v0.1 concept has been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently.

The new `IsZero` kata tests should fail until the learner completes the exercise.

If the full solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Milestone 10 completes the functional scope of v0.1.**
