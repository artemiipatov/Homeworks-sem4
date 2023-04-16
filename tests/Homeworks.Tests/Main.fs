open Expecto
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ PointFree.tests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
