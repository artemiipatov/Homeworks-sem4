module Homeworks.Tests.StringEvaluation

open Expecto
open Homework6.StringEvaluation

let createTest actualFun expected testName =
    test testName {
        let actual = actualFun ()
        Expect.equal actual expected "Results must be the same"
    }

let generalTests =
    [ let actualFun () =
          calculate {
              let! x = "1"
              let! y = "2"
              let z = x + y
              return z
          }

      createTest actualFun (Some 3) "test 1"

      let actualFun () =
          calculate {
              let! x = "1"
              let! y = "b"
              let z = x + y
              return z
          }

      createTest actualFun (None) "test 2" ]
    |> testList "generalTests"
