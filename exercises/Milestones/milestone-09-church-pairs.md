# Milestone 9 — Church Pairs

## Goal

Introduce Church-encoded pairs as data represented through functions.

You will implement:

```text
PAIR   = λx.λy.λf.f x y
FIRST  = λp.p TRUE
SECOND = λp.p FALSE
```

The central idea is:

> A pair does not have to store two fields.  
> It can be a function that knows how to give its two values to another function.

This milestone reinforces one of the main themes of LambdaFromScratch:

> Data can be represented as behavior.

Do not implement `IsZero`, predecessor, subtraction, comparisons, recursion, lists, or any later concept yet.

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
Principles/08-multiplication.md
Principles/09-pairs.md
```

You should already understand:

```text
TRUE  = λx.λy.x
FALSE = λx.λy.y
```

as selectors:

```text
TRUE  → choose the first value
FALSE → choose the second value
```

That behavior will become useful again in this milestone.

---

## 1. Start from familiar C#

A conventional C# pair might look like:

```csharp
var pair = ("left", "right");

var first = pair.Item1;
var second = pair.Item2;
```

This representation stores two values in a data structure.

Now ask:

> What if we remove tuples, records, objects, and fields too?

We still need something that can preserve two values and later allow another piece of code to decide what to do with them.

That is the problem Church pairs solve.

---

## 2. Change the mental model

A conventional pair is easy to imagine as:

```text
Pair
├── First
└── Second
```

A Church pair uses a different model:

```text
Pair
  ↓
give both values to a function
```

A Church pair does not expose fields directly.

Instead:

> It receives a function and lets that function decide what to do with the two captured values.

That is the conceptual center of this milestone.

---

## 3. Reveal PAIR

Use:

```text
PAIR = λx.λy.λf.f x y
```

Read it progressively:

```text
λx.        take x
    λy.    take y
        λf. take f
            f x y
```

In plain English:

> Take `x`.  
> Take `y`.  
> Take a function `f`.  
> Give `x` and `y` to `f`.

Because function application associates to the left:

```text
f x y
```

means:

```text
(f x) y
```

and corresponds to curried C#:

```csharp
f(x)(y)
```

So the C# shape should remain recognisable as:

```csharp
x => y => f => f(x)(y)
```

---

## 4. Why this is a pair

A natural question is:

> Where are the two values stored?

Do not answer in object-oriented terms.

For:

```text
PAIR x y
```

the resulting function captures `x` and `y`.

Later, when a function `f` is supplied, the pair evaluates:

```text
f x y
```

So the two values are preserved by the returned function itself.

In C# terms, they are captured by the closure.

No tuple or object with two fields is required.

---

## 5. Pair behavior

Conceptually:

```text
PAIR "left" "right"
```

does not immediately return either value.

It returns something that is waiting for another function.

Think of:

```text
PAIR x y selector
```

as:

```text
selector x y
```

That is the fundamental behavior.

A Church pair is therefore not primarily a container.

It is a function that gives its two captured values to another function.

---

## 6. C# typing

C#'s type system makes Church pairs slightly more awkward than the encodings you have seen so far.

Use the smallest named delegates necessary to keep the representation readable.

Do not solve typing problems with:

- interfaces;
- wrapper classes;
- visitors;
- inheritance;
- generic frameworks.

If keeping both pair elements of the same type makes the implementation significantly clearer, prefer a homogeneous representation such as:

```text
Pair<T>
```

where both values are `T`.

For this kata, clarity is more important than supporting heterogeneous pairs such as:

```text
(string, int)
```

Do not introduce extra machinery just to support mixed element types.

If you deliberately choose homogeneous pairs, explain that briefly in the documentation.

---

## 7. Reference implementation

Work under:

```text
src/LambdaFromScratch/Pairs/
```

Expose:

```text
Pair
First
Second
```

Keep currying visible.

Conceptual usage should resemble:

```csharp
var pair = Pair("left")("right");

