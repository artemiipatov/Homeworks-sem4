namespace Homework2

module PointFree =
    /// <summary>
    /// Multiplies every element of the list by given coefficient.
    /// </summary>
    /// <param name="x">Coefficient.</param>
    /// <param name="list">Input list.</param>
    let funcCommon x list =
        List.map (fun y -> y * x) list

    (*
    Steps to convert to point free:
    1. let funcCommon x list = List.map (fun y -> y * x) list
    2. let funcCommon x = List.map (fun y -> y * x)
    3. let funcCommon x = List.map ((*) x)
    4. let funcCommon: int -> int list -> int list = List.map << (*)
    *)

    /// funcCommon written in point free style.
    let funcPointFree: int -> int list -> int list = List.map << (*)
