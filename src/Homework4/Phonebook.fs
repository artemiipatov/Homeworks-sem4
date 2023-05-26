namespace Homework4

open System.IO

type Entry =
    { Name: string
      Number: string }

type Phonebook = Entry list

type Query =
    | Add of string * string
    | FindNumber of string
    | FindName of string
    | Save of string
    | Read of string

type Result =
    | Success of Phonebook
    | SuccessWith of Entry
    | Fail of string

module Phonebook =
    let add name number (pb: Phonebook) =
        pb
        @ [ { Name = name
              Number = number } ]
        |> Success

    let findByName name (pb: Phonebook) =
        match List.tryFind (fun (entry: Entry) -> entry.Name = name) pb with
        | None -> Fail "Number was not found"
        | Some entry -> SuccessWith entry

    let findByNumber number (pb: Phonebook) =
        match List.tryFind (fun (entry: Entry) -> entry.Number = number) pb with
        | None -> Fail "Name was not found"
        | Some entry -> SuccessWith entry

    let write (path: string) (pb: Phonebook) =
        try
            use writer = new StreamWriter(File.Open(path, FileMode.Create))

            for entry in pb do
                writer.WriteLine $"{entry.Name} {entry.Number}"

            Success pb
        with _ ->
            Fail "Failed to write"

    let read (path: string) (phoneBook: Phonebook) =
        try
            use reader = new StreamReader(path)

            let rec readRec () =
                match reader.EndOfStream with
                | true -> []
                | false ->
                    let entry = reader.ReadLine().Split ' '

                    let entry =
                        { Name = entry.[0]
                          Number = entry.[1] }

                    entry :: readRec ()

            let result = phoneBook @ readRec ()
            Success result
        with _ ->
            Fail "Failed to read from file"

    let processQuery (pb: Phonebook) query =
        match query with
        | Add(name, number) -> add name number pb
        | FindNumber name -> findByName name pb
        | FindName number -> findByNumber number pb
        | Save path -> write path pb
        | Read path -> read path pb

module PhonebookInteractive =
    let processQuery phoneBook query =
        match query with
        | [| "Add"; name; number |] ->
            let addQuery = Add(name, number)
            Phonebook.processQuery phoneBook addQuery
        | [| "FindNumber"; name |] ->
            let findQuery = FindNumber(name)
            Phonebook.processQuery phoneBook findQuery
        | [| "FindName"; number |] ->
            let findQuery = FindName(number)
            Phonebook.processQuery phoneBook findQuery
        | [| "Save"; path |] ->
            let saveQuery = Save(path)
            Phonebook.processQuery phoneBook saveQuery
        | [| "Read"; path |] ->
            let readQuery = Read(path)
            Phonebook.processQuery phoneBook readQuery
        | [| "Print" |] ->
            for entry in phoneBook do
                printfn $"Name: {entry.Name} Number: {entry.Number}"

            Success phoneBook
        | _ -> Fail "Incorrect query"
