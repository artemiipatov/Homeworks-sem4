open Expecto
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ ParseTree.ParseTreeTests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
