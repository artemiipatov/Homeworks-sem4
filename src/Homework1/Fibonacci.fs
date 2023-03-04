module Homework1.Fibonacci

let Fibonacci n =
    let fibonacciSequence = (0, 1)
                            |> Seq.unfold (fun (a, b) -> Some(a + b, (b, a + b)))
                            |> Seq.take n

    Seq.append [0; 1] fibonacciSequence
