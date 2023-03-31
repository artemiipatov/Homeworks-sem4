namespace Homework2

module LambdaInterpreter =
    type VarName = string 

    type Term =
        | Var of VarName
        | Abs of VarName * Term
        | App of Term * Term

    // let rename varName =
    //     match varName with
    //     | Var name ->

    let reduce expression =

        let rec replace mainTerm oldTerm newTerm =
            match mainTerm with
            | App(term1, term2) -> App(replace term1 oldTerm newTerm, replace term2 oldTerm newTerm)
            | Abs(var, term) -> Abs(var, replace term oldTerm newTerm)
            | Var var ->
                if var = oldTerm then newTerm else var |> Var

        let rec apply mainTerm applicableTerm =
            match mainTerm with
            | App(term1, term2) -> App(apply term1 applicableTerm, apply term2 applicableTerm)
            | Abs(var, term) -> replace term var applicableTerm
            | Var var -> var |> Var

        let rec reduceRec exp =
            match exp with
            | App(mainTerm, applicableTerm) -> apply mainTerm applicableTerm
            | Abs(var, term) -> Abs(var, term)
            | Var var -> Var var

        reduceRec expression
