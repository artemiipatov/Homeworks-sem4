open Expecto
open Homeworks.Tests

[<Tests>]
let allTests =
    testList "all Tests" [ Interpreter.generalTests; Interpreter.alphaConversionTest ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithCLIArgs [] argv
