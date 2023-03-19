open Expecto
open Homeworks.Tests

[<Tests>]
let allTests = testList "all Tests" [ EvenNumbers.evenNumbersTests ]

[<EntryPoint>]
let main argv =
    allTests
    |> runTestsWithCLIArgs [] argv
