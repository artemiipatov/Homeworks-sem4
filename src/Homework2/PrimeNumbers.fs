namespace Homework2

/// <summary>
/// Module that contains function for initializing infinite sequence of prime numbers.
/// </summary>
module PrimeNumbers =
    /// <summary>
    /// Checks if number is prime.
    /// </summary>
    /// <param name="n">Input number.</param>
    let isPrime n =

        let sqrt = float >> sqrt >> int

        [ 2 .. sqrt n ] |> List.forall (fun v -> n % v <> 0)

    /// <summary>
    /// Initializes infinite sequence of prime numbers.
    /// </summary>
    let initSeqOfPrimeNumber = (Seq.initInfinite id) |> Seq.filter isPrime
