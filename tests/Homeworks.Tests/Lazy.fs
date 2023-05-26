module Homeworks.Tests.Lazy

open System.Threading.Tasks
open Homework7.Lazy
open Expecto
open System.Threading

let checkComputeOnlyOnceSequential =
    test "unsafe" {
        let counter = ref 0

        let func () =
            counter.Value <- counter.Value + 1

        let l: ILazy<_> = Lazy func

        for _ in 0..10 do
            l.Get()

        Expect.equal counter.Value 1 "Counter should increment only once"
    }

let runParallel amount (l: ILazy<'a>) =
    Seq.init amount (fun _ -> async { return l.Get() })
    |> Async.Parallel
    |> Async.StartAsTask

let refsAreEqual amount (l: ILazy<_>) =
    (runParallel amount l).Result
    |> Array.pairwise
    |> Array.map obj.ReferenceEquals
    |> Array.reduce (&&)

let checkComputeOnlyOnceBlocking =
    test "blocking" {
        let counter = ref 0

        let func () =
            Interlocked.Increment counter |> ignore
            obj ()

        let lazyBlocking = LazyBlocking(func) :> ILazy<_>

        Expect.isTrue
            (refsAreEqual 200 lazyBlocking && (counter.Value = 1))
            "Refs should be equal and counter should increment only once"
    }

let checkLockFreeReturnsTheSameResultOnGet =
    test "lock free" {
        use manualResetEvent = new ManualResetEvent false

        let func () =
            manualResetEvent.WaitOne() |> ignore
            obj ()

        let lazyLockFree = LazyLockFree(func) :> ILazy<_>

        manualResetEvent.Set() |> ignore

        Expect.isTrue (refsAreEqual 200 lazyLockFree) "Refs should be equal"
    }

let tests =
    [ checkComputeOnlyOnceSequential
      checkComputeOnlyOnceBlocking
      checkLockFreeReturnsTheSameResultOnGet ]
    |> testList "Lazy computes only once"
