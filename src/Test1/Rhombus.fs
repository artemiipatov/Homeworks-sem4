namespace Test1

module Rhombus =
    let createLayer starCount sideLength =
        let rec createRec currentStarCount currentLength (layer: string) =
            if layer.Length = sideLength then
                layer
            else if currentStarCount > starCount then
                createRec currentStarCount (currentLength + 2) (" " + layer + " ")
            else
                createRec (currentStarCount + 2) (currentLength) ("*" + layer + "*")

        createRec 1 1 "*"

    let create side =
        let rec createRec currentRhombus iter =
            let starCount = 2 * iter - 1

            if iter = side then
                currentRhombus
            else
                createRec
                    (currentRhombus @ [ createLayer starCount (side * 2 - 1) ])
                    (iter + 1)

        let result = createRec [] 0

        let reversedList =
            match result |> List.rev with
            | [] -> []
            | _ :: tl -> tl

        result @ reversedList
