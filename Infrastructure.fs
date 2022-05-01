module FPPrank.Infrastructure

open System

let urls =
    [| "elixir-lang.org"
       "haskell.org"
       "elm-lang.org"
       "ocaml.org"
       "fsharp.org"
       "clojure.org"
       "cdn.discordapp.com/attachments/695431668944732270/823918793402744862/1616507924294.mp4" 
       "en.wikipedia.org/wiki/Simon_Peyton_Jones" 
       "en.wikipedia.org/wiki/Rich_Hickey" 
       "hackage.haskell.org/package/base-4.15.0.0/docs/Prelude.html#t:Monad"
       "github.com/haskell/haskell-mode"
       "en.wikipedia.org/wiki/Pure_function" |]

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
        let other = str.[1..]
        Some(first + other)

let words str = String.split '-' str

let lastIndexOf (str: string) (c: string) = str.LastIndexOf c
let subString (i: string) (s: int) = i.Substring s

let getPathAfterUrl (str: string) =
    lastIndexOf str "/" |> (+) 1 |> subString str

let formatUrlToTitle =
    getPathAfterUrl
    >> words
    >> List.map capitalise
    >> List.filter Option.isSome
    >> List.map Option.get
    >> Seq.ofList
    >> String.concat " "
