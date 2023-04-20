namespace Homework1

/// <summary>
/// Module that contains function that creates sequence of powers of 2.
/// </summary>
module Sequence =
    /// <summary>
    /// Calculates
    /// </summary>
    /// <param name="n">Smallest power in the sequence.</param>
    /// <param name="m">Highest power in the sequence.</param>
    let createSequenceOfPowersOfTwo n m =
        if n > m then
            failwith "n should be less or equal to m"

        (n, 2.0 ** n)
        |> Seq.unfold (fun (power, number) ->
            match (power, number) with
            | power, _ when power > m -> None
            | _ ->
                Some(
                    (number),
                    (power
                     + 1.0,
                     number
                     * 2.0)
                )
        )
