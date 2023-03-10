namespace Homework1

/// <summary>
/// Module that implements method that calculates factorial.
/// </summary>
module Factorial =
    /// <summary>
    /// Calculates factorial.
    /// </summary>
    /// <param name="number">Last number in factorial.</param>
    let factorial number =
        let rec factorialRec acc n =
            match n with
            | 0
            | 1 -> acc
            | _ -> factorialRec (acc * n) (n - 1)

        match number with
        | _ when number < 0 -> None
        | _ -> Some(factorialRec 1 number)
