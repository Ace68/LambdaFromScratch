# Milestone 6 — Successor

## Goal

Introduce the successor operation for Church numerals.

Use:

```text
SUCC = λn.λf.λx.f (n f x)
```

The central idea is:

> A Church numeral represents repeated application of a function.  
> Its successor applies that same function one additional time.

Conceptually:

```text
SUCC ZERO → ONE
SUCC ONE  → TWO
SUCC TWO  → THREE
```

Do not implement addition, multiplication, pairs, predicates, predecessor, subtraction, recursion, or any later concept yet.

---

## Before you start

Read:

```text
Milestones/milestone-00-before-to-start.md
Principles/01-functions.md
Principles/02-identity.md
Principles/01-functions.md
Principles/03-booleans.md
Principles/04-logic.md
Principles/05-church-numerals.md
```

The project already uses:

```csharp
public delegate Func<T, T> ChurchNumeral<T>(Func<T, T> f);
```

Preserve this representation.

Do not introduce another numeral abstraction.

---

## 1. Start from what you already know

From the previous milestone:

```text
ZERO  = apply f zero times
ONE   = apply f once
TWO   = apply f twice
THREE = apply f three times
```

So far, every numeral was written manually.

Now ask:

> Can we construct the next numeral from an existing one?

That is what `Successor` does.

---

## 2. Build the intuition with TWO

Recall:

```text
TWO = λf.λx.f (f x)
```

It applies `f` twice.

To obtain `THREE`, imagine doing exactly what `TWO` already does and then applying `f` once more:

```text
x
↓ f
f(x)
↓ f
f(f(x))
↓ one more f
f(f(f(x)))
```

So the successor of a numeral should mean:

```text
do what n already does
then apply f one more time
```

Only now introduce the formula:

```text
SUCC = λn.λf.λx.f (n f x)
```

---

## 3. Read the expression structurally

Format it progressively:

```text
λn.
    λf.
        λx.
            f (n f x)
```

Read it as:

> Take a Church numeral `n`.  
> Take a transformation `f`.  
> Take a starting value `x`.  
> Let `n` apply `f` to `x`.  
> Apply `f` one more time to the result.

The structure should remain visible in the C# implementation.

---

## 4. Understand the important part

Focus on:

```text
f (n f x)
```

Break it into two steps.

First:

```text
n f x
```

means:

> Let `n` apply `f` its number of times to `x`.

Then:

```text
f (...)
```

means:

> Apply `f` one additional time.

Therefore:

```text
SUCC n
```

means:

> Do what `n` already does, plus one more function application.

Do not think of this as adding `1` to a stored number.

There is no stored number.

---

## 5. Reference implementation

Work under:

```text
src/LambdaFromScratch/Numerals/
```

Expose:

```text
Successor
```

Keep it curried and structurally close to:

```text
SUCC = λn.λf.λx.f (n f x)
```

The C# shape should remain recognisable as:

```csharp
n => f => x =>
    f(n(f)(x))
```

Adapt the exact syntax to the existing project conventions.

Do not make the implementation more abstract than necessary.

---

## 6. Lambda Core restrictions

The implementation of `Successor` must not use:

```csharp
int
+
++
for
foreach
while
do
Enumerable
ToInt
```

Also:

- do not use counters;
- do not inspect the numeral;
- do not convert it to a primitive representation;
- do not use numeric addition internally.

`Successor` must work exclusively through function application.

---

## 7. Walk through SUCC TWO

Start from:

```text
SUCC TWO
```

Using:

```text
SUCC = λn.λf.λx.f (n f x)
```

replace `n` with `TWO`:

```text
λf.λx.f (TWO f x)
```

Now recall:

```text
TWO f x = f (f x)
```

So:

```text
f (TWO f x)
```

becomes:

```text
f (f (f x))
```

which is exactly the behavior of:

```text
THREE
```

Therefore:

```text
SUCC TWO → THREE
```

Keep this reasoning informal.

You do not need formal beta-reduction notation yet.

---

## 8. Use a non-numeric example

Reuse a transformation such as:

```csharp
Func<string, string> addStar = value => value + "*";
```

You already know:

```text
TWO addStar ""

→ "**"
```

Now apply `Successor`:

```text
SUCC TWO addStar ""

→ "***"
```

This is the key insight:

> Successor does not add the integer `1`.  
> It adds one more application of the transformation.

---

## 9. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Numerals/
```

Test at least:

```text
Successor of Zero behaves like One

Successor of One behaves like Two

