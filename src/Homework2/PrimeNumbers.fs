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

        let sqrtN = n |> float |> sqrt |> int

        let rec check i =
            i > sqrtN || (n % i <> 0 && check (i + 1))

        check 2

    /// <summary>
    /// Initializes infinite sequence of prime numbers.
    /// </summary>
    let initSeqOfPrimeNumber = (Seq.initInfinite id) |> Seq.filter isPrime
