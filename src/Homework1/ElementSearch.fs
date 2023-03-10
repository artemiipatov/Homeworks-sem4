namespace Homework1

/// <summary>
/// Module that implements function for searching element in the list
/// </summary>
module ElementSearch =
    /// <summary>
    /// Returns the position of the first element, that is equal to the given one.
    /// </summary>
    /// <param name="list">The input list.</param>
    /// <param name="element">Element to search.</param>
    let find element list =
        let rec findRec counter list =
            match list with
            | head :: _ when head = element -> Some counter
            | _ :: tail ->
                findRec
                    (counter
                     + 1)
                    tail
            | [] -> None

        findRec 0 list
