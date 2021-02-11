module FPPrank.Main

open FPPrank.Model.Model
open Logging
open Model
open Infrastructure
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
open Suave.DotLiquid
open DotLiquid


setTemplatesDir "./templates"

let urlResponse (ctx: HttpContext) =
    let randUrl = randomUrl ()
    let logger = ctx.Logger()
    let url = ctx.request.url.AbsolutePath
    let title = url |> formatUrlToTitle
    let data: Model = { title = title; url = url; redirect = randUrl }
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