var first = First(pair);
var second = Second(pair);
```

or the closest equivalent allowed by the chosen delegate representation.

Do not prefer APIs such as:

```csharp
new Pair<string>("left", "right")
```

or:

```csharp
pair.First
pair.Second
```

Those forms would hide the Church encoding.

---

## 8. Mandatory representation restrictions

The Church pair itself must not use:

```csharp
Tuple<,>
ValueTuple<,>
record
record struct
KeyValuePair<,>
object[]
```

Do not create:

```text
a class with First and Second fields
a struct with First and Second fields
```

Do not store the values in a conventional pair container.

The pair must fundamentally remain a function.

---

## 9. Derive FIRST from TRUE

Do not start by treating `FIRST` as ordinary field access.

Ask instead:

> If a pair gives its two values to a function, what function should we give it to retrieve the first value?

We need a function that receives:

```text
x
y
```

and returns:

```text
x
```

But you already have exactly that:

```text
TRUE = λx.λy.x
```

So:

```text
FIRST = λp.p TRUE
```

means:

> Give `TRUE` to the pair.

The pair then evaluates:

```text
TRUE x y
```

and `TRUE` selects `x`.

Expected behavior:

```text
FIRST (PAIR "left" "right")
→ "left"
```

Do not implement `First` by inspecting stored state, tuple positions, indexes, or fields.

---

## 10. Derive SECOND from FALSE

Now ask:

> What selector returns the second value?

You already have:

```text
FALSE = λx.λy.y
```

Therefore:

```text
SECOND = λp.p FALSE
```

means:

> Give `FALSE` to the pair.

The pair evaluates:

```text
FALSE x y
```

and `FALSE` selects `y`.

Expected behavior:

```text
SECOND (PAIR "left" "right")
→ "right"
```

Again, do not use conventional C# field or tuple access.

---

## 11. Reuse Church booleans

Reuse the Church boolean behavior already present in the project.

Do not introduce unrelated selector functions merely to avoid using `TRUE` and `FALSE`.

The important connection is:

```text
TRUE  → first selector
FALSE → second selector
```

More precisely:

```text
p TRUE  → first element
p FALSE → second element
```

This reuse should be visible in both the implementation and the documentation.

---

## 12. Walk through FIRST (PAIR A B)

Start with:

```text
FIRST (PAIR A B)
```

Using:

```text
FIRST = λp.p TRUE
```

this becomes conceptually:

```text
(PAIR A B) TRUE
```

Now the pair gives its two values to `TRUE`:

```text
TRUE A B
```

`TRUE` chooses the first value:

```text
A
```

Therefore:

```text
FIRST (PAIR A B) → A
```

No formal beta-reduction notation is needed.

Just follow which function receives which values.

---

## 13. Walk through SECOND (PAIR A B)

Similarly:

```text
SECOND (PAIR A B)
```

becomes:

```text
(PAIR A B) FALSE
```

Then:

```text
FALSE A B
```

and finally:

```text
B
```

Therefore:

```text
SECOND (PAIR A B) → B
```

---

## 14. Reference tests

Add focused xUnit tests under:

```text
src/LambdaFromScratch.Tests/Pairs/
```

Test at least:

```text
First returns the first value
Second returns the second value
```

For example:

```csharp
var pair = Pair("left")("right");

Assert.Equal("left", First(pair));
Assert.Equal("right", Second(pair));
```

Adapt the syntax to the actual implementation.

Also test another value type, for example:

```text
PAIR 10 20

FIRST  → 10
SECOND → 20
```

Keep every test independently readable.

Do not create sophisticated fixtures or shared test abstractions.

---

## 15. Add a behavioral selector test

Do not test the pair only through `First` and `Second`.

Add at least one test showing what the pair fundamentally does:

> It gives both values to a supplied function.

If the chosen type representation allows it naturally, use a selector conceptually equivalent to:

```csharp
x => y => $"{x}:{y}"
```

so that:

```text
PAIR "left" "right"
```

can produce:

```text
"left:right"
```

through the selector.

This reinforces that the pair is not fundamentally a container with two properties.

If returning a different result type would complicate the homogeneous pair representation, keep the selector test within the same type rather than introducing abstractions solely for the test.

---

## 16. Kata implementation

Create the corresponding incomplete exercises under:

```text
exercises/LambdaFromScratch.Kata/Pairs/
```

Include:

```text
Pair
First
Second
```

The kata project must compile.

Do not include the completed C# implementations in comments.

Comments may contain only the Lambda definitions:

```text
PAIR   = λx.λy.λf.f x y
FIRST  = λp.p TRUE
SECOND = λp.p FALSE
```

The learner should derive the C# implementation.

---

## 17. Kata tests

Add corresponding tests under:

```text
src/LambdaFromScratch.Kata.Tests/Pairs/
```

At minimum test:

```text
FIRST (PAIR x y)  → x
SECOND (PAIR x y) → y
```

Use simple strings first.

Include another value type if it remains clear.

The kata tests should fail until the learner completes the functions.

Do not skip or disable them.

---

## 18. Documentation

Create:

```text
docs/09-pairs.md
```

Start from ordinary C#:

```csharp
var pair = ("left", "right");

