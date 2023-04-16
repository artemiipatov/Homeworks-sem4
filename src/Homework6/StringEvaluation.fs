namespace Homework6

module StringEvaluation =
    let parse (x: string) =
        match System.Int32.TryParse x with
        | true, n -> Some n
        | _ -> None

    type CalculateBuilder() =
        member this.Bind(x: string, f) =
            match parse x with
            | Some n -> f n
            | _ -> None

        member this.Return(x: int) = Some x

    let calculate = CalculateBuilder()
