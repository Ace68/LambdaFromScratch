In the 1930s, one of the great questions in mathematics was surprisingly simple to state:

**what does it really mean to compute something?**

Alonzo Church, an American mathematician and logician, was one of the key figures trying to answer that question. While searching for a formal foundation for mathematics, he developed the **Lambda Calculus**, an extremely small formal system built essentially around three ideas: defining functions, applying functions, and substituting expressions.

There are no classes, objects, loops, or mutable variables. And yet, with these few rules, we can represent numbers, Boolean values, conditions, data structures and, more generally, any computation.

At almost the same time, a young English mathematician named **Alan Turing** approached the same problem from a completely different direction. Turing imagined an abstract machine capable of reading and writing symbols on a tape: what we now call a **Turing Machine**.

At first sight, the two models could hardly look more different.

Church described computation through functions and symbolic transformations.

Turing described it through a machine, a state, and a sequence of operations.

And yet they turned out to have the **same computational power**.

This is closely related to what we now call the **Church-Turing thesis**: the idea that anything that can be computed by an effective mechanical procedure can be expressed by these models of computation.

There is also an interesting historical connection between the two. Turing later went to Princeton, where he worked with Church and completed his PhD under Church's supervision in 1938.

So Lambda Calculus is not simply a curious ancestor of functional programming languages. It is one of the fundamental ideas through which computer science learned to define what computation itself is.

And perhaps this is the most surprising part.

To build something computationally universal, Church needed very little.

**A function.
An argument.
And the ability to apply one to the other.**

And that is exactly where we can start.
