# Milestone 8 — Multiplication

## Goal

Implement multiplication for Church numerals.

Use:

```text
MULT = λm.λn.λf.m (n f)
```

An equivalent fully expanded form is:

```text
MULT = λm.λn.λf.λx.m (n f) x
```

The central idea is:

> Multiplication means repeating a repetition.

If:

```text
n f
```

means:

> Build a transformation that applies `f` `n` times.

then:

```text
m (n f)
```

means:

> Repeat that whole transformation `m` times.

No primitive multiplication is involved.

Do not implement pairs, predicates, predecessor, subtraction, recursion, or any later milestone yet.

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
Principles/06-successor.md
Principles/07-addition.md
```

The project already uses:

```csharp
public delegate Func<T, T> ChurchNumeral<T>(Func<T, T> f);
```

Preserve this representation.

Do not introduce another numeral abstraction.

---

## 1. Start from what a numeral means

Recall:

```text
TWO
```

means:

> Apply a transformation twice.

And:

```text
THREE
```

means:

> Apply a transformation three times.

Now ask:

> What should TWO multiplied by THREE mean if numbers are repetitions?

Do not start from the Lambda formula.

Start from the behavior.

---

## 2. Build TWO × THREE visually

Start with:

```text
THREE f
```

This is itself a transformation:

```text
x → f(f(f(x)))
```

Now let `TWO` repeat that whole transformation.

Start from:

```text
x
```

Apply `THREE f` once:

```text
x
↓ THREE f
f(f(f(x)))
```

Apply `THREE f` again:

```text
f(f(f(x)))
↓ THREE f
f(f(f(f(f(f(x))))))
```

The final result applies `f` six times.

So:

> Multiplication is repetition of repetition.

That is the intuition behind Church multiplication.

---

## 3. Reveal the formula

Now introduce:

```text
MULT = λm.λn.λf.m (n f)
```

Do not treat it as something to memorize.

Read it progressively.

First:

```text
n f
```

means:

> Turn `f` into a transformation that applies `f` `n` times.

Then:

```text
m (n f)
```

means:

> Repeat that whole transformation `m` times.

That is the entire mechanism.

---

## 4. Read the expression structurally

Format it as:

```text
λm.
    λn.
        λf.
            m (n f)
```

Read it as:

> Take Church numeral `m`.  
> Take Church numeral `n`.  
> Take transformation `f`.  
> Let `n` transform `f` into repeated application of `f`.  
> Let `m` repeat that entire transformation.

This is the key conceptual difference from addition.

---

## 5. Contrast multiplication with addition

Recall addition:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

Addition means:

> Apply `f` `n` times, then apply `f` `m` more times.

That is:

```text
n repetitions
followed by
m repetitions
```

Multiplication uses:

```text
MULT = λm.λn.λf.m (n f)
```

and means:

> Build a transformation that already repeats `f` `n` times, then repeat that transformation `m` times.

So keep these two ideas separate:

```text
Addition        → continue repetition
Multiplication  → repeat a repetition
```

---

## 6. Connect the syntax carefully

Start from:

```text
n f
```

which corresponds to:

```csharp
n(f)
```

The result of `n(f)` is itself a transformation.

Now:

```text
m (n f)
```

corresponds to:

```csharp
m(n(f))
```

Therefore:

```text
λm.λn.λf.m (n f)
```

maps naturally to:

```csharp
m => n => f => m(n(f))
```

Keep this mapping visually explicit.

---

## 7. Reference implementation

Work under:

```text
src/LambdaFromScratch/Numerals/
```

Expose:

```text
Multiply
```

Keep currying explicit.

The implementation should remain structurally close to:

```text
MULT = λm.λn.λf.m (n f)
```

and therefore to:

```csharp
m => n => f =>
    m(n(f))
