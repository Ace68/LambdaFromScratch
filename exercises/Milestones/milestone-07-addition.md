# Milestone 7 — Addition

## Goal

Implement addition for Church numerals.

Use:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

The central idea is:

> Adding two Church numerals means composing their repetitions.

If `n` applies `f` `n` times and `m` applies `f` `m` times, then:

```text
ADD m n
```

means:

```text
apply f n times
then
apply f m more times
```

No primitive numeric addition is involved.

Do not implement multiplication, pairs, predicates, predecessor, subtraction, recursion, or any later milestone yet.

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

> Apply `f` twice.

And:

```text
THREE
```

means:

> Apply `f` three times.

Now ask:

> What should TWO plus THREE mean if numbers are only repeated function application?

Do not start from the Lambda formula.

Start from the behavior.

---

## 2. Build TWO + THREE visually

Take a starting value:

```text
x
```

First let `THREE` work:

```text
x
↓ f
f(x)
↓ f
f(f(x))
↓ f
f(f(f(x)))
```

Now let `TWO` continue from that result:

```text
f(f(f(x)))
↓ f
f(f(f(f(x))))
↓ f
f(f(f(f(f(x)))))
```

The final transformation applies `f` five times.

So:

> Addition is one numeral continuing where the other one stopped.

That is the intuition behind Church addition.

---

## 3. Reveal the formula

Now introduce:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

Do not treat it as something to memorize.

Read it progressively.

First:

```text
n f x
```

means:

> Let `n` apply `f` to `x`.

Then:

```text
m f (...)
```

means:

> Starting from that result, let `m` apply the same `f` again.

Together:

```text
m f (n f x)
```

means:

> Apply `f` `n` times, then `m` more times.

---

## 4. Read the expression structurally

Format it as:

```text
λm.
    λn.
        λf.
            λx.
                m f (n f x)
```

Read it as:

> Take a Church numeral `m`.  
> Take another Church numeral `n`.  
> Take a transformation `f`.  
> Take a starting value `x`.  
> Let `n` apply `f` to `x`.  
> Then let `m` continue applying the same `f` to that result.

The C# implementation should preserve this structure.

---

## 5. Revisit function application

This milestone is a good opportunity to reinforce an earlier rule:

> Function application associates to the left.

So:

```text
n f x
```

means:

```text
(n f) x
```

and corresponds to:

```csharp
n(f)(x)
```

Therefore:

```text
m f (n f x)
```

corresponds to:

```csharp
m(f)(n(f)(x))
```

This grouping is important when translating the expression into C#.

---

## 6. Reference implementation

Work under:

```text
src/LambdaFromScratch/Numerals/
```

Expose:

```text
Add
```

Keep currying explicit.

The implementation should remain structurally close to:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

and therefore to:

```csharp
m => n => f => x =>
    m(f)(n(f)(x))
```

Adapt the exact method and delegate syntax to the existing project conventions.

A conceptual use should look like:

```csharp
Add(Two)(Three)
```

and return another `ChurchNumeral<T>`.

Do not replace the curried API with:

```csharp
Add(Two, Three)
```

The Lambda structure should remain visible.

---

## 7. Lambda Core restrictions

Inside `Add`, do not use:

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
Successor
```

Also:

- do not convert Church numerals to integers;
- do not use loops;
- do not use counters;
- do not perform primitive arithmetic;
- do not repeatedly call `Successor`.

Although addition could be described in terms of repeated successor, this milestone is specifically about understanding the direct Church encoding:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

---

## 8. Walk through ADD TWO THREE

Start from:

```text
ADD TWO THREE
```

The body of `ADD` is:

```text
m f (n f x)
```

Substitute:

```text
m = TWO
n = THREE
```

giving:

```text
TWO f (THREE f x)
```

Now recall:

```text
THREE f x
```

applies `f` three times.

So the inner result is:

```text
f(f(f(x)))
```

Then `TWO` applies `f` twice more:

```text
f(f(f(f(f(x)))))
```

The result behaves like a Church numeral representing five applications.

You do not need a predefined `FIVE` constant.

`Add(Two)(Three)` already is that numeral.

---

## 9. Use a non-numeric example

Reuse:

```csharp
Func<string, string> addStar = value => value + "*";
```

You already know:

```text
TWO addStar ""   → "**"
THREE addStar "" → "***"
```

Now:

```text
ADD TWO THREE addStar ""
```

produces:

```text
"*****"
```

This is the key point:

> At no point did `Add` need to know that `TWO` means `2`, that `THREE` means `3`, or that the result means `5`.

It only composed repetitions.

---

## 10. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Numerals/
```

Test at least:

```text
ZERO + ZERO  → ZERO
ZERO + TWO   → TWO
TWO + ZERO   → TWO
ONE + TWO    → THREE
TWO + THREE  → FIVE
THREE + TWO  → FIVE
```

You may use the existing observation adapter where useful.

For example:

```csharp
var result = Add(Two)(Three);

Assert.Equal(5, ToInt(result));
```

But do not test addition only through integers.

---

## 11. Behavioral reference test

