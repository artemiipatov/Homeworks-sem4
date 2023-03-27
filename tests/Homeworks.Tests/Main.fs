open Expecto
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ TreeMap.treeMapTests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
