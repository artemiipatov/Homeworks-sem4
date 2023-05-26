open Expecto
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ Phonebook.tests ] |> testSequenced

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
