namespace Homeworks.Tests

open FsCheck
open Homework2.ParseTree

module Generators =
    type ParseTreeGenerator() =
        static let genOperation () =
            gen {
                let! op =
                    Gen.oneof
                        [ gen { return (+) }; gen { return (-) }; gen { return (*) } ]

                return op
            }

        static let genValue (valuesGenerator: Gen<'a>) =
            gen {
                let! value = valuesGenerator
                return value
            }

        static let rec treeGenerator size (valuesGenerator: Gen<'a>) =
            gen {
                let! value = genValue valuesGenerator
                let! op = genOperation ()

                if size = 0 then
                    return Literal value
                else
                    let! left = treeGenerator (size - 1) valuesGenerator
                    let! right = treeGenerator (size - 1) valuesGenerator
                    return Operation(op, left, right)
            }

        static let generateTree (valuesGenerator: Gen<'a>) =
            gen {
                let! size = Gen.sized <| fun s -> Gen.choose (1, s)
                let! tree = treeGenerator size valuesGenerator
                return tree
            }

        static member IntType() =
            Arb.generate<int> |> generateTree |> Arb.fromGen

// let generateTree treeSize =
//     let rec genRec size =
//         match size with
//         | 0 -> Gen.map Literal Arb.generate<int>
//         | n when n > 0 ->
//             let subtree = genRec (n / 2)
//             Gen.oneof [ Gen.map Literal Arb.generate<int>
//                         genOperation subtree]
//         | _ -> invalidArg "size" "Only positive arguments are allowed"
//
//     treeSize |> genRec
