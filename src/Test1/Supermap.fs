namespace Test1

module Supermap =
    let run valueList funList =
        valueList
        |> List.map (fun value -> funList |> List.map (fun func -> func value))
        |> List.concat