var first = pair.Item1;
var second = pair.Item2;
```

Then ask:

> What if we remove tuples, records, objects, and fields too?

Establish the problem before revealing the Lambda encoding.

The reader should first understand that we need behavior capable of preserving two values and later giving them to something else.

Then introduce:

```text
PAIR = λx.λy.λf.f x y
```

---

## 19. Use the three views

### PAIR

#### Lambda Calculus

```text
PAIR = λx.λy.λf.f x y
```

#### C#

Use the actual project implementation, keeping it structurally similar to:

```csharp
x => y => f => f(x)(y)
```

#### Meaning

> Capture two values and later give them to another function.

---

### FIRST

#### Lambda Calculus

```text
FIRST = λp.p TRUE
```

#### C#

Show the actual implementation based on the existing Church `True`.

#### Meaning

> Give the pair a selector that chooses the first value.

---

### SECOND

#### Lambda Calculus

```text
SECOND = λp.p FALSE
```

#### C#

Show the actual implementation based on the existing Church `False`.

#### Meaning

> Give the pair a selector that chooses the second value.

---

## 20. Make the reuse explicit

The progression is:

```text
Booleans
    ↓
TRUE and FALSE choose alternatives

Pairs
    ↓
PAIR gives two alternatives to a selector

Therefore
    ↓
TRUE  becomes the first selector
FALSE becomes the second selector
```

This is one of the most interesting aspects of Church encoding.

Previously built behavior can become part of a completely different abstraction.

---

## 21. The key conceptual shift

Avoid this as the primary mental model:

```text
Pair = object with two fields
```

Use instead:

```text
Pair = function that captures two values
       and later gives them to another function
```

A useful sentence is:

> A Church pair does not expose its values directly. It exposes a way to use them.

This is the central insight of the milestone.

---

## 22. What you should understand

At the end of this milestone, you should be able to read:

```text
λx.λy.λf.f x y
```

and mentally recognize:

```csharp
x => y => f => f(x)(y)
```

You should also understand why:

```text
FIRST = λp.p TRUE
SECOND = λp.p FALSE
```

works.

`TRUE` and `FALSE` were originally introduced as booleans, but their deeper behavior is selection.

Church pairs reuse that behavior directly.

---

## 23. Prepare the next milestone

So far you have seen functions represent:

```text
booleans
logic
numbers
arithmetic
pairs
```

A natural next question is:

> Can we inspect Church-encoded values and derive predicates from their behavior?

Do not implement or explain `IsZero` yet.

That belongs to the next milestone.

---

## Constraints

For this milestone, do not introduce:

- product types;
- category theory;
- algebraic data types in depth;
- existential types;
- formal closure semantics;
- Church lists;
- recursive tuples;
- predecessor algorithms based on pairs.

Do not implement:

```text
ISZERO
PREDECESSOR
SUBTRACT
LEQ
EQ
LIST
HEAD
TAIL
Y combinator
FACTORIAL
```

Also:

- do not introduce heterogeneous-pair machinery if it significantly complicates the implementation;
- do not add external dependencies;
- do not refactor earlier Church encodings unless strictly required.

---

## Completion checklist

Before moving to Milestone 10, verify that:

- [ ] `Pair` exists in the reference implementation;
- [ ] `First` exists in the reference implementation;
- [ ] `Second` exists in the reference implementation;
- [ ] the pair remains function-based;
- [ ] no tuple, record, object pair, or field-based representation is used;
- [ ] `First(Pair("left")("right"))` returns `"left"`;
- [ ] `Second(Pair("left")("right"))` returns `"right"`;
- [ ] another value type is covered by tests;
- [ ] at least one reference test exercises the pair with a custom selector;
- [ ] `First` derives its selection behavior from Church `True`;
- [ ] `Second` derives its selection behavior from Church `False`;
- [ ] reference tests pass;
- [ ] the kata project compiles;
- [ ] kata tests fail until the learner implements the pair functions;
- [ ] `docs/09-pairs.md` exists;
- [ ] `IsZero` and later concepts have not been implemented.

---

## Verification

Run:

```bash
dotnet build LambdaFromScratch.slnx
```

The solution must build successfully.

Run the reference tests independently and verify that all reference tests pass.

Run the kata tests independently.

The new Pair kata tests should fail until the learner completes the exercises.

If the full solution test command reports failure because kata tests intentionally fail, that is acceptable.

Do not disable or skip kata tests merely to make the whole solution green.

**Stop here. Do not start Milestone 10 yet.**
