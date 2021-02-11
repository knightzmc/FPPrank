module FPPrank.Main

open FPPrank.Model.Model
open Logging
open Infrastructure
open Suave
open Suave.SerilogExtensions
open Suave.Writers
open Suave.Operators
open Suave.DotLiquid


setTemplatesDir "./templates"

let urlResponse (ctx: HttpContext) =
    let randUrl = randomUrl ()
    let logger = ctx.Logger()
    let url = ctx.request.url.AbsolutePath
    let title = url |> formatUrlToTitle

    let data: Model =
        { title = title
          url = url
          redirect = randUrl }

    logger.Information("Replying to {sourceUrl} with redirect to {randUrl}", randUrl, ctx.connection.ipAddr)
    page "index.liquid" data

let noCache =
    setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
    >=> setHeader "Pragma" "no-cache"
    >=> setHeader "Expires" "0"

let webApp =
    context (urlResponse) >=> noCache
    |> wrapWithLogging

[<EntryPoint>]
let main _ =
    let config =
        { defaultConfig with
              bindings = [ HttpBinding.createSimple HTTP "0.0.0.0" 80 ] }

    startWebServer config webApp
    0
