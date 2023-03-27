module Homeworks.Tests.TreeMap

open Expecto
open Homework2

let arrayToTree array =
    let length = array |> Array.length

    let lastIndex = (length / 2) - 1

    let rec makeTreeRec acc =
        match array with
        | _ when acc > lastIndex -> Empty
        | _ -> Node(array[acc], makeTreeRec (acc * 2 + 1), makeTreeRec (acc * 2 + 2))

    makeTreeRec 0

let treeToArray tree =
    let rec treeToArrayRec curTree =
        match curTree with
        | Empty -> []
        | Node(value, Empty, Empty) -> value :: []
        | Node(value, left, Empty) -> value :: treeToArrayRec left
        | Node(value, left, right) ->
            value :: (treeToArrayRec left) @ (treeToArrayRec right)

    tree |> treeToArrayRec |> List.toArray

let makeTest array =
    let op x = x * 2
    let tree = arrayToTree array

    let actualArray = tree |> Tree.map op |> treeToArray

    let expectedArray = array |> Array.map op

    Expect.sequenceEqual actualArray expectedArray "arrays should be equal"

let treeMapTests =
    makeTest
    |> testProperty "Map should apply input function to all elements of the tree"
