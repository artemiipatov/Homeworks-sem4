module Homeworks.Tests.MiniCrawler

open Expecto
open Homework7

let checkPageSize (crawler: MiniCrawler) url length =
    test "page size" {
        let actual = crawler.getInfoAsync(url).Result
        let expected = url, length
        Expect.equal actual expected "size should be the same"
    }

let checkLengths (crawler: MiniCrawler) =
    test "length count" {
        let actual =
            crawler
                .getRefsInfo("https://oops.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova")
                .Result

        let expected =
            [| ("https://oops.math.spbu.ru/SE/search_form", 26404)
               ("https://oops.math.spbu.ru/SE/contacts", 22462)
               ("https://oops.math.spbu.ru/SE/candidate", 17554)
               ("https://oops.math.spbu.ru/SE/student", 19287)
               ("https://oops.math.spbu.ru/SE/info/schedule", 17364)
               ("https://oops.math.spbu.ru/SE/alumni", 49175)
               ("https://oops.math.spbu.ru/SE/seminar", 27492)
               ("https://oops.math.spbu.ru/SE/login_form", 12484)
               ("https://oops.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova?set_language=en",
                15461)
               ("https://oops.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova?set_language=ru",
                17718)
               ("https://oops.math.spbu.ru/SE", 23408)
               ("https://oops.math.spbu.ru/SE/2016", 16924)
               ("https://oops.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova/sendto_form",
                17477)
               ("https://oops.math.spbu.ru/SE/news", 51504)
               ("https://oops.math.spbu.ru/SE/2016/stipendiya-a.n.-terehova", 17686)
               ("https://oops.math.spbu.ru/SE/2014/zaschity-magisterskih-dissertacii-010300-abfundamentalnye-informatika-i-informacionnye-tehnologiibb",
                19501)
               ("https://oops.math.spbu.ru/SE/2014/zaschity-vkr-bakalavrov-010400-abinformacionnye-tehnologiibb",
                22025)
               ("https://oops.math.spbu.ru/SE/2014/predzaschity-magistrov-010300-i-bakalavrov-010400",
                18792)
               ("https://oops.math.spbu.ru/SE/2014/predzaschity-bakalavrov-010400-informacionnye-tehnologii",
                22717)
               ("https://oops.math.spbu.ru/SE/news", 51504) |]

        Expect.equal actual expected "Lengths should be the same"
    }

let tests =
    [ let crawler = new MiniCrawler()
      checkLengths crawler
      checkPageSize crawler "https://www.lettercount.com/" 42452 ]
    |> testList "crawler tests"
