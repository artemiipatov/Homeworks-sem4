module Homeworks.Tests.Phonebook

open System.IO
open Expecto
open Homework4.PhonebookInteractive
open Homework4

let createTest name query pb expectedResult =
    test name {
        let actual = processQuery pb query
        Expect.equal actual expectedResult "Results should be the same"
    }

let saveTest =
    test "save" {
        let path = "pb.txt"
        File.Create(path).Dispose()

        let pb =
            [ { Name = "John"
                Number = "12345" }
              { Name = "Mike"
                Number = "98765" } ]

        let query = [| "Save"; path |]
        let actual = processQuery pb query

        use reader = new StreamReader(path)
        let actualString = reader.ReadToEnd()

        let expectedString = "John 12345\nMike 98765\n"

        Expect.equal actualString expectedString "Strings should be the same"
        Expect.equal actual (Success pb) "Results should be the same"
    }

let readTest =
    test "Read" {
        let path = "pb1.txt"
        let file = File.Create(path)
        use writer = new StreamWriter(file)

        writer.Write("John 12345\nMike 98765\n")

        writer.Dispose()
        file.Dispose()

        let expectedPb =
            [ { Name = "John"
                Number = "12345" }
              { Name = "Mike"
                Number = "98765" } ]

        let query = [| "Read"; path |]
        let actual = processQuery [] query

        Expect.equal actual (Success expectedPb) "Strings should be the same"
    }

let tests =
    let record1 =
        { Name = "John"
          Number = "12345" }

    let record2 =
        { Name = "Mike"
          Number = "98765" }

    let pb1 = [ record1 ]
    let pb2 = [ record1; record2 ]

    [ createTest "add to empty pb" [| "Add"; "John"; "12345" |] [] (Success pb1)
      createTest "add to non-empty pb" [| "Add"; "Mike"; "98765" |] pb1 (Success pb2)

      createTest
          "find name in non-empty pb"
          [| "FindName"; "98765" |]
          pb2
          (SuccessWith record2)

      createTest
          "find name in empty pb"
          [| "FindName"; "98765" |]
          []
          (Fail "Name was not found")

      createTest
          "find number in non-empty pb"
          [| "FindNumber"; "Mike" |]
          pb2
          (SuccessWith record2)

      createTest
          "find number in empty pb"
          [| "FindNumber"; "Mike" |]
          []
          (Fail "Number was not found")

      saveTest
      readTest ]
    |> testList "pb tests"
