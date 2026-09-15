# Milestone 5 — Church Numerals

## Goal

Introduce Church numerals as repeated function application.

You will implement:

```text
ZERO  = λf.λx.x
ONE   = λf.λx.f x
TWO   = λf.λx.f (f x)
THREE = λf.λx.f (f (f x))
```

The central idea of this milestone is:

> A Church numeral is not a stored number.  
> It describes how many times a function should be applied.

Conceptually:

```text
ZERO  → apply f zero times
ONE   → apply f once
TWO   → apply f twice
THREE → apply f three times
```

Do not implement `Successor`, `Add`, `Multiply`, pairs, predicates, or any later concept yet.

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
```

You should already be comfortable reading expressions such as:

```text
λf.λx.f x
```

as a curried function:

```csharp
f => x => f(x)
```

---

## 1. Start from a familiar number

In ordinary C#, we usually represent a number as data:

```csharp
var number = 3;
```

Now ask a different question:

> What if we remove numbers too?

Instead of storing the value `3`, imagine representing it as behavior:

> Apply this transformation three times.

That is the idea behind Church numerals.

Do not start by thinking about integer storage.

Think about **repetition**.

---

## 2. Introduce a ChurchNumeral delegate

Create the smallest useful named delegate under:

```text
src/LambdaFromScratch/Numerals/
```

The conceptual type of a Church numeral is:

```text
(T → T) → T → T
```

A suitable C# representation is:

```csharp
public delegate Func<T, T> ChurchNumeral<T>(Func<T, T> f);
```

Use the delegate only if it keeps the code readable.

Do not create:

- `ChurchNumber` classes;
- value objects containing integers;
- interfaces;
- arithmetic abstractions;
- numeric wrappers.

A Church numeral must fundamentally remain a function.

---

## 3. ZERO

Start from:

```text
ZERO = λf.λx.x
```

Read it slowly:

> Take a function `f`.  
> Take a starting value `x`.  
> Do not apply `f`.  
> Return `x`.

Its C# shape is:

```csharp
f => x => x
```

Notice that `f` exists, but is never called.

That is what makes this `ZERO`.

---

## 4. ONE

Now consider:

```text
ONE = λf.λx.f x
```

Read it as:

> Take `f`.  
> Take `x`.  
> Apply `f` once to `x`.

Its C# shape is:

```csharp
f => x => f(x)
```

The only difference from `ZERO` is that `f` is now applied once.

---

## 5. TWO

Now:

```text
TWO = λf.λx.f (f x)
```

Translate the structure directly:

```csharp
f => x => f(f(x))
```

Read the execution visually:

```text
x
↓ f
f(x)
↓ f
f(f(x))
```

The function is applied twice.

Do not use:

- a loop;
- a counter;
- an integer field.

The repetition must be visible in the function structure itself.

---

## 6. THREE

Finally:

```text
THREE = λf.λx.f (f (f x))
```

The C# shape is:

```csharp
f => x => f(f(f(x)))
```

Again, read it as repeated transformation:

```text
x
↓ f
f(x)
↓ f
f(f(x))
↓ f
f(f(f(x)))
```

The function is applied three times.

No loop is needed.

No counter is needed.

No numeric value is stored.

---

## 7. Reference implementation

Work under:

```text
src/LambdaFromScratch/Numerals/
```

Expose:

```text
Zero
One
Two
Three
```

Keep currying explicit.

A typical use should look like:

```csharp
Func<int, int> increment = x => x + 1;

Two(increment)(0)
```

and produce:

```text
2
```

The use of `int` here is allowed.

The integer is being used to **observe the behavior** of the Church numeral.

The Church numeral itself must not contain or depend on the integer values:

```text
0
1
2
3
```

---

## 8. Observe repetition with something that is not a number

It is important not to confuse Church numerals with integers.

Try a string transformation:

```csharp
Func<string, string> addStar = value => value + "*";
```

Then observe:

```text
Zero(addStar)("")  → ""
One(addStar)("")   → "*"
Two(addStar)("")   → "**"
Three(addStar)("") → "***"
```

This reveals the real meaning of the encoding.

`TWO` is not fundamentally the integer `2`.

It means:

> Apply the supplied transformation twice.

Try another transformation:

```csharp
Func<int, int> doubleValue = x => x * 2;
```

Starting from:

```text
1
```

you get:

```text
Zero  → 1
One   → 2
Two   → 4
Three → 8
```

The arithmetic belongs to the function supplied from outside.

The Church numeral only controls how many times that function is applied.

---

## 9. Observation boundary

Add a minimal observation adapter under:

```text
src/LambdaFromScratch/Observation/
```

Expose a function conceptually equivalent to:

```csharp
ToInt(...)
```

Its job is to observe a Church numeral as a normal C# integer.

Conceptually, `ToInt` can supply:

```csharp
x => x + 1
```

and start from:

```csharp
0
```

so that:

```text
ToInt(Zero)  → 0
ToInt(One)   → 1
ToInt(Two)   → 2
ToInt(Three) → 3
```

The observation adapter is outside the Lambda Core.

It may therefore use:

```csharp
int
+
0
1
```

Do not use `ToInt` to implement a Church numeral.

The dependency direction must remain:

```text
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
Church numeral implementation
```

---

## 10. Reference tests

Create focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Numerals/
```

At minimum verify:

```text
Zero applies a function zero times
One applies a function once
Two applies a function twice
Three applies a function three times
```

Also verify the observation boundary:

```text
ToInt(Zero)  == 0
ToInt(One)   == 1
ToInt(Two)   == 2
ToInt(Three) == 3
```

Do not make all tests depend on `ToInt`.

At least some tests should demonstrate the more important concept: repeated function application.

