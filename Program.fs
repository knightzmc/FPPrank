// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System
open System.IO
open Suave
open Suave.Headers
open Suave.Response
open Suave.Http
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

let urlResponse _ =
    let randUrl = randFrom urls random |> (+) "https://"
    redirect randUrl
    
let handle = context (urlResponse)

    
[<EntryPoint>]
let main _ =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.createSimple HTTP "0.0.0.0" 80 ] }

    startWebServer config handle
    0 // return an integer exit code
