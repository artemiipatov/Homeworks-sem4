module Homeworks.Tests.Round

open Expecto
open Homework6.Round

let createTest actualFun expected testName =
    test testName {
        let actual = actualFun ()
        Expect.equal actual expected "Results must be the same"
    }

let generalTests =
    [ let rounding precision =
          RoundBuilder(precision)

      let actualFun () =
          rounding 3 {
              let! a = 2.0 / 12.0
              let! b = 3.5
              return a / b
          }

      createTest actualFun (0.048) "test 1"

      let actualFun () =
          rounding 5 {
              let! a = 100.0 / 56.0
              let! b = 3.5
              let! c = 4.3
              return c * a / b
          }

      createTest actualFun 2.19387 "test 2" ]

    |> testList "rounding tests"
