module Homeworks.Tests.PrimeNumbers

open Expecto
open Homework2

let isPrime n =
    let mutable isPrime = true

    for i in 2 .. n - 1 do
        if n % i = 0 then
            isPrime <- false

    isPrime

let checkSequence source (count: uint) =
    source |> Seq.take (int count) |> Seq.forall isPrime

let checkPrime n =
    isPrime n = PrimeNumbers.isPrime n

let primeNumbersTests =
    [ checkSequence PrimeNumbers.initSeqOfPrimeNumber
      |> testProperty "all numbers in the sequence are prime"

      checkPrime |> testProperty "isPrime works properly" ]
    |> testList "general tests"
