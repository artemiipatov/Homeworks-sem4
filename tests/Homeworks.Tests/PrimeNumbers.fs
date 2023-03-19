module Homeworks.Tests.PrimeNumbers

open Expecto
open Homework2

let makeTest source (count: uint) =
    source
    |> Seq.take (int count)
    |> Seq.forall PrimeNumbers.isPrime

let primeNumbersTests =
    PrimeNumbers.initSeqOfPrimeNumber
    |> makeTest
    |> testProperty "all numbers in the sequence are prime"
