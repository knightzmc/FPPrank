// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Suave
open Suave.Filters
open Suave.Successful
open Suave.Intermediate
open Suave.Redirection

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

let handle =
    context (fun _ ->
        let randUrl = randFrom urls random |> (+) "https://"
        moved_permanently randUrl)

let inverseLerp a b value =
    if a = b then 0.0 else max 0.0 value |> min 1.0

let pattern pred x = if pred x then Some () else None
let (|Negative|_|) = pattern ((<) 0)
let (|MoreThan1|_|) = pattern ((>) 1)
let clamp value =
    match value with
    | Negative -> 0.0
    | MoreThan1 -> 1.0
    | _ -> float value

[<EntryPoint>]
let main argv =
    startWebServer defaultConfig handle
    0 // return an integer exit code
