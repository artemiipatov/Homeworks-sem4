namespace Homework1

/// <summary>
/// Module that contains function for list reversing.
/// </summary>
module ListReverse =
    /// <summary>
    /// Reverse the list.
    /// </summary>
    /// <param name="list">List to reverse.</param>
    let reverse list =
        let rec reverseRec acc tail =
            match tail with
            | [] -> acc
            | hd::tl -> reverseRec (hd::acc) tl

        match list with
        | [] -> []
        | list -> reverseRec [] list