```

Adapt the exact method and delegate syntax to the existing project conventions.

Conceptual usage should look like:

```csharp
Multiply(Two)(Three)
```

and return another Church numeral.

Do not replace the curried API with:

```csharp
Multiply(Two, Three)
```

The Lambda structure should remain visible.

---

## 8. Lambda Core restrictions

Inside `Multiply`, do not use:

```csharp
int
*
+
++
for
foreach
while
do
Enumerable
ToInt
Add
Successor
```

Also:

- do not convert Church numerals to integers;
- do not use loops;
- do not use counters;
- do not use primitive multiplication;
- do not use primitive addition;
- do not implement multiplication as repeated `Add`;
- do not implement multiplication through `Successor`.

This milestone is specifically about understanding the direct Church encoding:

```text
MULT = λm.λn.λf.m (n f)
```

---

## 9. Walk through MULT TWO THREE

Start from:

```text
MULT TWO THREE
```

The body of `MULT` is:

```text
m (n f)
```

Substitute:

```text
m = TWO
n = THREE
```

giving:

```text
TWO (THREE f)
```

Now recall:

```text
THREE f
```

is a transformation that applies `f` three times.

Then `TWO` applies that three-step transformation twice.

So the final behavior is:

```text
six applications of f
```

You do not need a predefined `SIX` constant.

`Multiply(Two)(Three)` already behaves as that Church numeral.

---

## 10. Use a non-numeric example

Reuse:

```csharp
Func<string, string> addStar = value => value + "*";
```

Think first about:

```text
THREE addStar
```

This is a transformation that adds three stars.

Now apply multiplication:

```text
MULT TWO THREE addStar ""
```

The three-star transformation is applied twice.

The result is:

```text
"******"
```

The key point is:

> `Multiply` never needs to know that `TWO` means `2`, that `THREE` means `3`, or that the result means `6`.

It only composes repeated transformations.

---

## 11. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Numerals/
```

Test at least:

```text
ZERO × THREE  → ZERO
ONE × THREE   → THREE
TWO × THREE   → SIX
THREE × TWO   → SIX
THREE × THREE → NINE
```

You may use the existing `ToInt` observation adapter where useful.

For example:

```csharp
var result = Multiply(Two)(Three);

Assert.Equal(6, ToInt(result));
```

But do not test multiplication only through integers.

---

## 12. Behavioral reference test

Add at least one test using a non-numeric transformation.

For example:

```csharp
Func<string, string> addStar = value => value + "*";

var six = Multiply(Two)(Three);
var result = six(addStar)("");

Assert.Equal("******", result);
```

This test is important because it demonstrates that multiplication composes **repetition**, rather than calculating a stored numeric product.

Keep the test explicit.

Do not introduce test helper frameworks.

---

## 13. Make repeated repetition visible

A useful mental model is:

```text
THREE f
```

which behaves like:

```text
f ∘ f ∘ f
```

Now:

```text
TWO (THREE f)
```

means:

```text
apply the three-step transformation
twice
```

So behaviorally:

```text
3 applications
then
3 applications
```

becomes:

```text
6 applications
```

The arithmetic is only a convenient observation.

The deeper idea is composition of behavior.

---

## 14. Observation boundary

It is fine to observe:

```text
ToInt(Multiply(Two)(Three)) → 6
```

But:

```text
6
```

exists only at the observation boundary.

Inside `Multiply`, there are no numeric values.

The dependency remains:

```text
Multiply
    ↓
ChurchNumeral
    ↓
Observation
    ↓
int
```

Never the reverse.

---

## 15. Do not implement Multiply through Add

The previous milestone introduced:

```text
Add
```

It may be tempting to describe multiplication as repeated addition.

Do not implement it that way here.

This milestone exists specifically to understand:

```text
MULT = λm.λn.λf.m (n f)
```

directly.

The important idea is not:

```text
add several times
```

but:

```text
repeat a repeated transformation
```

---

## 16. Kata implementation

Create the corresponding incomplete exercise under:

```text
exercises/LambdaFromScratch.Kata/Numerals/
```

Expose:

```text
Multiply
```

with the same conceptual API as the reference implementation.

The kata project must compile.

Do not include the completed C# implementation in comments.

