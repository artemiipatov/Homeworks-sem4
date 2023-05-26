namespace Homework7

open System
open System.Net.Http
open System.Text.RegularExpressions
open System.Threading.Tasks

type MiniCrawler() =
    let client = new HttpClient()

    interface IDisposable with
        member this.Dispose() = client.Dispose()

    member this.readAsync(url: string) =
        task {
            let! html = client.GetStringAsync(url)
            return html
        }

    member this.getInfoAsync(url: string) =
        task {
            let! html = this.readAsync url
            return url, html.Length
        }

    member this.getRefsInfo url =
        let hrefRegex = Regex(@"<a href=""?(https://?\S*)""", RegexOptions.Compiled)

        let fetchedUrl = (this.readAsync url).Result

        fetchedUrl
        |> hrefRegex.Matches
        |> Seq.map (fun x ->
            let url = x.Groups[1].Value
            this.getInfoAsync url
        )
        |> Task.WhenAll
