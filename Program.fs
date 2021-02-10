// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Suave
open Suave.Redirection
open Suave.SerilogExtensions
open Serilog
open Suave.Writers
open Suave.Filters
open Suave.Filters
open Suave.Operators
open Suave.Successful

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

let urlResponse (ctx: HttpContext) =
    let randUrl = randFrom urls random |> (+) "https://"
    let logger = ctx.Logger()
   
    logger.Information("Replying with redirect to {randUrl}", randUrl)

    found randUrl

let noCache =
  setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
  >=> setHeader "Pragma" "no-cache"
  >=> setHeader "Expires" "0"
  
let webApp = noCache >=> context (urlResponse)
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

    startWebServer config webAppWithLogging
    0
