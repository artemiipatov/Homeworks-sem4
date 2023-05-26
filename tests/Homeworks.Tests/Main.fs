open Expecto

open Homework7
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ Lazy.tests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