A comment containing only the Lambda definition is enough:

```text
MULT = λm.λn.λf.m (n f)
```

The learner should derive the C# translation.

---

## 17. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Numerals/
```

At minimum test:

```text
ONE × THREE
TWO × THREE
THREE × TWO
```

Also include at least one non-numeric transformation test.

For example:

```text
TWO × THREE applies a string transformation six times
```

The kata tests should fail until the learner implements `Multiply`.

Do not skip or disable them.

---

## 18. Documentation

Create:

```text
docs/08-multiplication.md
```

Start from:

```text
TWO   → apply a transformation twice
THREE → apply a transformation three times
```

Then ask:

> What should TWO multiplied by THREE mean if numbers are repetitions?

Build the intuition visually with:

```text
THREE f
```

followed by:

```text
TWO (THREE f)
```

Only after the behavior is clear should you show:

```text
MULT = λm.λn.λf.m (n f)
```

---

## 19. Use the three views

### Lambda Calculus

```text
MULT = λm.λn.λf.m (n f)
```

### C#

Use the actual project implementation, keeping it structurally similar to:

```csharp
m => n => f =>
    m(n(f))
```

### Meaning

> Build a transformation that repeats `f` `n` times, then repeat that transformation `m` times.

Keep this presentation consistent with the previous chapters.

---

## 20. The key conceptual shift

Avoid making this the primary mental model:

```text
2 × 3 = 6
```

For Church numerals, think instead:

```text
THREE turns f into:
apply f three times

TWO then says:
repeat that whole transformation twice
```

So:

```text
multiplication = repetition of repetition
```

That is the central insight of this milestone.

---

## 21. What you should understand

At the end of this milestone, you should be able to read:

```text
λm.λn.λf.m (n f)
```

and mentally translate it to:

```csharp
m => n => f => m(n(f))
```

You should also understand the difference:

```text
ADD  → chain repetitions
MULT → repeat a repetition
```

No primitive arithmetic is required.

The result emerges from function composition.

---

## 22. Prepare the next milestone

So far, functions have represented:

```text
booleans
numbers
logic
arithmetic
```

Now ask:

> Can functions represent data structures too?

That is the direction of the next milestone.

Do not introduce `PAIR`, `FIRST`, or `SECOND` yet.

---

## Constraints

For this milestone, do not introduce:

- formal function composition notation in depth;
- category theory;
- predecessor;
- subtraction;
- recursion;
- fixed-point combinators;
- pairs implementation.

Do not implement:

```text
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
- do not refactor the Church numeral representation unless strictly necessary;
- do not use primitive multiplication inside `Multiply`;
- do not use primitive addition inside `Multiply`;
- do not use `Add`;
- do not use `Successor`.

---

## Completion checklist

Before moving to Milestone 9, verify that:

- [ ] `Multiply` exists in the reference implementation;
- [ ] `Multiply` preserves currying;
- [ ] `Multiply(Zero)(Three)` behaves like `Zero`;
- [ ] `Multiply(One)(Three)` behaves like `Three`;
- [ ] `Multiply(Two)(Three)` applies a transformation six times;
- [ ] `Multiply(Three)(Two)` applies a transformation six times;
- [ ] `Multiply(Three)(Three)` applies a transformation nine times;
- [ ] at least one reference test uses a non-numeric transformation;
- [ ] `Multiply` contains no `int`;
- [ ] `Multiply` contains no primitive multiplication;
- [ ] `Multiply` contains no primitive addition;
- [ ] `Multiply` contains no loops or counters;
- [ ] `Multiply` does not use `ToInt`;
- [ ] `Multiply` does not use `Add`;
- [ ] `Multiply` does not use `Successor`;
- [ ] reference tests pass;
- [ ] the kata project compiles;
- [ ] kata tests fail until the learner implements `Multiply`;
- [ ] `docs/08-multiplication.md` exists;
- [ ] pairs and later milestones have not been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently.

The new Multiplication kata tests should fail until the learner completes the exercise.

If the full solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 9 yet.**
