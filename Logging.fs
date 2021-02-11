module FPPrank.Logging
open Serilog
open Suave.SerilogExtensions

Log.Logger <-
    LoggerConfiguration()
        .Destructure.FSharpTypes()
        .WriteTo.Console()
        .CreateLogger()

let wrapWithLogging = SerilogAdapter.Enable
