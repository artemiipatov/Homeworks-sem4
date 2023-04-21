module Homeworks.Tests.EvenNumbers

open Expecto
open Homework2

let checkCorrectness func (count: uint) =
    let isEven n = (n % 2 = 0)

    let countEvenNumbers list =
        let rec countRec list acc =
            match list with
            | [] -> acc
            | h :: t when isEven h -> countRec t (acc + 1)
            | _ :: t -> countRec t acc

        countRec list 0

    let list = [ 0 .. (int count) ]

    let expected = countEvenNumbers list
    let actual = func list

    Expect.equal expected actual "Counters should be equal"

let evenNumbersTests =
    testList
        "Even number counters tests"
        [ (checkCorrectness EvenNumbers.CountUsingFilter)
          |> testProperty "CountUsingFilter should correctly count even numbers"

          (checkCorrectness EvenNumbers.CountUsingFold)
          |> testProperty "CountUsingFold should correctly count even numbers"

          (checkCorrectness EvenNumbers.CountUsingMap)
          |> testProperty "CountUsingMap should correctly count even numbers" ]
