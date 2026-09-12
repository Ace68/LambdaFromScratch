namespace LambdaFromScratch.Numerals;

/// <summary>
/// A Church numeral is not a stored number.
/// It describes how many times a function should be applied.
/// ZERO  = λf.λx.x
/// ONE   = λf.λx.f x
/// TWO   = λf.λx.f (f x)
/// THREE = λf.λx.f (f (f x))
/// </summary>
public static class ChurchNumerals
{
    public static Func<T, T> Zero<T>(Func<T, T> f) =>
        x => x;

    public static Func<T, T> One<T>(Func<T, T> f) =>
        x => f(x);

    public static Func<T, T> Two<T>(Func<T, T> f) =>
        x => f(f(x));

    public static Func<T, T> Three<T>(Func<T, T> f) =>
        x => f(f(f(x)));

    public static Func<T, T> Four<T>(Func<T, T> f) =>
        x => f(f(f(f(x))));

    public static Func<T, T> Five<T>(Func<T, T> f) =>
        x => f(f(f(f(f(x)))));

    public static Func<T, T> Six<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(x))))));

    public static Func<T, T> Seven<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(x)))))));

    public static Func<T, T> Eight<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(f(x))))))));

    public static Func<T, T> Nine<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(f(f(x)))))))));

    public static Func<T, T> Ten<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(f(f(f(x))))))))));

    /// <summary>
    /// SUCC = λn.λf.λx.f (n f x)
    /// A Church numeral represents repeated application of a function.
    /// Its successor applies that same function one additional time.
    /// 
    /// How to Read Successor, Add, and Multiply
    /// A useful way to read these functions is to separate the C# method signature from the functional body.
    /// One important detail is that the first λ from the Lambda Calculus expression is often represented in C# by the method parameter itself.
    ///
    /// The three ideas together
    /// The three operations can be remembered in a very compact way.
    /// Successor
    /// SUCC = λn.λf.λx.f (n f x)
    /// Do what n does, then apply f once more.
    /// 
    /// Addition
    /// ADD = λm.λn.λf.λx.m f (n f x)
    /// Do what n does, then let m continue.
    /// 
    /// Multiplication
    /// MULT = λm.λn.λf.m (n f)
    /// Turn n into a repetition, then repeat that repetition m times.
    /// 
    /// The important distinction is:
    /// Addition chains repetitions.
    /// Multiplication repeats a repetition.
    /// That difference is small in the code, but conceptually it is one of the most interesting parts of Church numerals.
    /// </summary>

    /// public static ChurchNumeral<T> Successor<T>(ChurchNumeral<T> n) => f => x => f(n(f)(x));
    /// Start with the signature:
    /// Successor<T>(ChurchNumeral<T> n)
    /// It says:
    /// Take a Church numeral n and return another Church numeral.
    /// Now read the functional body:
    /// f => x => f(n(f)(x))
    /// as:
    /// Take f.
    /// Then take x.
    /// Let n apply f to x.
    /// Finally, apply f one more time to the result.
    /// The expression:
    /// n(f)(x)
    /// means:
    /// Let n apply f to x as many times as n represents.
    /// Then:
    /// f(n(f)(x))
    /// means:
    /// Do everything n already does, then apply f once more.
    /// This corresponds directly to:
    /// SUCC = λn.λf.λx.f (n f x)
    /// We can mentally rewrite the C# as:
    /// n => f => x => f(n(f)(x))
    /// The initial:
    /// n =>
    /// is simply represented by the method parameter:
    /// Successor(ChurchNumeral<T> n)
    /// A concise way to remember Successor is:
    /// Do what n does, then apply f once more.
    public static ChurchNumeral<T> Successor<T>(ChurchNumeral<T> n) =>
        f => x => f(n(f)(x));

    /// <summary>
    /// public static Func<ChurchNumeral<T>, ChurchNumeral<T>> Add<T>(ChurchNumeral<T> m) => n => f => x => m(f)(n(f)(x));
    /// Start again with the method signature:
    /// Add<T>(ChurchNumeral<T> m)
    /// It says:
    /// Take a Church numeral m.
    /// The return type is:
    /// Func<ChurchNumeral<T>, ChurchNumeral<T>>
    /// which means:
    /// Return a function that takes another Church numeral n and returns a new Church numeral.
    /// Now read the body:
    /// n => f => x => m(f)(n(f)(x))
    /// step by step:
    /// Take n.
    ///     Then take a function f.
    ///     Then take a starting value x.
    ///     Let n apply f to x.
    ///     Take that result and let m continue applying f.
    ///     The inner expression:
    /// n(f)(x)
    ///     means:
    /// Apply f according to n.
    ///     Then:
    /// m(f)(n(f)(x))
    /// means:
    /// Starting from the result produced by n, apply f according to m as well.
    /// This corresponds almost one-to-one with:
    /// ADD = λm.λn.λf.λx.m f (n f x)
    /// and its C# shape:
    /// m => n => f => x => m(f)(n(f)(x))
    /// Again, the first:
    /// m =>
    /// has been absorbed into the method parameter:
    /// Add(ChurchNumeral<T> m)
    /// Example: TWO + THREE
    /// Suppose:
    /// m = TWO
    /// n = THREE
    /// Then:
    /// m f (n f x)
    /// becomes:
    /// TWO f (THREE f x)
    /// THREE applies f three times:
    /// x
    /// → f(x)
    ///   → f(f(x))
    ///     → f(f(f(x)))
    /// Then TWO continues from there and applies f two more times:
    /// f(f(f(x)))
    ///     → f(f(f(f(x))))
    ///     → f(f(f(f(f(x)))))
    /// 
    /// The result is five applications of f.
    /// Notice that Add never needs to know that TWO means 2 or THREE means 3.
    /// A concise way to remember Add is:
    /// Do what n does, then let m continue.
    public static Func<ChurchNumeral<T>, ChurchNumeral<T>> Add<T>(ChurchNumeral<T> m) =>
        n => f => x => m(f)(n(f)(x));

    /// <summary>
    /// public static Func<ChurchNumeral<T>, ChurchNumeral<T>> Multiply<T>(ChurchNumeral<T> m) => n => f => m(n(f));
    /// The method signature says:
    /// Take a Church numeral m and return a function that takes another Church numeral n.
    /// Now consider the body:
    /// n => f => m(n(f))
    /// Read it as:
    /// Take n.
    /// Then take f.
    /// Build n(f), which is itself a transformation that applies f repeatedly.
    /// Then ask m to repeat that whole transformation.
    /// The key expression is:
    /// n(f)
    /// This does not produce a final value.
    /// It produces another function:
    /// T → T
    /// For example, if n is Three, then:
    /// Three(f)
    /// behaves like:
    /// x => f(f(f(x)))
    /// It is a transformation that applies f three times.
    /// Now:
    /// m(n(f))
    /// means:
    /// Repeat that entire transformation according to m.
    /// Example: TWO × THREE
    /// Consider:
    /// Multiply(Two)(Three)
    /// First:
    /// Three(f)
    /// creates a transformation that applies f three times.
    /// Then:
    /// Two(Three(f))
    /// applies that three-step transformation twice.
    /// Conceptually:
    /// x
    /// ↓ THREE f
    /// f(f(f(x)))
    ///     ↓ THREE f
    /// f(f(f(f(f(f(x))))))
    /// So the final result applies f six times.
    /// Again, Multiply never needs to know anything about the integer values 2, 3, or 6.
    /// It works entirely by composing behavior.
    /// A concise way to remember Multiply is:
    /// Turn n into a repetition, then repeat that repetition m times.
    /// Why does Multiply not have x =>?
    /// At first, you might expect multiplication to be written as:
    /// m => n => f => x => m(n(f))(x)
    /// That would be correct.
    /// However, the implementation is shorter:
    /// m => n => f => m(n(f))
    /// Why?
    /// Because:
    /// m(n(f))
    /// already returns a:
    /// Func<T, T>
    ///     which is exactly the function that would receive x.
    /// Therefore:
    /// x => m(n(f))(x)
    /// can simply be written as:
    /// m(n(f))
    /// The final x is not missing conceptually. It is simply unnecessary in the C# expression.
    /// This mirrors the compact Lambda Calculus definition:
    /// MULT = λm.λn.λf.m (n f)
    /// rather than the fully expanded equivalent:
    /// MULT = λm.λn.λf.λx.m (n f) x
    public static Func<ChurchNumeral<T>, ChurchNumeral<T>> Multiply<T>(ChurchNumeral<T> m) =>
        n => f => m(n(f));
}
