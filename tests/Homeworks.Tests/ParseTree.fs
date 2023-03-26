namespace Homeworks.Tests

open Expecto
open Homework2.ParseTree

module ParseTree =
    let config = Utils.defaultConfig

    let evalTree sourceTree =
        let rec evalTreeRec tree =
            match tree with
            | Literal n -> n
            | Operation(op, left, right) -> op (evalTreeRec left) (evalTreeRec right)

        evalTreeRec sourceTree

    let checkResult tree =
        let actual = eval tree

        let expected =
            match tree with
            | Empty -> None
            | Node t -> Some(evalTree t)

        Expect.isTrue (actual = expected) "Results should be the same"

    let ParseTreeTests =
        checkResult |> testPropertyWithConfig config "Int parse tree tests"
