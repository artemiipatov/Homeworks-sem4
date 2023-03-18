namespace Homework2

type Tree<'a> =
    | Node of value: 'a * left: Tree<'a> * right: Tree<'a>
    | Empty

module Tree =

    let rec map op tree =

        match tree with
        | Node(value, left, right) ->
            (op value, map op left, map op right)
            |> Node
        | Empty -> Empty
