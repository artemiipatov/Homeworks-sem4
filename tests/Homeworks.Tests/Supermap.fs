module Homeworks.Tests.Test1.Supermap

open Expecto
open Test1.Supermap

let createTest mapFunList valueList expected testName =
    test testName {
        let actual: int list = run valueList mapFunList
        Expect.equal actual expected "Results must be the same"
    }

let testCommonMap valueList =
    [ let mapFun = (*) 3
      let expected = valueList |> List.map mapFun
      createTest [ mapFun ] valueList expected "Common map" ]
    |> testList "Common map"

let generalTests (valueList, coefficients) =
    [ let mapFun = coefficients |> List.map (fun x -> (*) x)

      let expected: int list =
          let rec calculate values mappedValues =
              match values with
              | [] -> mappedValues
              | v :: tail ->
                  calculate
                      tail
                      (mappedValues @ (mapFun |> List.map (fun func -> func v)))

          calculate valueList []

      createTest mapFun valueList expected "Many functions list" ]
    |> testList "General"

let tests =
    [ testCommonMap
      |> testProperty "Supermap works as common map on one function lists"
      generalTests |> testProperty "General" ]
    |> testList "Supermap"
