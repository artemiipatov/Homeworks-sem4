namespace Homework6

module Round =
    let round (value: float) (precision: int) =
        System.Math.Round(value, precision)

    type RoundBuilder(precision: int) =
        member this.Bind(x: float, f) =
            let roundedValue = round x precision
            f roundedValue

        member this.Return(x: float) = round x precision
