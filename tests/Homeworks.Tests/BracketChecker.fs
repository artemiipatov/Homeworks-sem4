module Homeworks.Tests.BracketChecker

open Expecto
open Homework4.BracketChecker

let getName brackets =
    $"test on {brackets}"

let createTest brackets expected =
    test (getName brackets) {
        let actual = check brackets
        Expect.equal actual expected "Results must be the same"
    }

let generalTests =
    [ createTest
          "(()([][[{{(aadf) fdf} fdfa}]] klne)(){ rgqie}{} rgqrkqgie [][[r qiermgiqegi miemrgie{()()(({}))}]])"
          true

      createTest "" true

      createTest "({([])})(2+3)() * ()(){}{kgkwemrgkmqe}{}{}[][qrgqerger][][]" true

      createTest "(]" false

      createTest
          "(()([][[{{(aadf) fdf} fdfa}]] klne)() rgqie}{} rgqrkqgie [][[r qiermgiqegi miemrgie{()()(({}))}]])"
          false

      createTest
          "(()([][[{{(aadf) fdf} fdfa}]] klne)(){ rgqie}{} rgqrkqgie [[[r qiermgiqegi miemrgie{()()(({}))}]])"
          false

      createTest
          "(()([][[{{(aadf) fdf} fdfa}]] klne)(){ rgqie}{} rgqrkqgie [[[r qiermgiqegi miemrgie{())(({}))}]])"
          false ]
    |> testList "general tests"
