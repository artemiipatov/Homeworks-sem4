namespace Homework2

module ParseTree =

    type NodeType<'a> =
        | Number of 'a
        | Operation of ('a -> 'a -> 'a) * NodeType<'a> * NodeType<'a>

        member this.eval =
            match this with
            | Number n -> n
            | Operation(op, left, right) -> (op left.eval right.eval)

    type Tree<'b> =
        | Node of NodeType<'b>
        | Empty

    let eval tree =
        match tree with
        | Node node -> Some node.eval
        | Empty -> None
