namespace Fractals.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Fractals
open System.DrawingCore
open System.IO
open Microsoft.Net.Http.Headers
open Microsoft.Extensions.Primitives

[<ApiController>]
[<Route("[controller]")>]
type MandelbrotController (logger : ILogger<MandelbrotController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member __.Get() : FileContentResult =
        let image = Mandelbrot.picture(800, 2.0, 100)
        let stream = new MemoryStream()
        image.Save(stream, Imaging.ImageFormat.Png)
        let res = new FileContentResult(stream.ToArray(), "image/png")
        __.Response.Headers.Add(HeaderNames.ContentDisposition, StringValues("inline; filename=\"mandelbrot.png\"" ) )
        res