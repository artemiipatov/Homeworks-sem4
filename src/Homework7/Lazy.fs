namespace Homework7

open System.Threading

module Lazy =
    type ILazy<'a> =
        abstract member Get: unit -> 'a

    type Lazy<'a>(func) =
        member this.Func: unit -> 'a = func

        member val Result = None with get, set

        interface ILazy<'a> with

            member this.Get() =
                match this.Result with
                | Some res -> res
                | None ->
                    let result = func ()
                    this.Result <- Some result
                    result

    type LazyThreadSafe<'a>(func) =
        member this.Func: unit -> 'a = func

        member val Result = None with get, set

        interface ILazy<'a> with

            member this.Get() =
                (fun () ->
                    match this.Result with
                    | Some res -> res
                    | None ->
                        let result = func ()
                        this.Result <- Some result
                        result
                )
                |> lock this

    type LazyLockFree<'a>(func) =
        let mutable result = None
        member this.Func: unit -> 'a = func

        // member val Result = None with get, set

        interface ILazy<'a> with

            member this.Get() =
                match Volatile.Read(ref result) with
                | Some res -> res
                | None ->
                    let res = func ()
                    Volatile.Write(ref result, Some res)
                    res
