namespace Homework2

module PrimeNumbers =
    let isPrime n =

        let sqrt =
            float
            >> sqrt
            >> int

        [ 2 .. sqrt n ]
        |> List.forall (fun v -> n % v <> 0)

    let initSeqOfPrimeNumber =
        (Seq.initInfinite id)
        |> Seq.filter isPrime
