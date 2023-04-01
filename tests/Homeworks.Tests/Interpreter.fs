namespace Homeworks.Tests

open Expecto
open Homework2.Interpreter

module Interpreter =
    let v (n: string) = n |> VarName
    let vv (n: string) = n |> VarName |> Var

    let generalTests =
        testList
            "general tests"
            [ test "test 1" {
                  let exp1 =
                      Abs(v "m", Abs(v "n", Abs(v "a", Abs(v "b", App(App(vv "m", App(App(vv "n", vv "a"), vv "b")), vv "b")))))

                  let exp2 = Abs(v "f", Abs(v "x", vv "x"))
                  let exp3 = Abs(v "f", Abs(v "x", App(vv "f", vv "x")))
                  let expression = App(App(exp1, exp2), exp3)

                  let actual = reduce expression
                  let expected = Abs(v "a", Abs(v "b", vv "b"))
                  Expect.equal actual expected "terms should be equal"
              } ]

    let alphaConversionTest =
        testList
            "alpha conversion tests"
            [ test "variables should be renamed while reducing" {
                  let expression =
                      App(App(App(Abs(v "x", Abs(v "y", Abs(v "z", App(vv "y", App(vv "z", vv "x"))))), vv "y"), vv "x"), vv "z")

                  let actual = reduce expression
                  let expected = App(App(vv "x", vv "z"), vv "y")
                  Expect.equal actual expected "terms should be equal"
              }

              test "only bound variables should be renamed" {
                  let expression =
                      App(App(Abs(v "x", Abs(v "y", Abs(v "z", Abs(v "m", Abs(v "n", App(vv "k", App(vv "m", App(vv "x", App(vv "y", App(vv "z", vv "n")))))))))), vv "m"), vv "m")

                  let actual = reduce expression

                  let expected =
                      Abs(v "z'", Abs(v "m'", Abs(v "n", App(vv "k", App(vv "m'", App(vv "z", App(vv "m", App(vv "z'", vv "n"))))))))

                  Expect.equal actual expected "terms should be equal"
              } ]
