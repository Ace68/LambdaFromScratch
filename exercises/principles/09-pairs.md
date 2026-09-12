# 09 — Church Pairs

## Start from C#

In everyday C#, a pair can be a tuple:

```csharp
var pair = ("left", "right");

var first = pair.Item1;
var second = pair.Item2;
```

The tuple stores two values and exposes each one by position.

> What if we remove tuples, records, objects, and fields too?

We still need something capable of preserving two values and later allowing us
to choose what to do with them.

The familiar representation looks like data with two parts:

```text
Pair
├── First
└── Second
```

The Church representation changes that mental model:

```text
Pair
  ↓
give both values to a function
```

> A Church pair does not expose its values directly.
> It receives a function and lets that function decide what to do with them.

## Revealing PAIR

The Church encoding is:

```text
PAIR = λx.λy.λf.f x y
```

Read it progressively:

```text
λx.        take x
   λy.     take y
      λf.  take f
           f x y
```

In natural English:

> Take `x`.
> Take `y`.
> Take `f`.
> Apply `f` to `x` and `y`.

Function application associates to the left, so:

```text
f x y
```

means:

```text
(f x) y
```

The corresponding curried C# application is:

```csharp
f(x)(y)
```

## Where are the values?

For:

```text
PAIR x y
```

the resulting function captures `x` and `y`. When it later receives `f`, it
evaluates:

```text
f x y
```

The values are remembered through the returned function's lexical closure.
That is enough to preserve and use them without an object containing two
fields.

This project uses a homogeneous pair:

```csharp
ChurchPair<T>
```

Both values have the same type `T`. This keeps the selector and the Lambda
structure visible without introducing interfaces or other machinery merely to
support unrelated types in one pair.

## Three views of PAIR

### Lambda Calculus

```text
PAIR = λx.λy.λf.f x y
```

### C#

```csharp
public static Func<T, ChurchPair<T>> Pair<T>(T x) =>
    y => selector => selector(x)(y);
```

The method parameter represents the first lambda:

```text
λx.λy.λf.f x y
 x => y => f => f(x)(y)
```

### Meaning

> Capture two values and later give them to another function.

For example:

```csharp
var pair = Pair("left")("right");
ChurchBoolean<string> join = x => y => $"{x}:{y}";

var result = pair(join); // "left:right"
```

The selector receives both values. The pair is behavior that accepts this
function, not a container with properties.

## Deriving FIRST from TRUE

To select the first value, we need a function that receives `x` and `y` and
returns `x`.

We already have exactly that function:

```text
TRUE = λx.λy.x
```

Therefore:

```text
FIRST = λp.p TRUE
```

means:

> Give TRUE to the pair.

The pair supplies both values:

```text
TRUE x y
```

and `TRUE` selects `x`.

### Three views of FIRST

#### Lambda Calculus

```text
FIRST = λp.p TRUE
```

#### C#

```csharp
public static T First<T>(ChurchPair<T> p) =>
    p(True<T>);
```

#### Meaning

> Give the pair the selector that chooses its first value.

## Deriving SECOND from FALSE

To select the second value, we need:

```text
λx.λy.y
```

We already know that function as:

```text
FALSE = λx.λy.y
```

Therefore:

```text
SECOND = λp.p FALSE
```

means:

> Give FALSE to the pair.

The pair supplies both values:

```text
FALSE x y
```

and `FALSE` selects `y`.

### Three views of SECOND

#### Lambda Calculus

```text
SECOND = λp.p FALSE
```

#### C#

```csharp
public static T Second<T>(ChurchPair<T> p) =>
    p(False<T>);
```

#### Meaning

> Give the pair the selector that chooses its second value.

## Walking through FIRST (PAIR A B)

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

PAIR gives its values to TRUE:

```text
TRUE A B
```

TRUE chooses the first value:

```text
A
```

Therefore:

```text
FIRST (PAIR A B) → A
```

## Walking through SECOND (PAIR A B)

Similarly:

```text
SECOND (PAIR A B)
```

becomes:

```text
(PAIR A B) FALSE
```

then:

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

## Reusing earlier ideas

The selectors are not new special-purpose mechanisms:

```text
Booleans
    ↓
TRUE and FALSE choose alternatives

Pairs
    ↓
PAIR exposes two alternatives to a selector

Therefore

TRUE  becomes FIRST
FALSE becomes SECOND
```

More precisely:

```text
p TRUE  → first element
p FALSE → second element
```

Two ideas that were introduced separately now compose naturally. Church
booleans choose between alternatives, and a Church pair gives its two values to
one of those selectors.

## What we have learned

A pair does not have to store two fields:

```text
PAIR = λx.λy.λf.f x y
```

It can capture two values and offer them to another function. `FIRST` and
`SECOND` do not inspect the pair. They supply the Church booleans that already
know how to select the first or second alternative.

> Data can be represented as behavior.