Add at least one test using a non-numeric transformation.

For example:

```csharp
Func<string, string> addStar = value => value + "*";

var five = Add(Two)(Three);
var result = five(addStar)("");

Assert.Equal("*****", result);
```

This test is important because it demonstrates that addition combines **function applications**, not stored numbers.

Keep the test explicit.

Do not introduce test helper frameworks.

---

## 12. Observation boundary

It is fine to observe:

```text
ToInt(Add(Two)(Three)) → 5
```

But:

```text
5
```

exists only at the observation boundary.

Inside `Add`, there are no numeric values.

The dependency remains:

```text
Add
 ↓
ChurchNumeral
 ↓
Observation
 ↓
int
```

Never the reverse.

---

## 13. Do not implement Add through Successor

The previous milestone introduced:

```text
SUCC
```

It may be tempting to define addition by repeatedly applying `Successor`.

Do not do that here.

This milestone exists specifically to understand:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

directly.

The interesting idea is not “increment many times”.

It is:

> compose two repetitions of the same transformation.

---

## 14. Kata implementation

Create the corresponding incomplete exercise under:

```text
exercises/LambdaFromScratch.Kata/Numerals/
```

Expose:

```text
Add
```

with the same conceptual API as the reference implementation.

The kata project must compile.

Do not include the completed C# implementation in comments.

A comment containing only the Lambda definition is enough:

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

The learner must derive the C# translation.

---

## 15. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Numerals/
```

At minimum test:

```text
ZERO + TWO
ONE + TWO
TWO + THREE
```

Also include at least one non-numeric transformation test.

For example:

```text
TWO + THREE applies a string transformation five times
```

The kata tests should fail until the learner implements `Add`.

Do not skip or disable them.

---

## 16. Documentation

Create:

```text
docs/07-addition.md
```

Start from the behavioral meaning of numerals:

```text
TWO   → apply f twice
THREE → apply f three times
```

Then ask:

> What should TWO plus THREE mean if numbers are only repeated function application?

Build `TWO + THREE` visually before showing the formula.

The learner should understand the behavior first and only then see the Lambda expression that encodes it.

---

## 17. Use the three views

### Lambda Calculus

```text
ADD = λm.λn.λf.λx.m f (n f x)
```

### C#

Use the actual project implementation, keeping it structurally similar to:

```csharp
m => n => f => x =>
    m(f)(n(f)(x))
```

### Meaning

> Let `n` apply `f`, then let `m` apply the same function to the result.

Keep this presentation consistent with previous chapters.

---

## 18. The key conceptual shift

Avoid this mental model:

```text
2 + 3 = 5
```

as the primary explanation.

For Church numerals, think instead:

```text
apply f three times
then
apply f two more times
```

The result is:

```text
five applications of f
```

A useful way to express the idea is:

> Addition does not combine stored numeric values. It chains repeated behavior.

That is the central insight of this milestone.

---

## 19. What you should understand

At the end of this milestone, you should be able to read:

```text
λm.λn.λf.λx.m f (n f x)
```

and mentally translate it to:

```csharp
m => n => f => x =>
    m(f)(n(f)(x))
```

You should also understand the execution as:

```text
n applies f first
m continues from that result
```

Addition emerges from composition alone.

No primitive arithmetic is required.

---

## 20. Prepare multiplication

Now ask:

> If addition combines repeated applications, what would multiplication look like?

A useful intuition for the next milestone is:

> repeating a repetition

Do not show or implement `MULTIPLY` yet.

---

## Constraints

For this milestone, do not introduce:

- formal beta reduction;
- Peano arithmetic;
- predecessor;
- subtraction;
- recursion;
- fixed-point combinators;
- multiplication implementation.

Do not implement:

```text
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
- do not refactor the Church numeral representation unless strictly necessary;
- do not use primitive arithmetic inside `Add`;
- do not use `Successor` to implement `Add`.

---

## Completion checklist

Before moving to Milestone 8, verify that:

- [ ] `Add` exists in the reference implementation;
- [ ] `Add` preserves currying;
- [ ] `Add(Zero)(Zero)` behaves like `Zero`;
- [ ] `Add(Zero)(Two)` behaves like `Two`;
- [ ] `Add(Two)(Zero)` behaves like `Two`;
- [ ] `Add(One)(Two)` behaves like `Three`;
- [ ] `Add(Two)(Three)` applies a transformation five times;
- [ ] `Add(Three)(Two)` applies a transformation five times;
- [ ] at least one reference test uses a non-numeric transformation;
- [ ] `Add` contains no `int`;
- [ ] `Add` contains no primitive numeric addition;
- [ ] `Add` contains no loops or counters;
- [ ] `Add` does not use `ToInt`;
- [ ] `Add` does not use `Successor`;
- [ ] reference tests pass;
- [ ] the kata project compiles;
- [ ] kata tests fail until the learner implements `Add`;
- [ ] `docs/07-addition.md` exists;
- [ ] multiplication and later milestones have not been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently.

The new Addition kata tests should fail until the learner completes the exercise.

If the full solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 8 yet.**
