namespace Homework2

/// Lambda interpreter, which supports beta reduction and automatic alpha conversion.
module Interpreter =
    /// Represents variable name.
    type VarName = string

    /// Represents lambda term.
    type Term =
        | Var of VarName
        | Abs of VarName * Term
        | App of Term * Term

    /// <summary>
    /// Replaces all occurrences of the one term with another.
    /// </summary>
    /// <param name="expression">Lambda expression where terms are replaced.</param>
    /// <param name="oldTerm">Term that is being replaced.</param>
    /// <param name="newTerm">New term.</param>
    let rec replace expression oldTerm newTerm =
        match expression with
        | App(term1, term2) -> App(replace term1 oldTerm newTerm, replace term2 oldTerm newTerm)
        | Abs(var, term) ->
            match newTerm with
            | Var v when v = var ->
                match (renameBoundVar term var) with
                | Abs(newVar, t) -> Abs(newVar, replace t oldTerm newTerm)
                | _ -> failwith "Rename output type should be the same as the input one."
            | _ -> Abs(var, replace term oldTerm newTerm)
        | Var var -> if var = oldTerm then newTerm else var |> Var

    /// <summary>
    /// Renames given bound variable.
    /// </summary>
    /// <param name="absBody">Abstraction body in which renaming take place.</param>
    /// <param name="oldName">Name of the bound variable to be renamed.</param>
    and renameBoundVar absBody (oldName: VarName) =
        let newName = ((oldName |> string) + "'") |> VarName
        let newVar = newName |> Var
        let newTerm = replace absBody oldName newVar
        Abs(newName, newTerm)

    /// <summary>
    /// Applies one term to another.
    /// </summary>
    /// <param name="mainTerm">Term to which apply another term.</param>
    /// <param name="applicableTerm">Term to apply.</param>
    let rec apply mainTerm applicableTerm =
        match mainTerm with
        | App _ -> App(mainTerm, applicableTerm)
        | Abs(var, term) -> replace term var applicableTerm
        | Var var -> var |> Var

    /// <summary>
    /// Converts given lambda expression to beta normal form.
    /// </summary>
    /// <param name="expression"></param>
    let reduce expression =
        let rec reduceRec exp =
            match exp with
            | App(mainTerm, applicableTerm) ->
                match mainTerm with
                | Var _ -> App(mainTerm, reduceRec applicableTerm)
                | App _ -> apply (reduceRec mainTerm) applicableTerm
                | _ -> reduceRec (apply mainTerm applicableTerm)
            | Abs(var, term) -> Abs(var, reduceRec term)
            | Var var -> Var var

        reduceRec expression
