namespace Homework2

/// <summary>
///Module, that contains functions for counting even numbers in the list using different functions.
/// </summary>
module EvenNumbers =
    /// <summary>
    /// Counts number of even numbers in the list using map function.
    /// </summary>
    /// <param name="list">Input list.</param>
    let CountUsingMap list =
        list |> List.map (fun v -> abs v % 2) |> List.sum |> (-) (List.length list)

    /// <summary>
    /// Counts number of even numbers in the list using filter function.
    /// </summary>
    /// <param name="list">Input list.</param>
    let CountUsingFilter list =
        list |> List.filter (fun v -> (v % 2) = 0) |> List.length

    /// <summary>
    /// Counts number of even numbers in the list using fold function.
    /// </summary>
    /// <param name="list">Input list.</param>
    let CountUsingFold list =
        list |> (List.fold (fun state v -> state + abs v % 2) 0) |> (-) (List.length list)
