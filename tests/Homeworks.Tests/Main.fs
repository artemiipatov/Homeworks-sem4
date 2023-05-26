open Expecto

open Homeworks.Tests

let config = { defaultConfig with allowDuplicateNames = true }

[<Tests>]
let allTests = testList "all Tests" [ MiniCrawler.tests ]

[<EntryPoint>]
let main argv =
    allTests |> runTestsWithArgs config argv
