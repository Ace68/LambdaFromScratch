# Milestone 4 — Logical Operators

## Goal

Build logical behavior entirely from Church booleans and function application.

You will implement:

```text
NOT
AND
OR
IF
```

without using ordinary C# boolean logic inside the Lambda Core.

The central idea of this milestone is:

> If booleans are functions that choose between alternatives, logical operators can themselves be built by composing those choices.

You already have:

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

and conceptually:

```csharp
x => y => x
x => y => y
```

Now you will use those functions as values.

Do not implement Church numerals or any later milestone yet.

---

## Before you start

Read:

```text
Milestones/milestone-00-before-to-start.md
Principles/01-functions.md
Principles/02-identity.md
Principles/01-functions.md
Principles/03-booleans.md
```

The existing Church boolean implementation is:

```csharp
public static class ChurchBooleans
{
    public static Func<T, T> True<T>(T x) => y => x;

    public static Func<T, T> False<T>(T x) => y => y;
}
```

Preserve the conceptual simplicity of this implementation.

You should already understand:

```text
TRUE  → choose the first alternative
FALSE → choose the second alternative
```

---

## 1. Introduce a ChurchBoolean delegate

Logical operators need to receive Church booleans as values.

Introduce the smallest useful named delegate in the Booleans area:

```csharp
public delegate Func<T, T> ChurchBoolean<T>(T x);
```

Its purpose is readability.

Do not introduce:

- interfaces;
- wrapper classes;
- inheritance;
- a generic boolean framework.

The existing methods:

```csharp
True<T>(T x)
False<T>(T x)
```

may remain unchanged.

A method group should be usable as a Church boolean value:

```csharp
ChurchBoolean<string> trueValue = ChurchBooleans.True;
```

Keep the representation function-based.

---

## 2. NOT

Start from:

```text
NOT = λp.λx.λy.p y x
```

Read it slowly:

> Take boolean `p`.  
> Take `x`.  
> Take `y`.  
> Ask `p` to choose between `y` and `x`.

A Church boolean normally chooses between:

```text
x
y
```

`NOT` reverses those alternatives:

```text
y
x
```

That is enough to reverse the meaning.

The C# shape should remain recognisable as:

```csharp
p => x => y => p(y)(x)
```

Expected behavior:

```text
NOT TRUE  → FALSE
NOT FALSE → TRUE
```

Do not implement `NOT` by converting a Church boolean to a C# `bool`.

Do not compare values.

Do not use C# logical negation.

---

## 3. See NOT visually

Start with:

```text
TRUE a b
   ↓
   a
```

Now reverse the alternatives:

```text
NOT TRUE a b
     ↓
TRUE b a
     ↓
     b
```

The important idea is:

> NOT does not calculate a new boolean value. It reverses the alternatives.

This is the behavior encoded by:

```text
NOT = λp.λx.λy.p y x
```

and:

```csharp
p => x => y => p(y)(x)
```

---

## 4. IF

A Church boolean already behaves like a conditional.

Start from:

```text
IF = λp.λx.λy.p x y
```

Read it as:

> Take condition `p`.  
> Take the value for the true case.  
> Take the value for the false case.  
> Let `p` choose between them.

The corresponding C# shape is:

```csharp
p => x => y => p(x)(y)
```

Expected usage should preserve currying:

```csharp
If(condition)("yes")("no")
```

Expected behavior:

```text
IF TRUE  "yes" "no" → "yes"
IF FALSE "yes" "no" → "no"
```

Do not use:

```csharp
if
?:
```

The central insight is:

> `IF` does almost nothing because the Church boolean already is the decision.

---

## 5. Why IF almost disappears

Compare ordinary C#:

```csharp
if (condition)
{
    return whenTrue;
}

return whenFalse;
```

with Church encoding:

```text
condition whenTrue whenFalse
```

The condition does not need an external `if`.

The condition itself knows which alternative to select.

That is exactly what `TRUE` and `FALSE` already do.

---

## 6. AND

Reason about behavior first.

For:

```text
p AND q
```

there are two important cases.

If `p` is `FALSE`, the result must be `FALSE`, regardless of `q`.

If `p` is `TRUE`, the result depends on `q`.

Use:

```text
AND = λp.λq.λx.λy.p (q x y) y
```

Read it progressively:

> Take `p`.  
> Take `q`.  
> Take the true alternative `x`.  
> Take the false alternative `y`.  
> Let `q` choose between `x` and `y`.  
> Then let `p` choose between that result and `y`.

