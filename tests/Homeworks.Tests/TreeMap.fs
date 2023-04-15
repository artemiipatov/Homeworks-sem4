module Homeworks.Tests.TreeMap

open Expecto
open Homework2

let arrayToTree array =
    let length = array |> Array.length

    let lastIndex = (length / 2) - 1

    let rec makeTreeRec acc =
        match array with
        | _ when acc >= length -> Empty
        | _ -> Node(array[acc], makeTreeRec (acc * 2 + 1), makeTreeRec (acc * 2 + 2))

    makeTreeRec 0

let treeToArray length tree =
    let result = Array.zeroCreate length

    let rec treeToArrayRec curTree index =
        match curTree with
        | Empty -> ()
        | Node(value, left, right) ->
            result[index] <- value
            treeToArrayRec left (index * 2 + 1)
            treeToArrayRec right (index * 2 + 2)

    treeToArrayRec tree 0
    result

let makeTest (array, c) =
    let length = array |> Array.length
    let op x = x * c
    let tree = arrayToTree array

    let actualArray = tree |> Tree.map op |> treeToArray length

    let expectedArray = array |> Array.map op

    Expect.sequenceEqual actualArray expectedArray "arrays should be equal"

let treeMapTests =
    makeTest
    |> testProperty "Map should apply input function to all elements of the tree"
