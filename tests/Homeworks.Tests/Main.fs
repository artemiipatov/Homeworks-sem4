open Expecto
open Homework6
open Homeworks.Tests

[<Tests>]
let allTests =
    testList "all Tests" [ StringEvaluation.generalTests; Round.generalTests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
