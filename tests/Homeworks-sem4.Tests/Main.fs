open Expecto

[<Tests>]
let allTests = testList "all Tests" []

[<EntryPoint>]
let main argv =
    allTests
    |> runTestsWithCLIArgs [] argv
