open Expecto
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ BracketChecker.generalTests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
