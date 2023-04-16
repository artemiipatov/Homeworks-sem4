namespace Homework2

/// <summary>
/// Represents binary tree.
/// </summary>
type Tree<'a> =
    | Node of value: 'a * left: Tree<'a> * right: Tree<'a>
    | Empty

/// <summary>
/// Module, that contains map function for <see cref="Tree"/>.
/// </summary>
module Tree =
    /// <summary>
    /// Applies given operation to each element of the given tree.
    /// </summary>
    /// <param name="op">Input operation.</param>
    /// <param name="tree">Input tree.</param>
    let rec map op tree =

        match tree with
        | Node(value, left, right) -> (op value, map op left, map op right) |> Node
        | Empty -> Empty
