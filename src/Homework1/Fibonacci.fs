namespace Homework1

/// <summary>
/// Module that contains fibonacci implementation.
/// </summary>
module Fibonacci =
    /// <summary>
    /// Calculates fibonacci sequence.
    /// </summary>
    /// <param name="n">Length of the sequence.</param>
    let fibonacci n =
        let fibonacciSequence =
            (0, 1)
            |> Seq.unfold (fun (a, b) -> Some(a + b, (b, a + b)))
            |> Seq.take n

        Seq.append
            [
                0
                1
            ]
            fibonacciSequence
