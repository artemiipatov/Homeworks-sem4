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

let compareWithStaticSeq =
    test "static seq" {
        let correctSequence = seq { 1; 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41 }
        Expect.equal (PrimeNumbers.initSeqOfPrimeNumber |> Seq.take 14) correctSequence ""
    }
let primeNumbersTests =
    [ checkSequence PrimeNumbers.initSeqOfPrimeNumber
      |> testProperty "all numbers in the sequence are prime"

      checkPrime |> testProperty "isPrime works properly"

      compareWithStaticSeq ]
    |> testList "general tests"
