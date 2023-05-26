namespace Homework7

open System.Threading

module Lazy =
    type ILazy<'a> =
        abstract member Get: unit -> 'a

    type Lazy<'a>(func) =
        let mutable result = None
        member this.Func: unit -> 'a = func

        interface ILazy<'a> with

            member this.Get() =
                if result.IsNone then
                    result <- Some(this.Func())

                result.Value

    type LazyBlocking<'a>(func) =
        let mutable result = None
        member this.Func: unit -> 'a = func

        interface ILazy<'a> with

            member this.Get() =
                if result.IsNone then
                    (fun () ->
                        if result.IsNone then
                            result <- Some(this.Func())
                    )
                    |> lock this

                result.Value

    type LazyLockFree<'a>(func) =
        let mutable result = None
        member this.Func: unit -> 'a = func

        interface ILazy<'a> with

            member this.Get() =
                if result.IsNone then
                    Interlocked.CompareExchange(&result, Some(this.Func()), None)
                    |> ignore

                result.Value
