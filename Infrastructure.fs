module FPPrank.Infrastructure

open System

let urls =
    [| "elixir-lang.org"
       "haskell.org"
       "elm-lang.org"
       "ocaml.org"
       "fsharp.org"
       "clojure.org" |]

let random = Random()

let nth arr i = Array.item i arr

let randFrom (arr: 'a []) (random: Random) =
    Array.length arr |> random.Next |> nth arr

let randomUrl () = randFrom urls random |> (+) "https://"

let capitalise str =
    if str = "" then
        None
    else if str.Length = 1 then
        Some(str.ToUpper())
    else
        let first = Char.ToUpper str.[0] |> Char.ToString
        let rem = str.[1..]
        Some(first + rem)

let splitWords str = String.split '-' str

let lastIndexOf (str: string) (c: string) = str.LastIndexOf c
let subStr (i: string) (s: int) = i.Substring s

let getPathAfterUrl (str: string) =
    lastIndexOf str "/" |> (+) 1 |> subStr str

let formatUrlToTitle =
    getPathAfterUrl
    >> splitWords
    >> List.map capitalise
    >> List.filter Option.isSome
    >> List.map Option.get
    >> Seq.ofList
    >> String.concat " "
