open Expecto
open Homeworks.Tests
open Homeworks.Tests.Test1

[<Tests>]
let allTests = testList "all Tests" [ Supermap.tests; Rhombus.generalTests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
