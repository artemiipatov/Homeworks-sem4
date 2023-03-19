open Expecto
open Homeworks.Tests.PrimeNumbers

[<Tests>]
let allTests = testList "all Tests" [ primeNumbersTests ]

[<EntryPoint>]
let main argv =
    allTests
    |> runTestsWithCLIArgs [] argv