For example:

```text
Two applies a string transformation twice
```

Simple repetition in the test suite is acceptable.

These tests are teaching material.

Avoid creating parameterized-test infrastructure if it makes the examples less obvious.

---

## 11. Kata implementation

Create the corresponding incomplete exercises under:

```text
exercises/LambdaFromScratch.Kata/Numerals/
```

Include:

```text
Zero
One
Two
Three
```

The kata project must compile.

Do not include the complete C# solution in comments.

Comments may contain only the Lambda definitions:

```text
ZERO = λf.λx.x
ONE  = λf.λx.f x
TWO  = λf.λx.f (f x)
THREE = λf.λx.f (f (f x))
```

The learner should derive the C# translation.

---

## 12. Kata tests

Create corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Numerals/
```

The tests should guide the learner toward repeated function application.

Do not make all tests depend only on `ToInt`.

At least one test must use a non-numeric transformation.

For example:

```text
Two applies a string transformation twice
```

This reinforces that a Church numeral is not an integer wrapper.

The kata tests should fail until the learner completes the exercises.

Do not disable or skip them.

---

## 13. Documentation

Create:

```text
docs/05-church-numerals.md
```

Begin from ordinary C#:

```csharp
var number = 3;
```

Then ask:

> What if we remove numbers too?

Do not immediately present the Lambda formulas.

First establish the behavioral interpretation:

> A numeral can describe how many times a transformation should be applied.

Then build `ZERO`, `ONE`, `TWO`, and `THREE` progressively.

---

## 14. Build the numerals progressively

### ZERO

#### Lambda Calculus

```text
ZERO = λf.λx.x
```

#### C#

```csharp
f => x => x
```

#### Meaning

> Apply `f` zero times and return `x`.

---

### ONE

#### Lambda Calculus

```text
ONE = λf.λx.f x
```

#### C#

```csharp
f => x => f(x)
```

#### Meaning

> Apply `f` once to `x`.

---

### TWO

#### Lambda Calculus

```text
TWO = λf.λx.f (f x)
```

#### C#

```csharp
f => x => f(f(x))
```

#### Meaning

> Apply `f` twice to `x`.

---

### THREE

#### Lambda Calculus

```text
THREE = λf.λx.f (f (f x))
```

#### C#

```csharp
f => x => f(f(f(x)))
```

#### Meaning

> Apply `f` three times to `x`.

---

## 15. Connect the idea to C#

Use an increment function:

```csharp
Func<int, int> increment = x => x + 1;
```

Then:

```text
ZERO  increment 0 → 0
ONE   increment 0 → 1
TWO   increment 0 → 2
THREE increment 0 → 3
```

Immediately follow this with a non-numeric example:

```csharp
Func<string, string> addStar = x => x + "*";
```

Then:

```text
ZERO  addStar "" → ""
ONE   addStar "" → "*"
TWO   addStar "" → "**"
THREE addStar "" → "***"
```

The second example is crucial.

> This is why a Church numeral should not be thought of primarily as an integer.

It represents repeated application.

---

## 16. The key conceptual shift

Compare the two representations.

Traditional representation:

```text
2 = a stored numeric value
```

Church representation:

```text
TWO = apply a transformation twice
```

A useful way to state the idea is:

> TWO does not know what the number 2 is. It only knows how to repeat a function twice.

That is the central insight of this milestone.

---

## 17. What you should understand

At the end of this milestone, you should be able to look at:

```text
λf.λx.f (f x)
```

and mentally recognize:

```csharp
f => x => f(f(x))
```

and understand:

```text
Apply f twice to x.
```

You should also understand the separation between:

```text
Church numeral
```

and:

```text
ordinary C# integer used to observe it
```

The numeral itself contains no integer representation.

It is behavior.

---

## 18. Prepare the next milestone

Now a natural question appears:

> If numbers are repeated function application, how do we create the next number without writing it manually?

That is the subject of the next milestone.

Do not answer it yet.

Do not implement `Successor`.

---

## Constraints

For this milestone, do not introduce:

- formal Church encoding theory;
- Peano arithmetic;
- induction;
- beta-reduction notation;
- recursion;
- fixed-point combinators.

Do not implement:

```text
SUCCESSOR
ADD
MULTIPLY
PAIR
FIRST
SECOND
ISZERO
PREDECESSOR
SUBTRACT
Y combinator
FACTORIAL
```

Also:

- do not add external dependencies;
- do not implement loops in Church numerals;
- do not use counters;
- do not store integer values inside Church numerals.

Keep the milestone focused on understanding what a Church numeral **is**.

---

## Completion checklist

Before moving to Milestone 6, verify that:

- [ ] `ChurchNumeral<T>` exists if useful for readability;
- [ ] `Zero` applies the supplied function zero times;
- [ ] `One` applies it once;
- [ ] `Two` applies it twice;
- [ ] `Three` applies it three times;
- [ ] Church numerals contain no stored integer values;
- [ ] Church numerals use no loops or counters;
- [ ] `ToInt` exists as an observation adapter;
- [ ] `ToInt(Zero)` returns `0`;
- [ ] `ToInt(One)` returns `1`;
- [ ] `ToInt(Two)` returns `2`;
- [ ] `ToInt(Three)` returns `3`;
- [ ] reference tests demonstrate repetition directly;
- [ ] at least one test uses a non-numeric transformation;
- [ ] reference tests pass;
- [ ] the kata project compiles;
- [ ] the kata tests fail until the learner completes the exercise;
- [ ] `docs/05-church-numerals.md` exists;
- [ ] `Successor`, arithmetic, and later concepts have not been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently.

The new Church numeral kata tests should fail until the learner completes the exercises.

If the complete solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 6 yet.**
