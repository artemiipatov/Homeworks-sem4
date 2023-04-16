module Homeworks.Tests.PointFree

open Expecto
open Homework2.PointFree

let createTest (c, list) =
    let expected = funcCommon c list
    let actual = funcPointFree c list

    Expect.equal actual expected "Lists should be equal"

let tests =
    testPropertyWithConfig FsCheckConfig.defaultConfig "point free tests" createTest
