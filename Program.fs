// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Net
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

[<EntryPoint>]
let main _ =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.create HTTP IPAddress.Loopback (uint16 80) ] }

    startWebServer config handle
    0 // return an integer exit code
