module Homeworks.Tests.Test1.Rhombus

open Expecto
open Test1.Rhombus

let createTest sideLength expected testName =
    test testName {
        let actual = create sideLength
        Expect.equal actual expected "Results must be the same"
    }

let generalTests =
    [ createTest 2 [ " * "; "***"; " * " ] "side length 2"
      createTest 3 [ "  *  "; " *** "; "*****"; " *** "; "  *  " ] "side length 3"
      createTest
          4
          [ "   *   "; "  ***  "; " ***** "; "*******"; " ***** "; "  ***  "; "   *   " ]
          "side length 4" ]
    |> testList "Rhombus general tests"