The C# shape should remain close to:

```csharp
p => q => x => y =>
    p(q(x)(y))(y)
```

Expected behavior:

```text
TRUE  AND TRUE  → TRUE
TRUE  AND FALSE → FALSE
FALSE AND TRUE  → FALSE
FALSE AND FALSE → FALSE
```

Do not use `&&`.

---

## 7. Walk through AND

Take:

```text
TRUE AND FALSE
```

Using:

```text
AND = λp.λq.λx.λy.p (q x y) y
```

replace:

```text
p = TRUE
q = FALSE
```

giving:

```text
TRUE (FALSE x y) y
```

`FALSE x y` chooses:

```text
y
```

so the expression becomes:

```text
TRUE y y
```

and `TRUE` chooses the first value:

```text
y
```

So:

```text
TRUE AND FALSE → FALSE
```

You do not need formal reduction notation yet.

Reason about which alternative each function chooses.

---

## 8. OR

Again, reason about behavior first.

For:

```text
p OR q
```

if `p` is `TRUE`, the result is already `TRUE`.

If `p` is `FALSE`, the result depends on `q`.

Use:

```text
OR = λp.λq.λx.λy.p x (q x y)
```

Read it as:

> Let `p` choose between the true alternative `x` and the result selected by `q`.

The C# shape should remain close to:

```csharp
p => q => x => y =>
    p(x)(q(x)(y))
```

Expected behavior:

```text
TRUE  OR TRUE  → TRUE
TRUE  OR FALSE → TRUE
FALSE OR TRUE  → TRUE
FALSE OR FALSE → FALSE
```

Do not use `||`.

---

## 9. Walk through OR

Take:

```text
FALSE OR TRUE
```

Using:

```text
OR = λp.λq.λx.λy.p x (q x y)
```

replace:

```text
p = FALSE
q = TRUE
```

giving:

```text
FALSE x (TRUE x y)
```

`TRUE x y` chooses:

```text
x
```

so the expression becomes:

```text
FALSE x x
```

`FALSE` chooses the second value:

```text
x
```

Therefore:

```text
FALSE OR TRUE → TRUE
```

Again, focus on behavior rather than formal reduction rules.

---

## 10. Reference implementation

Work under:

```text
src/LambdaFromScratch/Booleans/
```

Implement:

```text
Not
And
Or
If
```

Keep currying explicit.

Prefer APIs conceptually equivalent to:

```csharp
Not(p)(x)(y)

And(p)(q)(x)(y)

Or(p)(q)(x)(y)

If(p)(x)(y)
```

Do not replace them with multi-parameter APIs such as:

```csharp
Not(p, x, y)

And(p, q, x, y)
```

The Lambda Calculus structure should remain visible in the C# API.

Use `ChurchBoolean<T>` where it makes the functions easier to read.

Do not create operator overloads.

---

## 11. Lambda Core restrictions

Inside the implementation of the Church logic, do not use:

```csharp
bool
if
switch
?:
&&
||
!
```

Do not convert Church booleans to C# booleans in order to implement logic.

Do not implement logic through an observation adapter.

The implementation must remain entirely inside the functional encoding.

---

## 12. Reference tests

Create focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Booleans/
```

Keep them explicit.

The repetition is useful here because the tests are also teaching material.

### NOT

Test:

```text
NOT TRUE  → FALSE
NOT FALSE → TRUE
```

You may observe the result using strings:

```csharp
var result = Not(trueValue)("yes")("no");

Assert.Equal("no", result);
```

### AND

Test all four combinations:

```text
TRUE  AND TRUE
TRUE  AND FALSE
FALSE AND TRUE
FALSE AND FALSE
```

Observe the resulting Church boolean using:

```text
"yes"
"no"
```

Expected results:

```text
yes
no
no
no
```

### OR

Test all four combinations.

Expected results:

```text
yes
yes
yes
no
```

### IF

Test:

```text
IF TRUE  "yes" "no" → "yes"
IF FALSE "yes" "no" → "no"
```

Do not create a truth-table framework or shared testing abstraction.

Straightforward tests are preferable here.

---

## 13. Observation boundary

If a `ToBool` adapter already exists from the previous milestone, it may be used in tests when useful.

But never implement logic like this:

```text
Church boolean
      ↓
ToBool
      ↓
C# logical operator
      ↓
