namespace Homework4

open System.IO

type Name = string

type Number = string

type Entry = {
        Name: Name
        Number: Number
    }

type PhoneBook = Entry list

type Path = string

type Query =
    | Add of Name * Number
    | FindNumber of Name
    | FindName of Number
    | Save of Path
    | Read of Path

type Result =
    | SuccessUpdate of PhoneBook
    | SuccessFind of Entry
    | Success
    | Fail of string

module PhoneBook =
    let write (path: string) (pb: PhoneBook) =
        try
            use writer = new StreamWriter(File.Open(path, FileMode.Create))
            for entry in pb do
                writer.WriteLine $"{entry.Name} {entry.Number}"

            Success
        with
            | _ -> Fail "Failed to write"

    let read (path: string) (phoneBook: PhoneBook) =
        try
            use reader = new StreamReader(path)
            let rec readRec () =
                match reader.EndOfStream with
                | true -> []
                | false ->
                    let entry = reader.ReadLine().Split ' '
                    let entry = { Name = entry.[0]; Number = entry.[1] }
                    entry :: readRec ()

            let result = phoneBook @ readRec ()
            SuccessUpdate result
        with
            | _ -> Fail "Failed to read from file"

    let processQuery (pb: PhoneBook) query =
        match query with
        | Add (name, number) ->
            pb @ [ { Name = name; Number = number } ]
            |> SuccessUpdate

        | FindNumber name ->
             match List.tryFind (fun (entry: Entry) -> entry.Name = name) pb with
             | None -> Fail "Name was not found"
             | Some entry -> SuccessFind entry

        | FindName number ->
             match List.tryFind (fun (entry: Entry) -> entry.Number = number) pb with
             | None -> Fail "Number was not found"
             | Some entry -> SuccessFind entry

        | Save path -> write path pb
        | Read path -> read path pb

module PhoneBookInterface =
    let processQuery phoneBook query =
        match query with
        | [| "Add"; name; number |] ->
            let addQuery = Add(name, number)
            PhoneBook.processQuery phoneBook addQuery
        | [| "FindNumber"; name |] ->
            let findQuery = FindNumber(name)
            PhoneBook.processQuery phoneBook findQuery
        | [| "FindName"; number |] ->
            let findQuery = FindNumber(number)
            PhoneBook.processQuery phoneBook findQuery
        | [| "Save"; path |] ->
            let saveQuery = Save(path)
            PhoneBook.processQuery phoneBook saveQuery
        | [| "Read"; path |] ->
            let readQuery = FindNumber(path)
            PhoneBook.processQuery phoneBook readQuery
        | [| "Print" |] ->
            for entry in phoneBook do
                printfn $"Name: {entry.Name} Number: {entry.Number}"
            Success
        | _ -> Fail "Incorrect query"
