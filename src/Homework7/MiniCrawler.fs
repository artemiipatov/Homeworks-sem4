namespace Homework7

open System.Net
open System.IO
open System.Text.RegularExpressions

module MiniCrawler =
    let readAsync (url: string) =
        async {
            let request = WebRequest.Create(url)
            use! response = request.AsyncGetResponse()

            use stream = response.GetResponseStream()

            use reader = new StreamReader(stream)
            let html = reader.ReadToEnd()

            return html
        }

    let printInfoAsync (url: string) =
        async {
            let! html = readAsync url

            do printfn $"%s{url} — %d{html.Length}"
        }

    let getRefsInfo url =
        let fetchedUrl = url |> readAsync |> Async.RunSynchronously

        Regex.Matches(
            fetchedUrl,
            """<a href\s*=\s*(?:["'](?<1>[^"']*)["']|(?<1>[^>\s]+))"""
        )
        |> Seq.iter (fun x ->
            printfn "%A" x.Value
            x.Value |> printInfoAsync |> Async.Start
        )
