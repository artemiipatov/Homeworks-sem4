namespace Homeworks.Tests

open Expecto
open FsCheck

module Utils =
    let defaultConfig =
        { FsCheckConfig.defaultConfig with
            arbitrary = [ typeof<Generators.ParseTreeGenerator> ]
            maxTest = 10 }
