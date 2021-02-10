// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Suave
open Suave.Redirection
open Suave.SerilogExtensions
open Serilog

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
    found randUrl

let webApp = context (urlResponse)
let webAppWithLogging = SerilogAdapter.Enable(webApp)

Log.Logger <-
   LoggerConfiguration()
    .Destructure.FSharpTypes()
    .WriteTo.Console()
    .CreateLogger()
    
[<EntryPoint>]
let main _ =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.createSimple HTTP "0.0.0.0" 80 ] }

    startWebServer config webApp
    0
