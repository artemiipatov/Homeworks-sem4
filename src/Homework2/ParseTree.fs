namespace Homework2

/// <summary>
/// Module that contains parse tree implementation and its evaluator
/// </summary>
module ParseTree =
    /// <summary>
    /// Type of parse tree node.
    /// </summary>
    type NodeType<'a> =
        | Literal of 'a
        | Operation of ('a -> 'a -> 'a) * NodeType<'a> * NodeType<'a>

        /// <summary>
        /// Applies operation to left and right sons.
        /// </summary>
        member this.eval =
            match this with
            | Literal n -> n
            | Operation(op, left, right) -> (op left.eval right.eval)

    /// <summary>
    /// Parse tree.
    /// </summary>
    type ParseTree<'b> =
        | Node of NodeType<'b>
        | Empty

    /// <summary>
    /// Evaluates parse tree.
    /// </summary>
    /// <param name="tree">Input parse tree.</param>
    let eval tree =
        match tree with
        | Node node -> Some node.eval
        | Empty -> None
