namespace Homework4

module BracketChecker =
    type Facing =
        | Fore
        | Back

    type Bracket =
        | Round
        | Square
        | Curly

    type Symbol =
        | Bracket of Bracket * Facing
        | Other

    let getSymbolType symbol =
        match symbol with
        | ')' -> Bracket(Round, Back)
        | '(' -> Bracket(Round, Fore)
        | ']' -> Bracket(Square, Back)
        | '[' -> Bracket(Square, Fore)
        | '}' -> Bracket(Curly, Back)
        | '{' -> Bracket(Curly, Fore)
        | _ -> Other

    let check brackets =
        let rec checkRec currentBrackets currentStack =
            match currentBrackets, currentStack with
            | [], [] -> true
            | [], _ -> false

            | h :: tail, [] ->
                match getSymbolType h with
                | Other -> checkRec tail []
                | Bracket(bracketType, Fore) ->
                    checkRec tail (bracketType :: currentStack)
                | _ -> false

            | h :: tail, stackHead :: stackTail ->
                match getSymbolType h with
                | Other -> checkRec tail currentStack
                | Bracket(bracketType, Fore) ->
                    checkRec tail (bracketType :: currentStack)
                | Bracket(bracketType, Back) when bracketType = stackHead ->
                    checkRec tail stackTail
                | _ -> false

        let charList = brackets |> List.ofSeq
        checkRec charList []
