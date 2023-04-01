namespace Homework2

module LambdaInterpreter =
    type VarName = string

    type Term =
        | Var of VarName
        | Abs of VarName * Term
        | App of Term * Term

    let rec replace mainTerm oldTerm newTerm =
        match mainTerm with
        | App(term1, term2) ->
            App(replace term1 oldTerm newTerm, replace term2 oldTerm newTerm)
        | Abs(var, term) ->
            match newTerm with
            | Var v when v = var ->
                match (rename (Abs(var, term)) var) with
                | Abs(newVar, t) -> Abs(newVar, replace t oldTerm newTerm)
                | _ -> failwith "Rename output type should be the same as input one."
            | _ -> Abs(var, replace term oldTerm newTerm)
        | Var var -> if var = oldTerm then newTerm else var |> Var

    and rename term (oldVar: VarName) =
        let newVar = ((oldVar |> string) + "'") |> VarName |> Var

        replace term oldVar newVar

    let rec apply mainTerm applicableTerm =
        match mainTerm with
        | App(term1, term2) -> App(apply term1 applicableTerm, apply term2 applicableTerm)
        | Abs(var, term) -> replace term var applicableTerm
        | Var var -> var |> Var

    let reduce expression =
        let rec reduceRec exp =
            match exp with
            | App(mainTerm, applicableTerm) ->
                match mainTerm with
                | Var _ -> App(mainTerm, reduceRec applicableTerm)
                | _ -> reduceRec (apply mainTerm applicableTerm)
            | Abs(var, term) -> Abs(var, term)
            | Var var -> Var var

        reduceRec expression
