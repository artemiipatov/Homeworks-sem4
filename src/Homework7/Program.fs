open Homework7

let crawler = new MiniCrawler()

let info =
    (crawler.getRefsInfo "https://oops.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova")
        .Result

for i in info do
    printfn $"\"{i}\""
