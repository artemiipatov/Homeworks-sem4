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

        (2.0 ** n)
        |> Seq.unfold (fun state ->
            match state with
            | state when state > m -> None
            | _ ->
                Some(
                    state,
                    state
                    * 2.0
                )
        )
