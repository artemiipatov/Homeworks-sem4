
open Argu
open System
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.BuildServer


//-----------------------------------------------------------------------------
// Metadata and Configuration
//-----------------------------------------------------------------------------

let sln = __SOURCE_DIRECTORY__ </> ".." </> "Homeworks-sem4.sln"

let src = __SOURCE_DIRECTORY__ </> ".." </> "src"

let srcCodeGlob =
    !! ( src  @@ "**/*.fs")
    ++ ( src  @@ "**/*.fsx")
    -- ( src  @@ "**/obj/**/*.fs")

let testsCodeGlob =
    !! (__SOURCE_DIRECTORY__ </> ".." </> "tests/**/*.fs")
    ++ (__SOURCE_DIRECTORY__ </> ".." </> "tests/**/*.fsx")
    -- (__SOURCE_DIRECTORY__ </> ".." </> "tests/**/obj/**/*.fs")

let srcGlob = src @@ "**/*.??proj"
let testsGlob = __SOURCE_DIRECTORY__ </> ".." </> "tests/**/*.??proj"

let distDir = __SOURCE_DIRECTORY__ </> ".." </> "dist"

let githubToken = Environment.environVarOrNone "GITHUB_TOKEN"

//-----------------------------------------------------------------------------
// Helpers
//-----------------------------------------------------------------------------
let isRelease (targets : Target list) =
    targets
    |> Seq.map(fun t -> t.Name)
    |> Seq.exists ((=)"Release")

let configuration (targets : Target list) =
    let defaultVal = if isRelease targets then "Release" else "Debug"
    match Environment.environVarOrDefault "CONFIGURATION" defaultVal with
    | "Debug" -> DotNet.BuildConfiguration.Debug
    | "Release" -> DotNet.BuildConfiguration.Release
    | config -> DotNet.BuildConfiguration.Custom config

let rec retryIfInCI times fn =
    match Environment.environVarOrNone "CI" with
    | Some _ ->
        if times > 1 then
            try
                fn()
            with
            | _ -> retryIfInCI (times - 1) fn
        else
            fn()
    | _ -> fn()

module dotnet =
    let fantomas args =
        DotNet.exec id "fantomas" args

module FSharpAnalyzers =
    type Arguments =
    | Project of string
    | Analyzers_Path of string
    | Fail_On_Warnings of string list
    | Ignore_Files of string list
    | Verbose
    with
        interface IArgParserTemplate with
            member s.Usage = ""

//-----------------------------------------------------------------------------
// Target Implementations
//-----------------------------------------------------------------------------

let clean _ =
    ["bin"; "temp" ; distDir]
    |> Shell.cleanDirs

    !! srcGlob
    ++ testsGlob
    |> Seq.collect(fun p ->
        ["bin";"obj"]
        |> Seq.map(fun sp ->
            IO.Path.GetDirectoryName p @@ sp)
        )
    |> Shell.cleanDirs

    [
        "paket-files/paket.restore.cached"
    ]
    |> Seq.iter Shell.rm

let dotnetRestore _ =
    [sln]
    |> Seq.map(fun dir -> fun () ->
        let args =
            [
            ]
        DotNet.restore(fun c ->
            { c with
                Common =
                    c.Common
                    |> DotNet.Options.withAdditionalArgs args
            }) dir)
    |> Seq.iter(retryIfInCI 10)

let dotnetBuild ctx =
    let args =
        [
            "--no-restore"
        ]
    DotNet.build(fun c ->
        { c with
            Configuration = configuration ctx.Context.AllExecutingTargets
            Common =
                c.Common
                |> DotNet.Options.withAdditionalArgs args
        }) sln

let dotnetTest ctx =
    DotNet.test(fun c ->
        let args =
            [
                "--no-build"
            ]
        { c with
            Configuration = configuration ctx.Context.AllExecutingTargets
            Common =
                c.Common
                |> DotNet.Options.withAdditionalArgs args
            }) sln

let formatCode _ =
    let result =
        [
            srcCodeGlob
            testsCodeGlob
        ]
        |> Seq.collect id
        // Ignore AssemblyInfo
        |> Seq.filter(fun f -> f.EndsWith("AssemblyInfo.fs") |> not)
        |> String.concat " "
        |> dotnet.fantomas

    if not result.OK then
        printfn "Errors while formatting all files: %A" result.Messages

let checkFormatCode _ =
    let result =
        [
            srcCodeGlob
            testsCodeGlob
        ]
        |> Seq.collect id
        // Ignore AssemblyInfo
        |> Seq.filter(fun f -> f.EndsWith("AssemblyInfo.fs") |> not)
        |> String.concat " "
        |> sprintf "%s --check"
        |> dotnet.fantomas

    if result.ExitCode = 0 then
        Trace.log "No files need formatting"
    elif result.ExitCode = 99 then
        failwith "Some files need formatting, check output for more info"
    else
        Trace.logf "Errors while formatting: %A" result.Errors

let initTargets () =
    BuildServer.install [
        GitHubActions.Installer
    ]
    /// Defines a dependency - y is dependent on x
    let (==>!) x y = x ==> y |> ignore
    /// Defines a soft dependency. x must run before y, if it is present, but y does not require x to be run.
    let (?=>!) x y = x ?=> y |> ignore
//-----------------------------------------------------------------------------
// Hide Secrets in Logger
//-----------------------------------------------------------------------------
    Option.iter(TraceSecrets.register "<GITHUB_TOKEN>" ) githubToken

    //-----------------------------------------------------------------------------
    // Target Declaration
    //-----------------------------------------------------------------------------

    Target.create "Clean" clean
    Target.create "DotnetRestore" dotnetRestore
    Target.create "DotnetBuild" dotnetBuild
    Target.create "DotnetTest" dotnetTest
    Target.create "FormatCode" formatCode
    Target.create "CheckFormatCode" checkFormatCode

    //-----------------------------------------------------------------------------
    // Target Dependencies
    //-----------------------------------------------------------------------------

    "Clean" ?=>! "DotnetRestore"

    "DotnetRestore"
        ==> "CheckFormatCode"
        ==> "DotnetBuild"
        ==>! "DotnetTest"

//-----------------------------------------------------------------------------
// Target Start
//-----------------------------------------------------------------------------

[<EntryPoint>]
let main argv =
    argv
    |> Array.toList
    |> Context.FakeExecutionContext.Create false "build.fsx"
    |> Context.RuntimeContext.Fake
    |> Context.setExecutionContext
    initTargets ()
    Target.runOrDefaultWithArguments "DotnetTest"

    0