Successor of Two behaves like Three
```

You may use the existing observation adapter:

```text
ToInt(Successor(Zero)) == 1
ToInt(Successor(One))  == 2
ToInt(Successor(Two))  == 3
```

But do not test `Successor` only through integers.

Add at least one behavioral test using a non-numeric transformation.

For example:

```csharp
Func<string, string> addStar = value => value + "*";

var result = Successor(Two)(addStar)("");

Assert.Equal("***", result);
```

This demonstrates that `Successor` adds one more function application.

Keep the tests explicit and simple.

Use xUnit only.

---

## 10. Observation boundary

It is acceptable to write:

```text
ToInt(Successor(Two)) → 3
```

But `ToInt` only observes the result.

It does not participate in building the successor.

The dependency remains:

```text
Successor
    ↓
Church numeral
    ↓
Observation
    ↓
int
```

Never:

```text
int
 ↓
Successor implementation
```

---

## 11. Kata implementation

Create the corresponding incomplete exercise under:

```text
exercises/LambdaFromScratch.Kata/Numerals/
```

Expose:

```text
Successor
```

with the same conceptual API as the reference implementation.

The kata project must compile.

Do not include the complete C# solution in comments.

A comment containing only the Lambda definition is enough:

```text
SUCC = λn.λf.λx.f (n f x)
```

The learner must derive the C# translation.

---

## 12. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Numerals/
```

Test:

```text
SUCC ZERO → ONE
SUCC ONE  → TWO
SUCC TWO  → THREE
```

Also include at least one non-numeric behavioral test.

For example:

```text
Successor of Two applies a string transformation three times
```

The kata tests should fail until the learner completes the exercise.

Do not skip or disable them.

---

## 13. Documentation

Create:

```text
docs/06-successor.md
```

Start from:

```text
ZERO  = apply f zero times
ONE   = apply f once
TWO   = apply f twice
THREE = apply f three times
```

Then ask:

> So far we wrote each numeral manually.  
> Can we construct the next numeral from an existing one?

Build the intuition using `TWO` before showing the Lambda formula.

---

## 14. Use the three views

### Lambda Calculus

```text
SUCC = λn.λf.λx.f (n f x)
```

### C#

Use the actual project implementation, keeping it structurally similar to:

```csharp
n => f => x =>
    f(n(f)(x))
```

### Meaning

> Let `n` apply `f`, then apply `f` one more time.

Keep this pattern consistent with the previous chapters.

---

## 15. The key conceptual shift

Compare these two interpretations.

Incorrect mental model:

```text
Successor = add the integer 1
```

Church interpretation:

```text
Successor = add one more function application
```

A useful way to express it is:

> `Successor` does not need to know what number `n` represents. It only needs to reuse its behavior and apply `f` once more.

That is the central insight of this milestone.

---

## 16. What you should understand

At the end of this milestone, you should be able to read:

```text
λn.λf.λx.f (n f x)
```

as:

```csharp
n => f => x => f(n(f)(x))
```

and understand it as:

```text
do what n already does
then apply f once more
```

You should also understand that `Successor` never needs to inspect or decode a Church numeral.

It composes behavior.

---

## 17. Prepare the next milestone

Now a new question appears:

> If Successor adds one application, could we apply Successor repeatedly to add two Church numerals?

That prepares the next milestone.

Do not implement or explain `ADD` yet.

---

## Constraints

For this milestone, do not introduce:

- Peano arithmetic in depth;
- induction;
- formal beta-reduction notation;
- recursion;
- fixed-point combinators.

Do not implement:

```text
ADD
MULTIPLY
PAIR
FIRST
SECOND
ISZERO
PREDECESSOR
SUBTRACT
LEQ
EQ
Y combinator
FACTORIAL
```

Also:

- do not add external dependencies;
- do not refactor the existing Church numeral representation unless strictly necessary;
- do not introduce integer-backed representations;
- do not use loops or counters in `Successor`.

---

## Completion checklist

Before moving to Milestone 7, verify that:

- [ ] `Successor` exists in the reference implementation;
- [ ] `Successor(Zero)` behaves like `One`;
- [ ] `Successor(One)` behaves like `Two`;
- [ ] `Successor(Two)` behaves like `Three`;
- [ ] `Successor` works with a non-numeric transformation;
- [ ] `Successor` contains no `int`;
- [ ] `Successor` contains no numeric addition;
- [ ] `Successor` contains no loops or counters;
- [ ] `Successor` does not use `ToInt`;
- [ ] reference tests pass;
- [ ] the kata project compiles;
- [ ] the kata tests fail until the learner implements `Successor`;
- [ ] `docs/06-successor.md` exists;
- [ ] addition and later milestones have not been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently.

The new `Successor` kata tests should fail until the learner completes the exercise.

If the full solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 7 yet.**
