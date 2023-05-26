module Homeworks.Tests.Lazy

open System.Threading.Tasks
open Homework7.Lazy
open Expecto
open System.Threading

let checkComputeOnlyOnceUnsafe =
    test "unsafe" {
        let counter = ref 0
        let func () = counter.Value <- counter.Value + 1

        let l: ILazy<_> = Lazy func
        for _ in 0 .. 10 do
            l.Get()
        
        Expect.equal counter.Value 1 "Counter should increment only once"
    }

let computeParallel (lazyInstance: ILazy<_>) =
    Parallel.For(0, 100, (fun _ -> lazyInstance.Get () |> ignore))

let makeTestComputeOnlyOnceParallel (lazyConstructor: (unit -> unit) -> ILazy<unit>) =
    let counter = ref 0
    let func () = Interlocked.Increment counter |> ignore

    let lazyInstance = lazyConstructor func
    
    computeParallel lazyInstance

let lazyComputesOnlyOnce =
    [ makeTestComputeOnlyOnceParallel (fun action -> LazyThreadSafe action)
      makeTestComputeOnlyOnceParallel (fun action -> LazyLockFree action) ]