Church boolean
```

That would bypass the point of the exercise.

The logical operators must remain pure Church encoding.

---

## 14. Kata implementation

Add incomplete implementations under:

```text
exercises/LambdaFromScratch.Kata/Booleans/
```

for:

```text
Not
And
Or
If
```

The kata project must compile.

Do not include the complete C# solution in comments.

Comments may contain only the Lambda Calculus definitions, for example:

```text
NOT = λp.λx.λy.p y x
```

The learner should derive the C# implementation from the formula and the documentation.

---

## 15. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Booleans/
```

Cover the same behavior as the reference tests:

```text
NOT TRUE
NOT FALSE

TRUE  AND TRUE
TRUE  AND FALSE
FALSE AND TRUE
FALSE AND FALSE

TRUE  OR TRUE
TRUE  OR FALSE
FALSE OR TRUE
FALSE OR FALSE

IF TRUE
IF FALSE
```

The tests should fail until the learner completes the implementations.

Do not disable them.

Do not skip them.

Keep them intentionally simple.

---

## 16. Documentation

Create:

```text
docs/04-logic.md
```

Start from the previous milestone:

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

Remind the reader:

```text
TRUE  chooses the first alternative.
FALSE chooses the second alternative.
```

Then ask:

> If choosing between two alternatives is enough to represent true and false, can we build logical operations using nothing but those choices?

Use this progression:

```text
NOT
IF
AND
OR
```

Reason about behavior before presenting each formula.

Avoid starting from abstract symbolic manipulation.

---

## 17. Use the three views

For every operator, show the same three perspectives used throughout LambdaFromScratch.

### NOT

#### Lambda Calculus

```text
NOT = λp.λx.λy.p y x
```

#### C#

```csharp
p => x => y => p(y)(x)
```

#### Meaning

> Swap the alternatives selected by `p`.

---

### IF

#### Lambda Calculus

```text
IF = λp.λx.λy.p x y
```

#### C#

```csharp
p => x => y => p(x)(y)
```

#### Meaning

> Let `p` choose between the two alternatives.

---

### AND

#### Lambda Calculus

```text
AND = λp.λq.λx.λy.p (q x y) y
```

#### C#

```csharp
p => q => x => y => p(q(x)(y))(y)
```

#### Meaning

> If `p` selects the true path, let `q` decide; otherwise select the false alternative.

---

### OR

#### Lambda Calculus

```text
OR = λp.λq.λx.λy.p x (q x y)
```

#### C#

```csharp
p => q => x => y => p(x)(q(x)(y))
```

#### Meaning

> If `p` selects the true alternative, keep it; otherwise let `q` decide.

---

## 18. What you should understand

At the end of this milestone, you should recognize that no new primitive logical mechanism was required.

You already had:

```text
TRUE  → choose first
FALSE → choose second
```

From that behavior alone, you built:

```text
NOT
IF
AND
OR
```

The important shift is:

> Logical operators are not external operations applied to Church booleans. They are compositions of the behavior Church booleans already have.

`IF` makes this especially visible:

```text
IF = λp.λx.λy.p x y
```

The boolean itself performs the choice.

---

## Constraints

For this milestone, do not introduce:

- formal boolean algebra;
- combinatory logic;
- SKI calculus;
- beta-reduction notation;
- normal forms;
- type theory;
- Church numerals.

Do not implement:

```text
ZERO
ONE
TWO
THREE
SUCC
ADD
MULT
PAIR
FIRST
SECOND
ISZERO
```

Also:

- do not add external dependencies;
- do not create object-oriented wrappers around Church booleans;
- do not introduce production-style architecture;
- do not implement future milestones.

---

## Completion checklist

Before moving to Milestone 5, verify that:

- [ ] `ChurchBoolean<T>` exists if needed for readability;
- [ ] `Not` is implemented using Church boolean behavior only;
- [ ] `And` is implemented using Church boolean behavior only;
- [ ] `Or` is implemented using Church boolean behavior only;
- [ ] `If` delegates the choice to the Church boolean;
- [ ] currying remains visible in the public API;
- [ ] no `bool`, `if`, `switch`, `?:`, `&&`, `||`, or `!` is used in the Lambda Core;
- [ ] all reference logical tests pass;
- [ ] the kata project compiles;
- [ ] the new kata tests fail until the learner solves the exercises;
- [ ] `docs/04-logic.md` exists;
- [ ] no Church numeral or later concept has been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and confirm that they all pass.

Run the kata tests independently.

The newly added logical-operator kata tests should fail until the learner completes the exercises.

If the complete solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 5 yet.**
